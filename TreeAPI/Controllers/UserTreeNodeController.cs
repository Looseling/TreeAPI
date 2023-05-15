using DataAccess.Tree.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TreeAPI.Services;
using System.Linq;
using TreeAPI.Services.TreeAPI.Services;
using DataAccess.Diagnostics.Model;

namespace TreeAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTreeNodeController : ControllerBase
    {
        private readonly NodeService _nodeService;
        private readonly JournalService _journalService;


        public UserTreeNodeController(NodeService nodeService, JournalService journalService)
        {
            _nodeService = nodeService;
            _journalService = journalService;
        }

        private static HashSet<int> exceptionIds = new HashSet<int>();
        private static Random random = new Random();

        [HttpPost]
        public IActionResult CreateNode(
            [FromHeader(Name = "treeName"), Required] string treeName,
            [FromHeader(Name = "parentNodeId"), Required] int parentNodeId,
            [FromHeader(Name = "nodeName"), Required] string nodeName)
        {
            var journalHistory = _journalService.GetAllJournals();
            var lastEventId = journalHistory.LastOrDefault()?.eventId;
            var newEventId = lastEventId.HasValue ? lastEventId.Value + 1 : 1;
            try
            {
                var rootTree = _nodeService.GetNodeByNameRoot(treeName);

                // Check if the tree exists
                if (rootTree == null)
                {

                    _journalService.CreateJournal(newEventId, "Parent node not found");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Tree not found");

                }

                // Check if the parent node exists in the tree
                if (!_nodeService.isParentIdInTree(parentNodeId, rootTree))
                {
                    _journalService.CreateJournal(newEventId, "Parent node not found");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Parent node not found");

                }

                // Create a new node and assign it to the parent node
                var newNode = new Node
                {
                    name = nodeName,
                    isRoot = false,
                    parentNodeId = parentNodeId
                };

                // Create the node after passing the checks
                _nodeService.CreateNode(newNode);

                return Ok(newNode);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the operation
                _journalService.CreateJournal(newEventId, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("deleteNode")]
        public IActionResult DeleteNode(
            [FromHeader(Name = "treeName"), Required] string treeName,
            [FromHeader(Name = "nodeId"), Required] int nodeId)
        {
            var journalHistory = _journalService.GetAllJournals();
            var lastEventId = journalHistory.LastOrDefault()?.eventId;
            var newEventId = lastEventId.HasValue ? lastEventId.Value + 1 : 1;
            try
            {
                // Retrieve the tree root based on the specified treeName
                var treeRoot = _nodeService.GetNodeByNameRoot(treeName);

                // Check if the tree root exists
                if (treeRoot == null)
                {
                    _journalService.CreateJournal(newEventId, "Parent node not found");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Tree not found");
                }

                // Check if the nodeId is in the specified tree root
                if (!_nodeService.isParentIdInTree(nodeId, treeRoot))
                {
                    _journalService.CreateJournal(newEventId, "Specified node is not in the tree");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Specified node is not in the tree");
                }

                // Retrieve the node by its Id
                var nodeToDelete = _nodeService.GetNodeById(nodeId);

                // Check if the node exists
                if (nodeToDelete == null)
                {
                    _journalService.CreateJournal(newEventId, "Node not found");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Node not found");
                }

                // Check if the node has children
                if (nodeToDelete.children.Any())
                {
                    _journalService.CreateJournal(newEventId, "Node has children and cannot be deleted");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Node has children and cannot be deleted");
                }

                // Delete the node
                _nodeService.DeleteNode(nodeToDelete.Id);

                return Ok("Node deleted successfully.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the operation
                _journalService.CreateJournal(newEventId, $"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("rename")]
        public IActionResult RenameNode(
            [FromHeader(Name = "treeName"), Required] string treeName,
            [FromHeader(Name = "nodeId"), Required] int nodeId,
            [FromHeader(Name = "newNodeName"), Required] string newNodeName)
        {
            var journalHistory = _journalService.GetAllJournals();
            var lastEventId = journalHistory.LastOrDefault()?.eventId;
            var newEventId = lastEventId.HasValue ? lastEventId.Value + 1 : 1;
            try
            {
                // Retrieve the tree root based on the specified treeName
                var treeRoot = _nodeService.GetNodeByNameRoot(treeName);
                                // Check if the tree root exists
                    if (treeRoot == null)
                {
                    _journalService.CreateJournal(newEventId, "Parent node not found");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Tree not found");
                }

                // Check if the nodeId is in the specified tree root
                if (!_nodeService.isParentIdInTree(nodeId, treeRoot))
                {
                    _journalService.CreateJournal(newEventId, "Specified node is not in the tree");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Specified node is not in the tree");
                }

                // Retrieve the node by its Id
                var nodeToRename = _nodeService.GetNodeById(nodeId);

                // Check if the node exists
                if (nodeToRename == null)
                {
                    _journalService.CreateJournal(newEventId, "Node not found");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Node not found");
                }

                // Perform the rename operation
                nodeToRename.name = newNodeName;

                // Update the node in the database
                _nodeService.UpdateNode(nodeToRename);

                return Ok("Node renamed successfully");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the operation
                _journalService.CreateJournal(newEventId, $"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}


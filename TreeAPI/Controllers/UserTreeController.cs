using DataAccess.Tree.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TreeAPI.Services;

namespace TreeAPI.Controllers
{
    [ApiController]
    [Route("api/usertree")]
    public class UserTreeController : ControllerBase
    {
        private readonly NodeService _nodeService;

        public UserTreeController(NodeService nodeService)
        {
            _nodeService = nodeService;
        }

        [HttpPost]
        public IActionResult CreateUserTree([FromHeader(Name = "treeName"), Required] string name)
        {
            var rootNode = _nodeService.GetNodeByNameRoot(name);

            if (rootNode == null) { 
                var newRootNode = new Node
                {
                    name = name,
                    isRoot = true
                };
                
                _nodeService.CreateNode(newRootNode);
                return Ok(newRootNode);
            }

            _nodeService.AssignChildrenToNode(rootNode);


            return Ok(rootNode);
        }



    }

}


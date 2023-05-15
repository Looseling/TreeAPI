using DataAccess.Tree.Model;
using TreeAPI.Services.IRepository;

namespace TreeAPI.Services
{
    public class NodeService
    {
        private readonly INodeRepository _nodeRepository;

        public NodeService(INodeRepository nodeRepository)
        {
            _nodeRepository = nodeRepository;
        }

        public Node GetNodeById(int id)
        {
            return _nodeRepository.GetById(id);
        }

        public Node GetNodeByNameRoot(string name)
        {
            return _nodeRepository.GetByNodeNameRoot(name);
        }

      
        public IEnumerable<Node> GetAllNodes()
        {
            return _nodeRepository.GetAll();
        }

        public void CreateNode(Node node)
        {
            _nodeRepository.Add(node);
        }

        public void UpdateNode(Node node)
        {
            _nodeRepository.Update(node);
        }

        public void DeleteNode(int id)
        {
            _nodeRepository.Delete(id);
        }

        public void AssignChildrenToNode(Node node)
        {
            var children = _nodeRepository.GetChildrenByParentId(node.Id).ToList(); // Materialize query results
            if (children != null)
            {
                foreach (var child in children)
                {
                    AssignChildrenToNode(child); // Recursively assign children to each child node
                }
                node.children = children;
            }
        }

        public bool isParentIdInTree(int parentId, Node treeRoot)
        {
            if (treeRoot.Id == parentId)
                return true;

            if (treeRoot.children == null)
                _nodeRepository.LoadDescendantsChildren(treeRoot);

            if (treeRoot.children != null)
            {
                foreach (var child in treeRoot.children)
                {
                    if (isParentIdInTree(parentId, child))
                        return true;
                }
            }

            return false;
        }
    }
}

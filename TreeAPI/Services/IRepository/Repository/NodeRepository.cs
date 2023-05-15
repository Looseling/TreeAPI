using DataAccess.Tree.Model;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace TreeAPI.Services.IRepository.Repository
{
    public class NodeRepository : INodeRepository
    {
        private readonly TreeApiDbContext _dbContext;

        public NodeRepository(TreeApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Node GetById(int id)
        {
            return _dbContext.Nodes.Find(id);
        }

        public Node GetByNodeNameRoot(string name)
        {
            return _dbContext.Nodes.FirstOrDefault(n => n.name == name && n.isRoot == true);
        }

        public IEnumerable<Node> GetAll()
        {
            return _dbContext.Nodes.ToList();
        }

        public void Add(Node node)
        {
            _dbContext.Nodes.Add(node);
            _dbContext.SaveChanges();
        }

        public void Update(Node node)
        {
            _dbContext.Nodes.Update(node);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var node = _dbContext.Nodes.Find(id);
            if (node != null)
            {
                _dbContext.Nodes.Remove(node);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Node> GetChildrenByParentId(int parentId)
        {
            return _dbContext.Nodes
                                .Include(node => node.children) // Eager loading of children
                                .Where(node => node.parentNodeId == parentId)
                                .ToList();
        }

        public void LoadDescendantsChildren(Node parentNode)
        {
            _dbContext.Entry(parentNode)
                .Collection(n => n.children)
                .Load();

            if (parentNode.children != null)
            {
                foreach (var child in parentNode.children)
                {
                    LoadDescendantsChildren(child);
                }
            }
        }
    }
}

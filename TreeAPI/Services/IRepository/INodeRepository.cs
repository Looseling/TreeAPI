using DataAccess.Tree.Model;

namespace TreeAPI.Services.IRepository
{
    public interface INodeRepository
    {
        Node GetById(int id);
        Node GetByNodeNameRoot(string name);
        IEnumerable<Node> GetChildrenByParentId(int parentId);
        IEnumerable<Node> GetAll();
        void Add(Node node);
        void Update(Node node);
        void Delete(int id);
        void LoadDescendantsChildren(Node parentNode);
    }
}

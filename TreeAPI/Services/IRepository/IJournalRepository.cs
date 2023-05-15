using DataAccess.Diagnostics.Model;

namespace TreeAPI.Services.IRepository
{
    public interface IJournalRepository
    {
        MJournal GetById(int id);
        IEnumerable<MJournal> GetAll();
        void Add(MJournal journal);
        void Update(MJournal journal);
        void Delete(int id);
    }

}

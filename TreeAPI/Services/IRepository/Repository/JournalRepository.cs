using DataAccess;
using DataAccess.Diagnostics.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeAPI.Services.IRepository.Repository
{
   

    public class JournalRepository : IJournalRepository
    {
        private readonly TreeApiDbContext _dbContext;

        public JournalRepository(TreeApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MJournal GetById(int id)
        {
            return _dbContext.Journals.FirstOrDefault(j => j.Id == id);
        }

        public IEnumerable<MJournal> GetAll()
        {
            return _dbContext.Journals.ToList();
        }

        public void Add(MJournal journal)
        {
            journal.createdAt = DateTime.Now;
            _dbContext.Journals.Add(journal);
            _dbContext.SaveChanges();
        }

        public void Update(MJournal journal)
        {
            var existingJournal = GetById(journal.Id);
            if (existingJournal != null)
            {
                existingJournal.eventId = journal.eventId;
                existingJournal.text = journal.text;
                existingJournal.createdAt = journal.createdAt;
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var journal = GetById(id);
            if (journal != null)
            {
                _dbContext.Journals.Remove(journal);
                _dbContext.SaveChanges();
            }
        }
    }

}

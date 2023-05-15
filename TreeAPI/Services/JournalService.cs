namespace TreeAPI.Services
{
    using DataAccess.Diagnostics.Model;
    using DataAccess.Diagnostics.View;
    using global::TreeAPI.Controllers;
    using global::TreeAPI.Services.IRepository;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace TreeAPI.Services
    {
        public class JournalService
        {
            private readonly IJournalRepository _journalRepository;

            public JournalService(IJournalRepository journalRepository)
            {
                _journalRepository = journalRepository;
            }

            public MJournal GetJournalById(int id)
            {
                return _journalRepository.GetById(id);
            }
            public VRange GetJournalByFilter(VJournalFilter filter,int skip, int take)
            {
                var journalHistory = _journalRepository.GetAll();
                var journalFiltered = journalHistory.Where(j => j.createdAt >= filter.from && j.createdAt <= filter.to).Skip(skip).Take(take).ToList();
                var range = new VRange
                {
                    skip = skip,
                    count = take,
                    items = journalFiltered.Select(j => new VJournalInfo
                    {
                        Id = j.Id,
                        eventId = j.eventId,
                        createdAt = j.createdAt
                    }).ToList()
                };
                return range;
            }

            public IEnumerable<MJournal> GetAllJournals()
            {
                return _journalRepository.GetAll();
            }

            public void CreateJournal(int _eventId, string _text)
            {

                var journalEntry = new MJournal
                {
                    eventId = _eventId,
                    text = _text,
                    createdAt = DateTime.Now
                };
                _journalRepository.Add(journalEntry);
            }

            public void UpdateJournal(MJournal journal)
            {
                _journalRepository.Update(journal);
            }

            public void DeleteJournal(int id)
            {
                _journalRepository.Delete(id);
            }
        }
    }

}

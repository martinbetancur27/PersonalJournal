using IService;
using Models;
using Data.Interfaces;

namespace Service
{
    public class JournalService : IJournal
    {
        private readonly IJournalRepository _journalRepository;
        
        public JournalService(IJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }

        public int? AddJournal(Journal journal)
        {
                return _journalRepository.AddJournal(journal);            
        }

        public bool RemoveJournal(int idJournal)
        {
            return _journalRepository.DeleteJournal(idJournal);
        }

        public bool EditJournal(Journal journal)
        {
            return _journalRepository.EditJournal(journal);
        }

        public Journal? FindJournal(int idJournal)
        {
            return _journalRepository.GetJournal(idJournal);
        }

        public IEnumerable<Journal>? GetJournalsOfUser(string idUser)
        {
            return _journalRepository.GetJournalsOfUser(idUser);
        }

        public IEnumerable<Note>? GetNotesOfJournal(int idJournal)
        {
            return _journalRepository.GetNotesOfJournal(idJournal);
        }
    }
}
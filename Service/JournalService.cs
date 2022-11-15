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

        public int? Add(Journal journal)
        {
                return _journalRepository.Add(journal);            
        }

        public bool Remove(int idJournal)
        {
            return _journalRepository.Delete(idJournal);
        }

        public bool Edit(Journal journal)
        {
            return _journalRepository.Edit(journal);
        }

        public Journal? Find(int idJournal)
        {
            return _journalRepository.Get(idJournal);
        }

        public IEnumerable<Journal>? GetOfUser(string idUser)
        {
            return _journalRepository.GetOfUser(idUser);
        }
    }
}
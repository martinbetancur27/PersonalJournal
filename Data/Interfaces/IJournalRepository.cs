using Models;

namespace Data.Interfaces
{
    public interface IJournalRepository
    {
        public int? AddJournal(Journal journal);
        public Journal? GetJournal(int idJournal);
        public bool EditJournal(Journal journal);
        public bool DeleteJournal(int idJournal);
        public IEnumerable<Journal>? GetJournalsOfUser(string idUser);
        public IEnumerable<Note>? GetNotesOfJournal(int idJournal);
    }
}
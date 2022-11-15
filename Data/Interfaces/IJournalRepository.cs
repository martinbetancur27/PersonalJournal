using Models;

namespace Data.Interfaces
{
    public interface IJournalRepository
    {
        public int? Add(Journal journal);
        public Journal? Get(int idJournal);
        public bool Edit(Journal journal);
        public bool Delete(int idJournal);
        public IEnumerable<Journal>? GetOfUser(string idUser);
    }
}
using IService;
using Models;
using Data.Interfaces;

namespace Service
{
    public class NoteService : INote
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public int? Add(Note note)
        {
            return _noteRepository.Add(note);
        }

        public bool Remove(int idNote)
        {
            return _noteRepository.Remove(idNote);
        }

        public bool Edit(Note note)
        {
            return _noteRepository.Edit(note);
        }

        public Note? Find(int idNote)
        {
            return _noteRepository.Find(idNote);
        }

        public IEnumerable<Note>? GetOfJournal(int idJournal)
        {
            return _noteRepository.GetOfJournal(idJournal);
        }
    }
}
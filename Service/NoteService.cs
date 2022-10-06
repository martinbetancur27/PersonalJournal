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

        public int? AddNote(Note note)
        {
            return _noteRepository.AddNote(note);
        }

        public bool RemoveNote(int idNote)
        {
            return _noteRepository.RemoveNote(idNote);
        }

        public bool EditNote(Note note)
        {
            return _noteRepository.EditNote(note);
        }

        public Note? FindNote(int idNote)
        {
            return _noteRepository.FindNote(idNote);
        }

        public IEnumerable<Comment>? GetCommentsOfNote(int idNote)
        {
            return _noteRepository.GetCommentsOfNote(idNote);
        }
    }
}
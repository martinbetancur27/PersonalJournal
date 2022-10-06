﻿using Models;

namespace IService
{
    public interface INote
    {
        public int? AddNote(Note note);
        public Note? FindNote(int idNote);
        public bool EditNote(Note note);
        public bool RemoveNote(int idNote);
        public IEnumerable<Comment>? GetCommentsOfNote(int idNote);
    }
}
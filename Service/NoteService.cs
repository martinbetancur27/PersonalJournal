using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService;
using Data;
using Models;


namespace Service
{
    public class NoteService : CompositeRepository<Note>, INote
    {
        private readonly ApplicationDbContext _databaseContext;


        public NoteService(ApplicationDbContext db) : base(db)
        {
            _databaseContext = db;
        }


        public IEnumerable<Comment> GetComments(int? idNote)
        {
            return _databaseContext.Comments.Where(x => x.IdNote == idNote).OrderByDescending(d => d.CreateDate);
        }
    }
}
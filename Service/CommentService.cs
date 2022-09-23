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
    public class CommentService : LeafRepository<Comment>, IComment
    {
        
        public CommentService(ApplicationDbContext db) : base(db)
        {
        }
    }
}
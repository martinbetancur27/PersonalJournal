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
    public class CommentService : IPostLeaf
    {
        private readonly ApplicationDbContext _databaseContext;
        
        
        public CommentService(ApplicationDbContext db)
        {
            _databaseContext = db;
        }


        public bool DeletePost(int? idPost)
        {
            try
            {
                var post = _databaseContext.Comments.Find(idPost);
                if (post == null)
                {
                    return false;
                }

                _databaseContext.Comments.Remove(post);
                _databaseContext.SaveChanges();
                
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
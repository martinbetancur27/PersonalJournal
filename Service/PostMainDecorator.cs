using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService;
using Models;
using Data;

namespace Service
{
    public class PostMainDecorator : IPostMainDecorator
    {

        private readonly ApplicationDbContext _databaseContext;
        private readonly IPost _post;
        public string Title { get; set; }
        public DateTime LastEditDate { get; set; }


        public PostMainDecorator(ApplicationDbContext db)
        {
            _databaseContext = db;
        }

        public PostMainDecorator(IPost post)
        {
            this._post = post;
        }

        public bool AddSubPost(IPost subPost, int IdSourcePost)
        {
            return true;
        }

        public bool AddTitle(string title)
        {
            this.Title = title;

            return true;
        }

        public bool RemoveSubPost(int id)
        {
            return true;
        }

        public Task<IPost> EditPost(IPost subPost, int idPost)
        {
            return (Task<IPost>)subPost;
        }
    }
}

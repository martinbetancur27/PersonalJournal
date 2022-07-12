using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IPostComposite : IPost
    {
        
        /*public Task<IPost> AddSubPost(IPost post, int IdSourcePost);
        public Task<IPost> RemoveSubPost(int id);*/

        public  bool AddPost<Post>(Post post, int? idPostRoot) where Post : class;
        public Post ShowPost<Post>(int? idPost) where Post : class;
        public bool EditPost<Post>(Post post) where Post : class;
        public bool DeleteSubPosts(int? idPost);
        public IEnumerable<Post> GetListSubPosts<Post>(int? idPost) where Post : class;
        public int? GetNumberContainedPost(int? idPost);
    }
}
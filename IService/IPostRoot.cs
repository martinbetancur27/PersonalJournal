using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IPostRoot: IPostComposite
    {
        public int? Create<Post>(Post post, string idUser) where Post : class;
        public IEnumerable<Post>? GetList<Post>(string idUser) where Post : class;
    }
}

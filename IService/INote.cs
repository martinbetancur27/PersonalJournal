using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Models;

namespace IService
{
    public interface INote : ICompositeRepository<Note>
    {
        public IEnumerable<Comment> GetComments(int? idPost);
    }
}
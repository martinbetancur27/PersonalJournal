using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface ICompositeRepository<T> where T : class
    {
        public bool Add<X>(X entity) where X : class;
        public T? Get(int? id);
        public bool Edit(T entity);
        public bool Delete(int? id);
    }
}

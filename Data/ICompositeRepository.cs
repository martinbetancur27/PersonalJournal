using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface ICompositeRepository<T> where T : class
    {
        public T? Get(int? id);
        public bool Edit(T entity);
        public bool Delete(int? id);

        public bool AddSub<X>(X entity) where X : class;
        public bool EditSub<X>(X entity) where X : class;
        public bool DeleteSub<X>(int? id) where X : class;
    }
}
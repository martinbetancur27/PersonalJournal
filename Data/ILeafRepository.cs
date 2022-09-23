using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface ILeafRepository<T> where T : class
    {
        public bool Delete(int? id);
    }
}

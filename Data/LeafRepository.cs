using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class LeafRepository<T> where T : class

    {
        private readonly ApplicationDbContext _databaseContext;
        public LeafRepository(ApplicationDbContext db)
        {
            _databaseContext = db;
        }

        public bool Delete(int? id)
        {
            try
            {
                var post = _databaseContext.Set<T>().Find(id);
                if (post == null)
                {
                    return false;
                }

                _databaseContext.Set<T>().Remove(post);
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

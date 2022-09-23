using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CompositeRepository<T> : ICompositeRepository<T> where T : class
    {
        private readonly ApplicationDbContext _databaseContext;

        public CompositeRepository(ApplicationDbContext db)
        {
            _databaseContext = db;
        }

        public bool Add<X>(X post) where X : class
        {
            try
            {
                _databaseContext.Set<X>().Add(post); // Añadir IdUser como foreign key
                _databaseContext.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
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

        public bool Edit(T post)
        {
            try
            {
                _databaseContext.Set<T>().Update(post);
                _databaseContext.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public T? Get(int? id)
        {
            try
            {
                return _databaseContext.Set<T>().Find(id);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}

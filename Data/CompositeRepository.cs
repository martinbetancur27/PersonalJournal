using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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


        public bool Edit(T entity)
        {
            try
            {
                _databaseContext.Set<T>().Update(entity);
                _databaseContext.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        public bool AddSub<X>(X subEntity) where X : class
        {
            try
            {
                _databaseContext.Set<X>().Add(subEntity); // Añadir IdUser como foreign key
                _databaseContext.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        public bool DeleteSub<X>(int? id) where X : class
        {
            try
            {
                var subEntity = _databaseContext.Set<X>().Find(id);
                if (subEntity == null)
                {
                    return false;
                }

                _databaseContext.Set<X>().Remove(subEntity);
                _databaseContext.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        public bool EditSub<X>(X subEntity) where X : class
        {
            try
            {
                _databaseContext.Set<X>().Update(subEntity);
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

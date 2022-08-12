using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Generic_Repository_Example
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public T Create(T entity)
        {
            throw new NotImplementedException();
        }

        public T Delete(T entity)
        {
            DataBaseRepository.Data.Where(x => x.Age == 10);
            return entity;
        }

        public IEnumerable<T> FindAll()
        {
            return (IQueryable<T>)DataBaseRepository.Data.AsQueryable();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
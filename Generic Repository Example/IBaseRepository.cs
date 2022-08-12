using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Generic_Repository_Example
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> FindAll();

        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);

        T Create(T entity);

        T Update(T entity);

        T Delete(T entity);
    }
}
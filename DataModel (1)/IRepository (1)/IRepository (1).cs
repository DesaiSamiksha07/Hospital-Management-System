using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.IRepository
{
    public interface IRepository<T>
    {
        void Add(T entity);

        void Remove(T entity);
        public IEnumerable<T> GetAll();

        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
    }
}

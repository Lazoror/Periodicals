using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicalsFinal.DataAccess.Repository
{
    public interface IRepository<T> where T:class
    {
        void Create(T entity);
        T GetById(Guid id);
        void Update(T entity);
        void Delete(T entity);
        void Save();

        IEnumerable<T> GetAll();
    }
}

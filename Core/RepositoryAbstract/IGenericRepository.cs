using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryAbstract
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        void Add(T entity);
        void Delete(T entity);
        int Commit();
        T Get(Func<T,bool>? func=null);
        List<T> GetAll(Func<T, bool>? func = null);
    }
}

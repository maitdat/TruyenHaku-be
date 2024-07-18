using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebBusiness.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(long Id);
        IEnumerable<T> GetAll();
        Task Add (T entity);
        void Update (T entity,long id);
        void Delete (T entity);
        void SoftDelet(T entity);
    }
}

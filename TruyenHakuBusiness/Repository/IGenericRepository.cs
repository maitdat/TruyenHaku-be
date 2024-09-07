using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TruyenHakuBusiness.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id);
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        Task UpdateAsync (T entity);
        Task DeleteAsync (T entity);
        
    }
}

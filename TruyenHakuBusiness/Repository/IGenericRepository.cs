using System.Linq.Expressions;
using TruyenHakuCommon;

namespace TruyenHakuBusiness.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(long id);
        IQueryable<T> GetAll();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update (T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove (T entity);
        void RemoveRange(IEnumerable<T> entities);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}

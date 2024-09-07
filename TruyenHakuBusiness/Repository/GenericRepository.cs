using Microsoft.EntityFrameworkCore;
using TruyenHakuCommon;
using TruyenHakuModels;

namespace TruyenHakuBusiness.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity 
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            if(_dbSet == null)
                _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();    
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await GetAll().Where(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity, long id)
        {
            throw new NotImplementedException();
        }
    }
}

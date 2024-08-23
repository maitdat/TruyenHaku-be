using Microsoft.EntityFrameworkCore;
using TruyenHakuCommon;
using TruyenHakuModels;

namespace TruyenHakuBusiness.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, 
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
             _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().Where(x=>!x.IsDeleted);
        }

        public async Task<T> GetById(long Id)
        {
            return await _context.Set<T>().Where(x=>x.Id ==  Id).FirstOrDefaultAsync();
        }

        public void SoftDelet(T entity)
        {
            entity.IsDeleted = true;
            _context.SaveChanges();
        }

        public void Update(T entity, long id)
        {
             _context.Set<T>().Update(entity);
        }
    }
}

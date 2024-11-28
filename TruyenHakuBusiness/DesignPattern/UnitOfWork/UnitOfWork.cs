using TruyenHakuBusiness.DesignPattern.Repository;
using TruyenHakuCommon;
using TruyenHakuModels;

namespace TruyenHakuBusiness.DesignPattern.UnitOfWork
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly AppDbContext _context;
        private Dictionary<Type, object> _repositories;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            // Nếu đã có repository cho kiểu T, trả về repository đó
            if (_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IGenericRepository<T>;
            }

            // Nếu chưa có, tạo mới một repository cho kiểu T
            var repository = new GenericRepository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

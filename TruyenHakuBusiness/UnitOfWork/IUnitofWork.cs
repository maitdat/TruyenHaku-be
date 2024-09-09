using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuBusiness.Repository;
using TruyenHakuCommon;

namespace TruyenHakuBusiness.UnitOfWork
{
    public interface IUnitofWork : IDisposable
    {
        public IGenericRepository<T> Repository<T>() where T : BaseEntity;

        void SaveChanges();

        Task SaveChangesAsync();
    }
}

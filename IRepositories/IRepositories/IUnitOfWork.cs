using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.DAL.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGeneraicRepository<TDbEntity> Repository<TDbEntity>() where TDbEntity : class;
    }
}

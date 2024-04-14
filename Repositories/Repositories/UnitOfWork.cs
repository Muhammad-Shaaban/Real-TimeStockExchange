using RealTimeStockExchange.BAL.IBusinessServices;
using RealTimeStockExchange.DAL.Context;
using RealTimeStockExchange.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ISessionService _sessionService;
        private Dictionary<string, object> repositories;

        public UnitOfWork(ApplicationDbContext context, ISessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
            repositories = new Dictionary<string, object>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGeneraicRepository<TDbEntity> Repository<TDbEntity>() where TDbEntity : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var typeToInstantiate = typeof(GeneraicRepository<TDbEntity>).Assembly.GetExportedTypes()
            .FirstOrDefault(t => t.BaseType == typeof(GeneraicRepository<TDbEntity>)) ?? typeof(GeneraicRepository<TDbEntity>);

            var type = typeof(TDbEntity).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryInstance = Activator.CreateInstance(typeToInstantiate, this._context, this._sessionService);
                repositories.Add(type, repositoryInstance);
            }
            return (IGeneraicRepository<TDbEntity>)repositories[type];
        }

    }
}

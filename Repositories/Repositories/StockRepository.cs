using IRepositories.IRepositories;
using RealTimeStockExchange.BAL.IBusinessServices;
using RealTimeStockExchange.DAL.Context;
using RealTimeStockExchange.DAL.Entities;
using RealTimeStockExchange.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class StockRepository : GeneraicRepository<Stock>, IStockRepository
    {
        public StockRepository(ApplicationDbContext contex, ISessionService sessionService) : base(contex, sessionService)
        { 
        }
    }
}

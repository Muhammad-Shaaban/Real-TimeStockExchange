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
    public class OrderRepository : GeneraicRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context, ISessionService sessionService) : base(context, sessionService)
        {
            
        }
    }
}

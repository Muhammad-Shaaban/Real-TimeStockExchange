using RealTimeStockExchange.DAL.DTOs;
using RealTimeStockExchange.DAL.Entities;
using RealTimeStockExchange.Helpers.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.BAL.IBusinessServices
{
    public interface IOrderService : _IBusinessService<Order, OrderDTO>
    {
        DataSourceResult<OrderDTO> GetAllOrderHistoryForCurrentUser(int page, int size);
    }
}

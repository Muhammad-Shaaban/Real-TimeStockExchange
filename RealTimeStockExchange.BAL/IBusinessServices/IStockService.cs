using RealTimeStockExchange.DAL.DTOs;
using RealTimeStockExchange.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.BAL.IBusinessServices
{
    public interface IStockService : _IBusinessService<Stock, StockDTO>
    {
        StockDTO GetStockHistory(int symbolId);
    }
}

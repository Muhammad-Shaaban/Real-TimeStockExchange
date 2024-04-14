using AutoMapper;
using RealTimeStockExchange.BAL.IBusinessServices;
using RealTimeStockExchange.DAL.DTOs;
using RealTimeStockExchange.DAL.Entities;
using RealTimeStockExchange.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.BAL.BusinessServices
{
    public class StockService : _BusinessService<Stock, StockDTO>, IStockService
    {
        public StockService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            
        }

        public override StockDTO Insert(StockDTO entity)
        {
            entity.Time = DateTimeOffset.Now;
            return base.Insert(entity);
        }

        public StockDTO GetStockHistory(int symbolId)
        {
            var stock = base.GetById(symbolId);
            var result = base._Mapper.Map<StockDTO>(stock);
            return result;
        }
    }
}

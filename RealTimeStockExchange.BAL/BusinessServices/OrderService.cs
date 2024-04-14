using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using RealTimeStockExchange.BAL.IBusinessServices;
using RealTimeStockExchange.DAL.DTOs;
using RealTimeStockExchange.DAL.Entities;
using RealTimeStockExchange.DAL.IRepositories;
using RealTimeStockExchange.Helpers.Configurations;
using RealTimeStockExchange.Helpers.Enums;
using RealTimeStockExchange.Helpers.SignalR;

namespace RealTimeStockExchange.BAL.BusinessServices
{
    public class OrderService : _BusinessService<Order, OrderDTO>, IOrderService
    {
        private readonly ISessionService _sessionService;
        private readonly IHubContext<SignalRHub> _signalR;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService, IHubContext<SignalRHub> hubContext) : base(unitOfWork, mapper)
        {
            _sessionService = sessionService;
            _signalR = hubContext;
        }

        public override OrderDTO Insert(OrderDTO entity)
        {
            Order newOrder = new()
            {
                StockSymbolId = entity.StockSymbolId,
                Quantity = entity.Quantity,
                OrderType = entity.OrderTypeId
            };

            var result = _UnitOfWork.Repository<Order>().Insert(newOrder);

            Stock currntStock = _UnitOfWork.Repository<Stock>()
                .GetById(entity.StockSymbolId);

            if (entity.OrderTypeId == 1)
            {
                currntStock.CurrentPrice = (int.Parse(currntStock.CurrentPrice) + (int.Parse(currntStock.CurrentPrice) * entity.Quantity)).ToString();
            }

            else {
                currntStock.CurrentPrice = ((int.Parse(currntStock.CurrentPrice) * entity.Quantity) - int.Parse(currntStock.CurrentPrice)).ToString();
            }

            currntStock.Time = DateTime.Now;
            _UnitOfWork.Repository<Stock>().Update(currntStock);

            _signalR.Clients.All.SendAsync("UpdateStockRealTime", currntStock.Id, currntStock.CurrentPrice, currntStock.Time);

            return _Mapper.Map<OrderDTO>(result);
        }

        public DataSourceResult<OrderDTO> GetAllOrderHistoryForCurrentUser(int page, int size)
        {
            var result = _UnitOfWork.Repository<Order>()
                .GetAll()
                .Where(o => o.CreatedBy.Equals(_sessionService.UserId))
                .Take(size).Skip((page - 1) * size)
                .Select(x => new OrderDTO
                {
                    Id = x.Id,
                    OrderTypeId = (int)x.OrderType,
                    Quantity = x.Quantity,
                    StockSymbolId = x.StockSymbolId,
                    StockSymbol = x.Stock.Symbol,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy
                }).ToList();

            return new DataSourceResult<OrderDTO>() { 
                Data = result,
                Count = result.Count()
            };
        }
    }
}

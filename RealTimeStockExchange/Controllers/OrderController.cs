using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealTimeStockExchange.BAL.IBusinessServices;
using RealTimeStockExchange.DAL.DTOs;

namespace RealTimeStockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : _BaseController<Order, OrderDTO>
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) : base(orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllOrderHistoryForCurrentUser")]
        public DataSourceResult<OrderDTO> GetAllOrderHistoryForCurrentUser(int page, int size) 
            => _orderService.GetAllOrderHistoryForCurrentUser(page, size);
    }
}

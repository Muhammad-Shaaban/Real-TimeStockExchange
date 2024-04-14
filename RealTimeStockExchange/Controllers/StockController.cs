using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealTimeStockExchange.BAL.IBusinessServices;
using RealTimeStockExchange.DAL.DTOs;

namespace RealTimeStockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : _BaseController<Stock, StockDTO>
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService) : base(stockService) 
        {
            _stockService = stockService;
        }

        [HttpGet("{symbolId}/GetStockHistory")]
        public StockDTO GetStockHistory(int symbolId)
        {
            return _stockService.GetStockHistory(symbolId);
        }
    }
}

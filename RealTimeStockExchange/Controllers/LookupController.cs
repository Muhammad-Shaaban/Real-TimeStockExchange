using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealTimeStockExchange.BAL.IBusinessServices;
using RealTimeStockExchange.DAL.DTOs;
using System.Linq;

namespace RealTimeStockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookUpService;

        public LookupController(ILookupService lookupService)
        {
            _lookUpService = lookupService;
        }

        [HttpGet, Route("GetLookUp")]
        public IEnumerable<LookUpDTO> GetLookUp(string type)
        {
            return _lookUpService.GetLookUps(type);
        }
    }
}

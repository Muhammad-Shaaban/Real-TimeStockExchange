using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealTimeStockExchange.BAL.IBusinessServices;

namespace RealTimeStockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _BaseController<TDbEntity, TDetailsDTO> : ControllerBase where TDbEntity : class
    {
        private readonly _IBusinessService<TDbEntity, TDetailsDTO> _IBusinessServices;

        public _BaseController(_IBusinessService<TDbEntity, TDetailsDTO> IBusinessServices)
        {
            _IBusinessServices = IBusinessServices;
        }

        [HttpGet, Route("GetAll")]
        public DataSourceResult<TDetailsDTO> GetAll(int pageSize, int page)
        {
            return _IBusinessServices.GetAll(pageSize, page);
        }

        [HttpGet, Route("GetDetails")]
        public virtual TDetailsDTO GetDetails(object id)
        {
            return _IBusinessServices.GetById(id);
        }

        [HttpPost, Route("Insert")]
        public TDetailsDTO Insert([FromBody] TDetailsDTO entity)
        {
            return _IBusinessServices.Insert(entity);
        }

        [HttpPut, Route("Update")]
        public TDetailsDTO Update([FromBody] TDetailsDTO entity)
        {
            return _IBusinessServices.Update(entity);
        }

        [HttpDelete, Route("Delete")]
        public TDetailsDTO Delete(int Id)
        {
            return _IBusinessServices.Delete(Id);
        }
    }
}

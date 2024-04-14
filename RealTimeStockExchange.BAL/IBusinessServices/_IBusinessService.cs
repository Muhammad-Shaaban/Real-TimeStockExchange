using RealTimeStockExchange.Helpers.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.BAL.IBusinessServices
{
    public interface _IBusinessService
    {
    }

    public interface _IBusinessService<TDbEntity, TDetailsDTO> : _IBusinessService
        where TDbEntity : class
    {
        DataSourceResult<TDetailsDTO> GetAll(int pageSize, int page);
        TDetailsDTO GetById(object id);
        TDetailsDTO Insert(TDetailsDTO entity);
        TDetailsDTO Update(TDetailsDTO entity);
        TDetailsDTO Delete(object id);
    }
}

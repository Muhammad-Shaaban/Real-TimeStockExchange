using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.DAL.IRepositories
{
    public interface IGeneraicRepository
    {

    }

    public interface IGeneraicRepository<TDbEntity> : IGeneraicRepository where TDbEntity : class
    {
        IEnumerable<TDbEntity> GetAll();
        TDbEntity GetById(object id);
        TDbEntity Insert(TDbEntity entity);
        TDbEntity Update(TDbEntity entity);
        IEnumerable<TDbEntity> UpdateRange(IEnumerable<TDbEntity> entity);
        TDbEntity Delete(TDbEntity entity);
    }
}

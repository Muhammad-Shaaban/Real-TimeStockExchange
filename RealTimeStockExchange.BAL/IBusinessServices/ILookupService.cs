using RealTimeStockExchange.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.BAL.IBusinessServices
{
    public interface ILookupService
    {
        IEnumerable<LookUpDTO> GetLookUps(string type);
    }
}

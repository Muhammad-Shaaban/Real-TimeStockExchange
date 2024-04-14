using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.DAL.ModelInterfaces
{
    internal interface IAuditableUpdate
    {
        string? UpdatedBy { get; set; }
        DateTimeOffset? UpdatedOn { get; set; }
    }
}

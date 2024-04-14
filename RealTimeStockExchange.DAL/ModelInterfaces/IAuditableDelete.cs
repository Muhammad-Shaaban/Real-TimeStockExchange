using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.DAL.ModelInterfaces
{
    internal interface IAuditableDelete
    {
        string? DeletedBy { get; set; }
        DateTimeOffset? DeletedOn { get; set; }
    }
}

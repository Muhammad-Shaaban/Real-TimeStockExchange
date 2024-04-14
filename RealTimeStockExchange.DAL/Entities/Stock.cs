using RealTimeStockExchange.DAL.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.DAL.Entities
{
    public class Stock : IAuditableInsert, IAuditableUpdate
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string CurrentPrice { get; set; }
        public DateTimeOffset Time { get; set; } 

        public virtual IEnumerable<Order> Orders { get; set; }

        #region IAuditableInsert
        public string? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        #endregion
        #region IAuditableUpdate
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        #endregion
    }
}

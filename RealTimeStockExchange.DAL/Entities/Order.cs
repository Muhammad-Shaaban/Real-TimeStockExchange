using Microsoft.AspNetCore.Identity;
using RealTimeStockExchange.DAL.ModelInterfaces;
using RealTimeStockExchange.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.DAL.Entities
{
    public class Order : IAuditableInsert, IAuditableUpdate, IAuditableDelete
    {
        public int Id { get; set; }
        /// <summary>
        /// 1 = Buy
        /// 2 = Sell
        /// </summary>
        public int OrderType { get; set; }
        public long Quantity { get; set; }
        public int StockSymbolId { get; set; }

        [ForeignKey(nameof(StockSymbolId))]
        public virtual Stock Stock { get; set; }

        #region IAuditableInsert
        public string? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        #endregion
        #region IAuditableUpdate
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        #endregion
        #region IAuditableDelete
        public string? DeletedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        #endregion
    }
}

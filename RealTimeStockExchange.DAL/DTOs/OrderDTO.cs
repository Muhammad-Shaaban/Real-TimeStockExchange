using RealTimeStockExchange.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.DAL.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int OrderTypeId { get; set; }
        public long Quantity { get; set; }
        public int StockSymbolId { get; set; }
        public string StockSymbol { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}

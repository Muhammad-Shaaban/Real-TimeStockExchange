using RealTimeStockExchange.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.DAL.DTOs
{
    public class StockDTO
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string CurrentPrice { get; set; }
        public DateTimeOffset Time { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.DAL.DTOs
{
    public class AuthDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public bool IsAuthenticated { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}

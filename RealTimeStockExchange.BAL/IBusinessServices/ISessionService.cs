using Microsoft.AspNetCore.Http;

namespace RealTimeStockExchange.BAL.IBusinessServices
{
    public interface ISessionService
    {
        HttpContext HttpContext { get; set; }
        string? UserId { get; }
        string? UserName { get; }
    }
}

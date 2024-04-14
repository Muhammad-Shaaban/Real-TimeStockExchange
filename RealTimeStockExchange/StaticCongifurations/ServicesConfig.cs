using RealTimeStockExchange.BAL.IBusinessServices;
using RealTimeStockExchange.BAL.BusinessServices;

namespace RealTimeStockExchange.StaticCongifurations
{
    public static class ServicesConfig
    {
        public static void AddBusinessServcies(this IServiceCollection services)
        {
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILookupService, LookUpService>();
        }

    }
}

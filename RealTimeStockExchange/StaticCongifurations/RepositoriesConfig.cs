using IRepositories.IRepositories;
using Repositories.Repositories;

namespace RealTimeStockExchange.StaticCongifurations
{
    public static class RepositoriesConfig
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            
        }
    }
}

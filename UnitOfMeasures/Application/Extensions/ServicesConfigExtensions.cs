using UnitOfMeasures.Infrastructure.Persistents.Extensions;

namespace UnitOfMeasures.Application.Extensions
{
    public static class ServicesConfigExtensions
    {
        public static IServiceCollection AddServiceConfigs(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddApplicationDbContext(configuration.GetConnectionString("MeasureDBConnectionString"));

            return services;
        }
    }
}

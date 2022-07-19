namespace UnitOfMeasures.Application.Contracts.Extensions
{
    public static class ContractConfigExtension
    {
        public static IServiceCollection AddContractsConfig(this IServiceCollection services)
        {
            services.AddScoped<ICalculateService, CalculateService>();
            return services;
        }
    }
}

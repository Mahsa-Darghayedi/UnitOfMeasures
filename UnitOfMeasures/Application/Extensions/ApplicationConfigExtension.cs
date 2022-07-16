namespace UnitOfMeasures.Application.Extensions
{
    public static class ApplicationConfigExtension
    {
        public static IApplicationBuilder UseApplicationConfig(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            return app;
        }
    }
}

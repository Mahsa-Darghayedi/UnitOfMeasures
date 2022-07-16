using UnitOfMeasures.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceConfigs(builder.Configuration);

var app = builder.Build();
app.UseApplicationConfig(app.Environment);
app.MapControllers();
app.Run();

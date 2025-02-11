
using eCommerceeCommerceClean.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ApplyMigrations();
    await app.SeedDataAsync();   
}
app.UsePresentation();
app.UseExceptionHandler();
app.UseSerilogRequestLogging();
app.MapHealthChecks("health", new HealthCheckOptions 
{ 
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse 
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

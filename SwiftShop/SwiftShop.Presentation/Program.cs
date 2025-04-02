using SwiftShop.Application.Commons.Data;
using SwiftShop.Infrastructure.DependencyInjection;
using SwiftShop.Infrastructure.Persistence;
using SwiftShop.Presentation.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddPresentation(builder.Configuration)
    .AddInfrastructure(builder.Configuration, builder.Environment.EnvironmentName);

builder.Services.AddScoped<ISwiftShopDbContext, SwiftShopDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UsePresentation();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

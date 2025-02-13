using eCommerce.API.Extensions;
using eCommerce.API.Helpers;
using eCommerce.API.Middleware;
using eCommerce.Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StoreContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("StoreDb")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

//app.Use(async (context, next) =>
//{
//    if (context.Request.Path.StartsWithSegments("/swagger") ||
//        context.Request.Path.StartsWithSegments("/swagger/index.html") ||
//        context.Request.Path.StartsWithSegments("/swagger/v1/swagger.json"))
//    {
//        await next();
//        return;
//    }
//    await next();
//});
//app.UseStatusCodePagesWithReExecute("/errors/{0}", "?path={1}");
app.UseStatusCodePagesWithReExecute("/errors/{0}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ApplyMigrations();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

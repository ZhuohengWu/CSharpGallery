using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setting up db connection
builder.Services.AddDbContextPool<StaffTrackDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StaffTrackAppConnection")
        ?? throw new InvalidOperationException("Database connection string is not found"))
);

builder.Services.AddScoped<IUserAccount, UserAccountRepository>();

// Register JWT
builder.Services.Configure<JwtSection>(builder.Configuration.GetSection(nameof(JwtSection)));


var app = builder.Build();

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

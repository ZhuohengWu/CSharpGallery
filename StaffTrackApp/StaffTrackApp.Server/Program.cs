using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register JWT
builder.Services.Configure<JwtSection>(builder.Configuration.GetSection(nameof(JwtSection)));
var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

// Setting up db connection
builder.Services.AddDbContextPool<StaffTrackDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StaffTrackAppConnection")
        ?? throw new InvalidOperationException("Database connection string is not found"))
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSection!.Issuer,
        ValidAudience = jwtSection.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!))
    };
});

builder.Services.AddScoped<IUserAccount, UserAccountRepository>();
builder.Services.AddScoped<IGenericRepository<GeneralDepartment>, GeneralDepartmentRepository>();
builder.Services.AddScoped<IGenericRepository<Department>, DepartmentRepository>();
builder.Services.AddScoped<IGenericRepository<Branch>, BranchRepository>();

builder.Services.AddScoped<IGenericRepository<Country>, CountryRepository>();
builder.Services.AddScoped<IGenericRepository<City>, CityRepository>();
builder.Services.AddScoped<IGenericRepository<Town>, TownRepository>();

builder.Services.AddScoped<IGenericRepository<Overtime>, OvertimeRepository>();
builder.Services.AddScoped<IGenericRepository<OvertimeType>, OvertimeTypeRepository>();
builder.Services.AddScoped<IGenericRepository<Sanction>, SanctionRepository>();
builder.Services.AddScoped<IGenericRepository<SanctionType>, SanctionTypeRepository>();
builder.Services.AddScoped<IGenericRepository<Vacation>, VacationRepository>();
builder.Services.AddScoped<IGenericRepository<VacationType>, VacationTypeRepository>();

builder.Services.AddScoped<IGenericRepository<Doctor>, DoctorRepository>();

builder.Services.AddScoped<IGenericRepository<Employee>, EmployeeRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        builder => builder.WithOrigins("https://localhost:7299", "http://localhost:5298")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        );
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Lifetime.ApplicationStopping.Register(() =>
{
    Console.WriteLine("Application is stopping...");
});

app.UseHttpsRedirection();

app.UseCors("AllowBlazorWasm");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

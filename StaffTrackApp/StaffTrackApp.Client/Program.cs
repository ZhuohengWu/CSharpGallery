using BaseLibrary.Entities;
using Blazored.LocalStorage;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StaffTrackApp.Client;
using StaffTrackApp.Client.ApplicationStates;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpHandler>();
builder.Services.AddHttpClient("SystemApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7100");
}).AddHttpMessageHandler<CustomHttpHandler>();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7100") });
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IGenericService<Employee>, GenericService<Employee>>();

builder.Services.AddScoped<IGenericService<GeneralDepartment>, GenericService<GeneralDepartment>>();
builder.Services.AddScoped<IGenericService<Department>, GenericService<Department>>();
builder.Services.AddScoped<IGenericService<Branch>, GenericService<Branch>>();

builder.Services.AddScoped<IGenericService<Country>, GenericService<Country>>();
builder.Services.AddScoped<IGenericService<City>, GenericService<City>>();
builder.Services.AddScoped<IGenericService<Town>, GenericService<Town>>();

builder.Services.AddScoped<IGenericService<Sanction>, GenericService<Sanction>>();
builder.Services.AddScoped<IGenericService<SanctionType>, GenericService<SanctionType>>();
builder.Services.AddScoped<IGenericService<Vacation>, GenericService<Vacation>>();
builder.Services.AddScoped<IGenericService<VacationType>, GenericService<VacationType>>();
builder.Services.AddScoped<IGenericService<Overtime>, GenericService<Overtime>>();
builder.Services.AddScoped<IGenericService<OvertimeType>, GenericService<OvertimeType>>();

builder.Services.AddScoped<IGenericService<Doctor>, GenericService<Doctor>>();


builder.Services.AddScoped<AllState>();
builder.Services.AddScoped<UserProfileState>();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX5cd3RSRWVeWEdxWko=");
builder.Services.AddScoped<SfDialogService>();
builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();

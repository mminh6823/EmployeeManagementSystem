using Frontend_EmployeeManagementSystem;
using FrontendLibrary.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using FrontendLibrary.Services.Contracts;
using FrontendLibrary.Services.Implementations;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;
using Frontend_EmployeeManagementSystem.ApplicationStates;
using Syncfusion.Licensing;
using BaseLibrary.Entities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Đăng ký các thành phần chính của ứng dụng
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Đăng ký các dịch vụ HTTP, như HttpClient
builder.Services.AddTransient<CustomHttpHandler>();
builder.Services.AddHttpClient("SystemApiClient", client =>
{
    client.BaseAddress = new Uri("https://backendemployee-e6f3gjhndtgfctcs.eastasia-01.azurewebsites.net/"); //URL API 
    /*client.BaseAddress = new Uri("https://localhost:7083/"); */
}).AddHttpMessageHandler<CustomHttpHandler>();

// Đăng ký giấy phép Syncfusion
SyncfusionLicenseProvider.RegisterLicense("MzcxNDg0NEAzMjM4MmUzMDJlMzBZSWVwemJnMzhyaHhJdEZsdGg2U0pCSnR2UnBpd214c0tsUjR1YlU5S3k0PQ==");

// Đăng ký dịch vụ Syncfusion
builder.Services.AddSyncfusionBlazor();

// Đăng ký các dịch vụ còn lại
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();

//GeneralDepartment, Department, Branch
builder.Services.AddScoped<IGenericServiceInterface<GeneralDepartment>, GenericServiceImplementation<GeneralDepartment>>();
builder.Services.AddScoped<IGenericServiceInterface<Department>, GenericServiceImplementation<Department>>();
builder.Services.AddScoped<IGenericServiceInterface<Branch>, GenericServiceImplementation<Branch>>();

//Country, City
builder.Services.AddScoped<IGenericServiceInterface<Country>, GenericServiceImplementation<Country>>();
builder.Services.AddScoped<IGenericServiceInterface<City>, GenericServiceImplementation<City>>();

builder.Services.AddScoped<IGenericServiceInterface<Doctor>, GenericServiceImplementation<Doctor>>();

builder.Services.AddScoped<IGenericServiceInterface<Overtime>, GenericServiceImplementation<Overtime>>();
builder.Services.AddScoped<IGenericServiceInterface<OvertimeType>, GenericServiceImplementation<OvertimeType>>();

builder.Services.AddScoped<IGenericServiceInterface<Sanction>, GenericServiceImplementation<Sanction>>();
builder.Services.AddScoped<IGenericServiceInterface<SanctionType>, GenericServiceImplementation<SanctionType>>();

builder.Services.AddScoped<IGenericServiceInterface<Vacation>, GenericServiceImplementation<Vacation>>();
builder.Services.AddScoped<IGenericServiceInterface<VacationType>, GenericServiceImplementation<VacationType>>();

//Employee
builder.Services.AddScoped<IGenericServiceInterface<Employee>, GenericServiceImplementation<Employee>>();

builder.Services.AddScoped<AllStates>();
builder.Services.AddScoped<UserProfileState>();
//SingalR
/*builder.Services.AddSingleton<EmployeeHubService>();*/

// Đăng ký Syncfusion Dialog Service nếu bạn muốn sử dụng Dialog
builder.Services.AddScoped<SfDialogService>();

await builder.Build().RunAsync();

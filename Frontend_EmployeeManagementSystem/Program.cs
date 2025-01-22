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
    client.BaseAddress = new Uri("https://localhost:7083/"); // Đảm bảo đây là URL API của bạn
}).AddHttpMessageHandler<CustomHttpHandler>();

// Đăng ký giấy phép Syncfusion
SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCfEx3THxbf1x1ZFREalxYTnVbUiweQnxTdEBjWH1ecHVWT2BbV0d0Vw==");

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

//Employee
builder.Services.AddScoped<IGenericServiceInterface<Employee>, GenericServiceImplementation<Employee>>();

builder.Services.AddScoped<AllStates>();

// Đăng ký Syncfusion Dialog Service nếu bạn muốn sử dụng Dialog
builder.Services.AddScoped<SfDialogService>();

await builder.Build().RunAsync();

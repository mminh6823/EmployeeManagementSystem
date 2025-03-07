using Backend_Library.Data;
using Backend_Library.Helpers;
using Backend_Library.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Backend_Library.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BaseLibrary.Entities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EmployeeService API",
        Version = "v1"
    });
});

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Xin lỗi, kết nối của bạn không thành công")));


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
        ValidIssuer = jwtSection!.Issuer,
        ValidAudience = jwtSection!.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!))
    };
});





builder.Services.AddScoped<IUserAccount, UserAccountRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<GeneralDepartment>, GeneralDepartmentRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Department>, DepartmentRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Branch>, BranchRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Country>, CountryRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<City>, CityRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Overtime>, OvertimeRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<OvertimeType>, OvertimeTypeRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Sanction>, SanctionRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<SanctionType>, SanctionTypeRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Vacation>, VacationRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<VacationType>, VacationTypeRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Doctor>, DoctorRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Employee>, EmployeeRepository>();
/*builder.Services.AddSignalR();*/ // Thêm SignalR service

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins(
            "https://frontendemployee-agb8c7g5hwcyg7ag.eastasia-01.azurewebsites.net",
             "https://localhost:7142"
                ) // Thay bằng URL Blazor trên Azure
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
// Hiển thị Swagger khi chạy ứng dụng
// Luôn hiển thị Swagger, kể cả trên Production
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
    c.RoutePrefix = string.Empty;
    /*c.RoutePrefix = "swagger";*/ // Đặt Swagger làm trang chủ
});


/*app.MapHub<EmployeeHub>("/employeeHub");*/

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

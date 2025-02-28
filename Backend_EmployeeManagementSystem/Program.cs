using Backend_Library.Data;
using Backend_Library.Helpers;
using Backend_Library.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Backend_Library.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// ✅ 1. Load cấu hình JWT để tránh bị null
var jwtConfig = new JwtSection();
builder.Configuration.GetSection("JwtSection").Bind(jwtConfig);
if (jwtConfig == null || string.IsNullOrEmpty(jwtConfig.Key))
{
    throw new InvalidOperationException("Lỗi: JwtSection không được load đúng từ appsettings.json");
}

// ✅ 2. Đăng ký dịch vụ
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ 3. Cấu hình kết nối SQL Server trên Azure
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Kết nối Database không thành công")));

// ✅ 4. Cấu hình Authentication + JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key!))
    };
});

// ✅ 5. Cấu hình CORS để hỗ trợ Blazor WebAssembly trên Azure
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        policy => policy.WithOrigins(
                "https://your-frontend.azurestaticapps.net",  // Thay bằng domain frontend Azure
                "https://employeewebapi-c0b7h5eqa2hrdehd.eastasia-01.azurewebsites.net")        // Thay bằng domain API trên Azure
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

// ✅ 6. Đăng ký Repository (Dependency Injection)
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

// ✅ 7. Hỗ trợ Header Forwarding (fix lỗi Azure)
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

// ✅ 8. Bật Swagger trên cả môi trường Development và Production
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ 9. Sử dụng Header Forwarding
app.UseForwardedHeaders();

// ✅ 10. Cấu hình Middleware
app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

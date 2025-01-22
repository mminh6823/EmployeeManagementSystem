
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_Library.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor chính xác
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        
        public DbSet<Employee> Employees { get; set; }

        //GeneralDepartment, Department, Branch
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }


        //Towns, City, Country
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        //Authentication, Role, System Roles
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; } 
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }

        //Other, Vacation, Sanction, Doctor, Overtime
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }
        public DbSet<OvertimeType> OvertimeTypes { get; set; }
        public DbSet<Sanction> Sanctions { get; set; }
        public DbSet<SanctionType> SanctionTypes { get; set; }
        public DbSet<Doctor> Doctors { get; set; }  
    
    }
}

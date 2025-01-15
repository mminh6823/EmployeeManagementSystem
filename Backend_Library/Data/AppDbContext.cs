
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

        // Các DbSet được khai báo ở đây
        public DbSet<Employee> Employees { get; set; }
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}

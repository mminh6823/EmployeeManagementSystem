using Backend_Library.Data;
using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend_Library.Repositories.Implementations
{
    public class EmployeeRepository(AppDbContext appDbContext/*, IHubContext<EmployeeHub> hubContext*/) : IGenericRepositoryInterface<Employee>
    {
    /*    private readonly IHubContext<EmployeeHub> _hubContext = hubContext;*/

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Employees.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.Employees.Remove(item);
            await Commit();

  
           /* await _hubContext.Clients.All.SendAsync("EmployeeDeleted", id);*/

            return Success();
        }

        public async Task<List<Employee>> GetAll()
        {
            return await appDbContext.Employees
                .AsNoTracking()
                .Include(a => a.City)
                .ThenInclude(b => b.Country)
                .Include(m => m.Branch)
                .ThenInclude(n => n.Department)
                .ThenInclude(h => h.GeneralDepartment)
                .ToListAsync();
        }

        public Task<Employee> GetById(int id)
        {
            return appDbContext.Employees
                .Include(a => a.City)
                .ThenInclude(b => b.Country)
                .Include(m => m.Branch)
                .ThenInclude(n => n.Department)
                .ThenInclude(h => h.GeneralDepartment)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<GeneralResponse> Insert(Employee item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Xin lỗi! Tên đã tồn tại");

            await appDbContext.Employees.AddAsync(item);
            await Commit();

           
            /*await _hubContext.Clients.All.SendAsync("EmployeeAdded", item);*/

            return Success();
        }

        public async Task<GeneralResponse> Update(Employee item)
        {
            var findUser = await appDbContext.Employees.FindAsync(item.Id);
            if (findUser is null) return new GeneralResponse(false, "Nhân viên không tồn tại!");

            findUser.Name = item.Name;
            findUser.Other = item.Other;
            findUser.Address = item.Address;
            findUser.TelephoneNumber = item.TelephoneNumber;
            findUser.BranchId = item.BranchId;
            findUser.CityId = item.CityId;
            findUser.CivilId = item.CivilId;
            findUser.FileNumber = item.FileNumber;
            findUser.Jobname = item.Jobname;
            findUser.Photo = item.Photo;

            await appDbContext.SaveChangesAsync();
            await Commit();

       
            /*await _hubContext.Clients.All.SendAsync("EmployeeUpdated", item);*/

            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy dữ liệu");
        private static GeneralResponse Success() => new(true, "Thao tác thành công!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Employees.FirstOrDefaultAsync(m => m.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}

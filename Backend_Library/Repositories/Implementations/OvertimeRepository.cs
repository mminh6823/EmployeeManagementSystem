using Backend_Library.Data;
using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Library.Repositories.Implementations
{
    public class OvertimeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Overtime>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Overtimes.FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (dep is null) return NotFound();
            appDbContext.Overtimes.Remove(dep!);
            await Commit();
            return Success();
        }

        public async Task<List<Overtime>> GetAll() => await appDbContext.Overtimes.ToListAsync();

        public async Task<Overtime> GetById(int id) => await appDbContext.Overtimes.FirstOrDefaultAsync(m => m.EmployeeId == id);

        public async Task<GeneralResponse> Insert(Overtime item)
        {
            appDbContext.Overtimes.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Overtime item)
        {
            var obj = await appDbContext.Overtimes.FirstOrDefaultAsync(m => m.EmployeeId == item.EmployeeId);
            if (obj is null) return NotFound();
            obj.StarDate = item.StarDate;
            obj.EndDate = item.EndDate;
            obj.OvertimeTypeId = item.OvertimeTypeId;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy dữ liệu");
        private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}

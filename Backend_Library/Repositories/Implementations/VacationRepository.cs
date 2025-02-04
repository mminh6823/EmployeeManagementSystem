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
    public class VacationRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Vacation>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var vac = await appDbContext.Vacations.FirstOrDefaultAsync(i=>i.EmployeeId == id);
            if (vac is null) return NotFound();
            appDbContext.Vacations.Remove(vac);
            await Commit();
            return Success();
        }

        public async Task<List<Vacation>> GetAll() => await appDbContext.Vacations.ToListAsync();

        public async Task<Vacation> GetById(int id) => await appDbContext.Vacations.FirstOrDefaultAsync(i => i.EmployeeId == id);

        public async Task<GeneralResponse> Insert(Vacation item)
        {
            await appDbContext.Vacations.AddAsync(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Vacation item)
        {
                var abc = await appDbContext.Vacations.FirstOrDefaultAsync(i => i.EmployeeId == item.EmployeeId);
            if (abc is null) return NotFound();
            abc.StartDate = item.StartDate;
            abc.NumberOfDays = item.NumberOfDays;
            abc.VacationTypeId = item.VacationTypeId;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy dữ liệu");
        private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}

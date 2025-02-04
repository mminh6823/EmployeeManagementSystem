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
    public class SanctionRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Sanction>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var minh = await appDbContext.Sanctions.FindAsync(id);
            if (minh is null) return NotFound();
            appDbContext.Sanctions.Remove(minh);
            await Commit();
            return Success();
        }

        public async Task<List<Sanction>> GetAll() => await appDbContext.Sanctions.ToListAsync();

        public async Task<Sanction> GetById(int id) => await appDbContext.Sanctions.FirstOrDefaultAsync(m => m.Id == id);


        public async Task<GeneralResponse> Insert(Sanction item)
        {
           var minhdz = await appDbContext.Sanctions.FirstOrDefaultAsync(m => m.Id == item.Id);
            if (minhdz is not null) return NotFound();
            await appDbContext.Sanctions.AddAsync(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Sanction item)
        {
            var minh = await appDbContext.Sanctions.FirstOrDefaultAsync(m => m.EmployeeId == item.EmployeeId);
            if (minh is null) return NotFound();
            minh.Punishment = item.Punishment;
            minh.Date = item.Date;
            minh.PunishmentDate = item.PunishmentDate;
            minh.SanctionType = item.SanctionType;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy dữ liệu");
        private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}

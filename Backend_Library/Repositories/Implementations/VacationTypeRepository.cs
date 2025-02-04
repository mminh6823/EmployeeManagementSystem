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
    public class VacationTypeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<VacationType>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.VacationTypes.FindAsync(id);
            if (item is null) return NotFound();
            appDbContext.VacationTypes.Remove(item);
            await Commit();
            return Success();
        }
           

        public async Task<List<VacationType>> GetAll()=> await appDbContext.VacationTypes.ToListAsync();

        public async Task<VacationType> GetById(int id) => await appDbContext.VacationTypes.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<GeneralResponse> Insert(VacationType item)
        {
            if (!await CheckName(item.Name!))
            {
                return new GeneralResponse(false, "Kiểu nghỉ phép đã tồn tại");
            }
            await appDbContext.VacationTypes.AddAsync(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(VacationType item)
        {
            var minhdzvai = await appDbContext.VacationTypes.FirstOrDefaultAsync(m => m.Id == item.Id);
            if (minhdzvai is null) return NotFound();
            minhdzvai.Name = item.Name;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy dữ liệu");
        private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.SanctionTypes.FirstOrDefaultAsync(m => m.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}

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
    public class SanctionTypeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<SanctionType>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var minhdz = await appDbContext.SanctionTypes.FindAsync(id);
            if (minhdz is null) return NotFound();
            appDbContext.SanctionTypes.Remove(minhdz);
            await Commit();
            return Success();
        }

        public async Task<List<SanctionType>> GetAll() => await appDbContext.SanctionTypes.ToListAsync();


        public async Task<SanctionType> GetById(int id) => await appDbContext.SanctionTypes.FirstOrDefaultAsync(m => m.Id == id);


        public async Task<GeneralResponse> Insert(SanctionType item)
        {
            if (!await CheckName(item.Name!))
            {
                return new GeneralResponse(false, "Kiểu phạt đã tồn tại");
            }
            await appDbContext.SanctionTypes.AddAsync(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(SanctionType item)
        {
           var minhdz = await appDbContext.SanctionTypes.FirstOrDefaultAsync(m => m.Id == item.Id);
            if (minhdz is null) return NotFound();
            minhdz.Name = item.Name;
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

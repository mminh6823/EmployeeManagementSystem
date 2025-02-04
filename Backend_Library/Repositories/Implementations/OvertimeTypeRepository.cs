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
    public class OvertimeTypeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<OvertimeType>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
             var minh = await appDbContext.OvertimeTypes.FindAsync(id);
            if (minh is null) return NotFound();
            appDbContext.OvertimeTypes.Remove(minh);
            await Commit();
            return Success();
        }

        public async Task<List<OvertimeType>> GetAll() => await appDbContext.OvertimeTypes.ToListAsync();

        public async Task<OvertimeType> GetById(int id) => await appDbContext.OvertimeTypes.FirstOrDefaultAsync(m => m.Id == id);


        public async Task<GeneralResponse> Insert(OvertimeType item)
        {
            if (!await CheckName(item.Name!))
            {
                return new GeneralResponse(false, "Kiểu tăng ca đã tồn tại");
            }
            await appDbContext.OvertimeTypes.AddAsync(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(OvertimeType item)
        {
           var minh = await appDbContext.OvertimeTypes.FirstOrDefaultAsync(m => m.Id == item.Id);
            if (minh is null) return NotFound();
            minh.Name = item.Name;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy dữ liệu");
        private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.OvertimeTypes.FirstOrDefaultAsync(m => m.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}

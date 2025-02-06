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
            // Kiểm tra xem id hình phạt đã tồn tại chưa
            if (!await CheckId(item.Id!)) return new GeneralResponse(false, "Hình phạt đã tồn tại!");

            // Kiểm tra xem SanctionTypeId có hợp lệ không (phải tồn tại trong cơ sở dữ liệu)
            var sanctionType = await appDbContext.SanctionTypes.AsNoTracking().FirstOrDefaultAsync(g => g.Id == item.SanctionTypeId);
            if (sanctionType == null)
            {
                return new GeneralResponse(false, "Kiểu hình phạt không tồn tại.");
            }

            // Chỉ cần thiết lập SanctionTypeId mà không cần đính kèm lại đối tượng SanctionType
            item.SanctionType = null;  // Đảm bảo không đính kèm lại đối tượng VacationType

            // Thêm mới phòng ban vào cơ sở dữ liệu
            appDbContext.Sanctions.Add(item);
            await Commit(); // Lưu thay đổi vào cơ sở dữ liệu
            return Success(); // Trả về kết quả thành công
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
        private async Task<bool> CheckId(int id)
        {
            var item = await appDbContext.Sanctions
                .FirstOrDefaultAsync(m => m.Id == id);
            return item is null;
        }
    }
}

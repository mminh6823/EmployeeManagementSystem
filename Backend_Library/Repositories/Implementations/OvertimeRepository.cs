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
            // Kiểm tra xem id nghỉ phép đã tồn tại chưa
            if (!await CheckId(item.Id!)) return new GeneralResponse(false, "Lịch làm thêm đã tồn tại!");

            // Kiểm tra xem VacationTypeId có hợp lệ không (phải tồn tại trong cơ sở dữ liệu)
            var overtimeType = await appDbContext.OvertimeTypes.AsNoTracking().FirstOrDefaultAsync(g => g.Id == item.OvertimeTypeId);
            if (overtimeType == null)
            {
                return new GeneralResponse(false, "Kiểu làm thêm không tồn tại.");
            }

            // Chỉ cần thiết lập VacationTypeId mà không cần đính kèm lại đối tượng VacationType
            item.OvertimeType = null;  // Đảm bảo không đính kèm lại đối tượng VacationType

            // Thêm mới phòng ban vào cơ sở dữ liệu
            appDbContext.Overtimes.Add(item);
            await Commit(); // Lưu thay đổi vào cơ sở dữ liệu
            return Success(); // Trả về kết quả thành công
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
        private async Task<bool> CheckId(int id)
        {
            var item = await appDbContext.Overtimes
                .FirstOrDefaultAsync(m => m.Id == id);
            return item is null;
        }
    }
}

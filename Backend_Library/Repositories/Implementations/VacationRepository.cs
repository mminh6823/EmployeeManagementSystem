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
            // Kiểm tra xem id nghỉ phép đã tồn tại chưa
            if (!await CheckId(item.Id!)) return new GeneralResponse(false, "Lịch nghỉ phép đã tồn tại!");

            // Kiểm tra xem VacationTypeId có hợp lệ không (phải tồn tại trong cơ sở dữ liệu)
            var vacationType = await appDbContext.VacationTypes.AsNoTracking().FirstOrDefaultAsync(g => g.Id == item.VacationTypeId);
            if (vacationType == null)
            {
                return new GeneralResponse(false, "Kiểu nghỉ phép không tồn tại.");
            }

            // Chỉ cần thiết lập VacationTypeId mà không cần đính kèm lại đối tượng VacationType
            item.VacationType = null;  // Đảm bảo không đính kèm lại đối tượng VacationType

            // Thêm mới phòng ban vào cơ sở dữ liệu
            appDbContext.Vacations.Add(item);
            await Commit(); // Lưu thay đổi vào cơ sở dữ liệu
            return Success(); // Trả về kết quả thành công
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
        private async Task<bool> CheckId(int id)
        {
            var item = await appDbContext.Vacations
                .FirstOrDefaultAsync(m => m.Id == id);
            return item is null;
        }
    }
}

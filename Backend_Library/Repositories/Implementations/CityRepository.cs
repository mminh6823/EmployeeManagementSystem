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
    public class CityRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<City> 
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Cities.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.Cities.Remove(dep!);
            await Commit();
            return Success();
        }
        public async Task<List<City>> GetAll() => await appDbContext
            .Cities
                .AsNoTracking()
                .Include(m => m.Country)
                .ToListAsync();

        public async Task<City> GetById(int id) => await appDbContext.Cities.FirstOrDefaultAsync(m => m.Id == id);
        public async Task<GeneralResponse> Insert(City item)
        {
            // Kiểm tra xem tên thành phố đã tồn tại chưa
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Thành phố đã có sẵn!");

            // Kiểm tra xem CountryId có hợp lệ không (phải tồn tại trong cơ sở dữ liệu)
            var generalDepartment = await appDbContext.Countries.AsNoTracking().FirstOrDefaultAsync(g => g.Id == item.CountryId);
            if (generalDepartment == null)
            {
                return new GeneralResponse(false, "Quốc gia không tồn tại.");
            }

            // Chỉ cần thiết lập CountryId mà không cần đính kèm lại đối tượng Country
            item.Country = null;  // Đảm bảo không đính kèm lại đối tượng Country

            // Thêm mới phòng ban vào cơ sở dữ liệu
            appDbContext.Cities.Add(item);
            await Commit(); // Lưu thay đổi vào cơ sở dữ liệu
            return Success(); // Trả về kết quả thành công
        }

        public async Task<GeneralResponse> Update(City item)
        {
            // Tìm đối tượng trong database
            var dep = await appDbContext.Cities.FindAsync(item.Id);
            if (dep is null) return NotFound();

            // Cập nhật thông tin
            dep.Name = item.Name;
            dep.CountryId = item.CountryId; // Cập nhật GeneralDepartmentId

            // Lưu thay đổi
            await Commit();

            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy dữ liệu");
        private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Cities.FirstOrDefaultAsync(m => m.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}

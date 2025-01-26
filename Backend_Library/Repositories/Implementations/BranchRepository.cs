 using Backend_Library.Data;
using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;


namespace Backend_Library.Repositories.Implementations
{
    public class BranchRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Branch>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Branches.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.Branches.Remove(dep!);
            await Commit();
            return Success();
        }
        public async Task<List<Branch>> GetAll() => await appDbContext
            .Branches
            .AsNoTracking()
            .Include(m => m.Department)
            .ToListAsync();

        public async Task<Branch> GetById(int id) => await appDbContext.Branches.FirstOrDefaultAsync(m => m.Id == id);
        public async Task<GeneralResponse> Insert(Branch item)
        {
            // Kiểm tra xem tên phòng bộ phận đã tồn tại chưa
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Chi nhánh đã có sẵn!");

            // Kiểm tra xem DepartmentId có hợp lệ không (phải tồn tại trong cơ sở dữ liệu)
            var department = await appDbContext.Departments.AsNoTracking().FirstOrDefaultAsync(g => g.Id == item.DepartmentId);
            if (department == null)
            {
                return new GeneralResponse(false, "Phòng bộ phận không tồn tại.");
            }

            // Chỉ cần thiết lập DepartmentId mà không cần đính kèm lại đối tượng Department
            item.Department = null;  // Đảm bảo không đính kèm lại đối tượng Department

            // Thêm mới phòng ban vào cơ sở dữ liệu
            appDbContext.Branches.Add(item);
            await Commit(); // Lưu thay đổi vào cơ sở dữ liệu
            return Success(); // Trả về kết quả thành công
        }

        public async Task<GeneralResponse> Update(Branch item)
        {
            // Tìm đối tượng trong database
            var dep = await appDbContext.Branches.FindAsync(item.Id);
            if (dep is null) return NotFound();

            // Cập nhật thông tin
            dep.Name = item.Name;
            dep.DepartmentId = item.DepartmentId; // Cập nhật GeneralDepartmentId

            // Lưu thay đổi
            await Commit();

            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy chi nhánh");
        private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Branches.FirstOrDefaultAsync(m => m.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

    }

}


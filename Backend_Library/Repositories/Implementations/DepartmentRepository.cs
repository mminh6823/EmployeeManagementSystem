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
        public class DepartmentRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Department>
        {
            public async Task<GeneralResponse> DeleteById(int id)
            {
                var dep = await appDbContext.Departments.FindAsync(id);
                if (dep is null) return NotFound();
                appDbContext.Departments.Remove(dep!);
                await Commit();
                return Success();
            }
            public async Task<List<Department>> GetAll() => await appDbContext
                .Departments
                .AsNoTracking()
                .Include(m=>m.GeneralDepartment)
                .ToListAsync();

            public async Task<Department> GetById(int id) => await appDbContext.Departments.FirstOrDefaultAsync(m => m.Id == id);
        public async Task<GeneralResponse> Insert(Department item)
        {
            // Kiểm tra xem tên phòng ban đã tồn tại chưa
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Phòng ban đã có sẵn!");

            // Kiểm tra xem GeneralDepartmentId có hợp lệ không (phải tồn tại trong cơ sở dữ liệu)
            var generalDepartment = await appDbContext.GeneralDepartments.AsNoTracking().FirstOrDefaultAsync(g => g.Id == item.GeneralDepartmentId);
            if (generalDepartment == null)
            {
                return new GeneralResponse(false, "Phòng ban tổng hợp không tồn tại.");
            }

            // Chỉ cần thiết lập GeneralDepartmentId mà không cần đính kèm lại đối tượng GeneralDepartment
            item.GeneralDepartment = null;  // Đảm bảo không đính kèm lại đối tượng GeneralDepartment

            // Thêm mới phòng ban vào cơ sở dữ liệu
            appDbContext.Departments.Add(item);
            await Commit(); // Lưu thay đổi vào cơ sở dữ liệu
            return Success(); // Trả về kết quả thành công
        }



        public async Task<GeneralResponse> Update(Department item)
            {
                // Tìm đối tượng trong database
                var dep = await appDbContext.Departments.FindAsync(item.Id);
                if (dep is null) return NotFound();

                // Cập nhật thông tin
                dep.Name = item.Name;
                dep.GeneralDepartmentId = item.GeneralDepartmentId; // Cập nhật GeneralDepartmentId

                // Lưu thay đổi
                await Commit();

                return Success();
            }

            private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy dữ liệu");
            private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit()
        {
            try
            {
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log lỗi (nếu sử dụng log system như Serilog hoặc NLog)
                Console.WriteLine($"Lỗi xảy ra trong quá trình lưu cơ sở dữ liệu: {ex.Message}");
                throw;
            }
        }


        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Departments
                .FirstOrDefaultAsync(m => m.Name!.ToLower() == name.ToLower());
            return item is null;
        }


    }
}

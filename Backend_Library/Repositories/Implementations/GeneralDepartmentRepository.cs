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
    public class GeneralDepartmentRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<GeneralDepartment>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.GeneralDepartments.FirstOrDefaultAsync(m => m.Id == id);
            if (dep is null) return NotFound();
            appDbContext.GeneralDepartments.Remove(dep);
            await Commit();
            return Success();

        }

        public  async Task<List<GeneralDepartment>> GetAll() => await appDbContext.GeneralDepartments.ToListAsync();

        public async Task<GeneralDepartment> GetById(int id) => await appDbContext.GeneralDepartments.FirstOrDefaultAsync(m => m.Id == id);


        public async Task<GeneralResponse> Insert(GeneralDepartment item)
        {
            // Kiểm tra xem tên phòng ban tổng hợp đã có trong cơ sở dữ liệu chưa
            var checkIfNull = await CheckName(item.Name!);
            if (!checkIfNull) return new GeneralResponse(false, "Phòng ban tổng hợp đã có sẵn!");

            // Thêm mới phòng ban tổng hợp vào cơ sở dữ liệu
            appDbContext.GeneralDepartments.Add(item);
            await Commit(); // Lưu thay đổi vào cơ sở dữ liệu
            return Success(); // Trả về kết quả thành công
        }


        public async Task<GeneralResponse> Update(GeneralDepartment item)
        {
            var dep = await appDbContext.GeneralDepartments.FindAsync(item.Id);
            if (dep is null) return NotFound(); 
            dep.Name= item.Name;
            await Commit();
            return Success();  
        }
        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy dữ liệu");
        private static GeneralResponse Success() => new(true, "Thao tác thành công!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();
    
    private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.GeneralDepartments.FirstOrDefaultAsync(m=>m.Name!.ToLower().Equals(name.ToLower()));
            return item is null ;
        }
    
    }
}

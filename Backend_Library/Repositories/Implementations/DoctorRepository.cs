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
    public class DoctorRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Doctor>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Doctors.FirstOrDefaultAsync(m=>m.EmployeeId == id);
            if (dep is null) return NotFound();
            appDbContext.Doctors.Remove(dep!);
            await Commit();
            return Success();
        }
       
        public async Task<List<Doctor>> GetAll() => await  appDbContext.Doctors.AsNoTracking().ToListAsync();

        public async Task<Doctor> GetById(int id) => await appDbContext.Doctors.FirstOrDefaultAsync(m => m.EmployeeId == id);

        public async Task<GeneralResponse> Insert(Doctor item)
        {
           appDbContext.Doctors.Add(item);
             await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Doctor item)
        {
            var obj = await appDbContext.Doctors.FirstOrDefaultAsync(m => m.EmployeeId == item.EmployeeId);
            if (obj is null) return NotFound();
            obj.MedicalRecommendation = item.MedicalRecommendation;
            obj.MedicalDiagnose = item.MedicalDiagnose;
            obj.Date = item.Date;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy dữ liệu");
        private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

    }
}

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
        public async Task<List<City>> GetAll() => await appDbContext.Cities.ToListAsync();

        public async Task<City> GetById(int id) => await appDbContext.Cities.FirstOrDefaultAsync(m => m.Id == id);
        public async Task<GeneralResponse> Insert(City item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Thành phố đã có sẵn!");
            appDbContext.Cities.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(City item)
        {
            var dep = await appDbContext.Cities.FindAsync(item.Id);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy thành phố");
        private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Cities.FirstOrDefaultAsync(m => m.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}

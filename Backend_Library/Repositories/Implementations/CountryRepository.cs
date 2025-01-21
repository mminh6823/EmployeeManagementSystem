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
    public class CountryRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Country>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Countries.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.Countries.Remove(dep!);
            await Commit();
            return Success();
        }
        public async Task<List<Country>> GetAll() => await appDbContext.Countries.ToListAsync();

        public async Task<Country> GetById(int id) => await appDbContext.Countries.FirstOrDefaultAsync(m => m.Id == id);
        public async Task<GeneralResponse> Insert(Country item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Quốc gia đã có sẵn!");
            appDbContext.Countries.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Country item)
        {
            var dep = await appDbContext.Countries.FindAsync(item.Id);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy chi nhánh");
        private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Countries.FirstOrDefaultAsync(m => m.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

    }
}

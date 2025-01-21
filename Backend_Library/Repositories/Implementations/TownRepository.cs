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
    public class TownRepository (AppDbContext appDbContext) : IGenericRepositoryInterface<Town>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Towns.FindAsync(id);
            if (dep is null) return NotFound();
            appDbContext.Towns.Remove(dep!);
            await Commit();
            return Success();
        }
        public async Task<List<Town>> GetAll() => await appDbContext.Towns.ToListAsync();

        public async Task<Town> GetById(int id) => await appDbContext.Towns.FirstOrDefaultAsync(m => m.Id == id);
        public async Task<GeneralResponse> Insert(Town item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Thị trấn đã có sẵn!");
            appDbContext.Towns.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Town item)
        {
            var dep = await appDbContext.Towns.FindAsync(item.Id);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            await Commit();
            return Success();
        }
        private static GeneralResponse NotFound() => new(false, "Xin lỗi! Không tìm thấy thị trấn");
        private static GeneralResponse Success() => new(true, "Quá trình hoàn tất!");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Towns.FirstOrDefaultAsync(m => m.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}

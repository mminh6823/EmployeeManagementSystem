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
        public async Task<List<Branch>> GetAll() => await appDbContext.Branches.ToListAsync();

        public async Task<Branch> GetById(int id) => await appDbContext.Branches.FirstOrDefaultAsync(m => m.Id == id);
        public async Task<GeneralResponse> Insert(Branch item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Chi nhánh đã có sẵn!");
            appDbContext.Branches.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Branch item)
        {
            var dep = await appDbContext.Branches.FindAsync(item.Id);
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
            var item = await appDbContext.Branches.FirstOrDefaultAsync(m => m.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

    }

}


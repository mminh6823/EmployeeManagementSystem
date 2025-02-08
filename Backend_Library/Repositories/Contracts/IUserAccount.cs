using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
namespace Backend_Library.Repositories.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAsync(Register user);
        Task<LoginResponse> SingInAsync(Login user);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
        Task<List<ManageUser>> GetUsers();
        Task<GeneralResponse> UpdateUser(ManageUser user);
        Task<List<SystemRole>> GetRoles();
        Task<GeneralResponse> DeleteUser(int id);
        Task<string> GetUserImage(int id);
        Task<bool> UpdateProfile(UserProfile profile);

    }
}

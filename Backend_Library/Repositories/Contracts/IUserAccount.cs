using BaseLibrary.DTOs;
using BaseLibrary.Responses;
namespace Backend_Library.Repositories.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAsync(Register user);
        Task<LoginResponse> SingInAsync(Login user);
    }
}




using BaseLibrary.DTOs;
using BaseLibrary.Responses;

namespace FrontendLibrary.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<GeneralResponse> CreatAsync(Register user);
        Task<LoginResponse> LoginAsync(Login user);
        Task<LoginResponse> RefreshToken(RefreshToken token);

    }
}

using BaseLibrary.DTOs;
using BaseLibrary.Responses;
using FrontendLibrary.Helpers;
using FrontendLibrary.Services.Contracts;
using System.Net.Http.Json;


namespace FrontendLibrary.Services.Implementations
{
    public class UserAccountService(GetHttpClient getHttpClient) : IUserAccountService
    {
        public const string AuthUrl = "api/authentication";
        public async Task<GeneralResponse> CreatAsync(Register user)
        {
           var httpClient =  getHttpClient.GetPublicHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
            if(!response.IsSuccessStatusCode) return new GeneralResponse(false, "Tạo tài khoản thất bại");
            return await response.Content.ReadFromJsonAsync<GeneralResponse>();
        }


        public async Task<LoginResponse> LoginAsync(Login user)
        {
            var httpClient = getHttpClient.GetPublicHttpClient();   
            var response = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);  
            if (!response.IsSuccessStatusCode) return new LoginResponse(false, "Login thất bại");
            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public async Task<LoginResponse> RefreshToken(RefreshToken token)
        {   
            throw new NotImplementedException();
        }



    }
}

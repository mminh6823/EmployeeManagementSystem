using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using FrontendLibrary.Helpers;
using FrontendLibrary.Services.Contracts;
using Microsoft.AspNetCore.Components;
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
           var httpClient = getHttpClient.GetPublicHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{AuthUrl}/refresh-token", token);
            if (!response.IsSuccessStatusCode) return new LoginResponse(false, "Refresh token thất bại");
            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }


        public async Task<List<ManageUser>> GetUsers()
        {
           var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.GetFromJsonAsync<List<ManageUser>>($"{AuthUrl}/users");
            return response!;
        }

        public async Task<GeneralResponse> UpdateUser(ManageUser user)
        {
           var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"{AuthUrl}/update-user", user);
            if (!response.IsSuccessStatusCode) return new GeneralResponse(false, "Cập nhật tài khoản thất bại");
            return await response.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }

        public async Task<List<SystemRole>> GetRoles()
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.GetFromJsonAsync<List<SystemRole>>($"{AuthUrl}/roles");
            return response!;

        }

        public async Task<GeneralResponse> DeleteUser(int id)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response =await httpClient.DeleteAsync($"{AuthUrl}/delete-user/{id}");
            if (!response.IsSuccessStatusCode) return new GeneralResponse(false, "Xóa tài khoản thất bại");
            return await response.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }
    }
}

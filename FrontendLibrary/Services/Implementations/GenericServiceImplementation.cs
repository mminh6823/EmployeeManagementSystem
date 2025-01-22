using BaseLibrary.Responses;
using FrontendLibrary.Helpers;
using FrontendLibrary.Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FrontendLibrary.Services.Implementations
{
    public class GenericServiceImplementation<T>(GetHttpClient getHttpClient) : IGenericServiceInterface<T>
    {
        //Xóa theo Id
        public async Task<GeneralResponse> DeleteById(int id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var respone = await httpClient.DeleteAsync($"{baseUrl}/delete/{id}");
            var result = await respone.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        //Hiển thị tất cả
        public async Task<List<T>> GetAll(string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            return await httpClient.GetFromJsonAsync<List<T>>($"{baseUrl}/all");
        }

        //Hiển thị theo id
        public async Task<T> GetById(int id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<T>($"{baseUrl}/single/{id}");
            return result!;
        }

        public async Task<GeneralResponse> Insert(T item, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var respone = await httpClient.PostAsJsonAsync($"{baseUrl}/add", item);
            var result = await respone.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        //Cập nhật model
        public async Task<GeneralResponse> Update(T item, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var respone = await httpClient.PutAsJsonAsync($"{baseUrl}/update", item);
            var result = await respone.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
    }
}

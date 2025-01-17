using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace FrontendLibrary.Helpers
{
    public class LocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;
        private const string StorageKey = "authentication-token";

        // Constructor nhận ILocalStorageService qua Dependency Injection
        public LocalStorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        // Lấy token từ Local Storage
        public async Task<string> GetToken() => await _localStorageService.GetItemAsStringAsync(StorageKey);

        // Lưu token vào Local Storage
        public async Task SetToken(string item) => await _localStorageService.SetItemAsStringAsync(StorageKey, item);

        // Xóa token khỏi Local Storage
        public async Task RemoveToken() => await _localStorageService.RemoveItemAsync(StorageKey);
    }
}


using System.Net;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using BaseLibrary.DTOs;
using FrontendLibrary.Services.Contracts;
namespace FrontendLibrary.Helpers
{
    public class CustomHttpHandler : DelegatingHandler
    {
        public GetHttpClient GetHttpClient { get; }
        public LocalStorageService LocalStorageService { get; set; }

        public IUserAccountService accountService { get; set; }
        public CustomHttpHandler(GetHttpClient getHttpClient, LocalStorageService localStorageService, IUserAccountService accountService)
        {
            GetHttpClient = getHttpClient;
            LocalStorageService = localStorageService;
            this.accountService = accountService;
        }


        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool loginUrl = request.RequestUri.AbsoluteUri.Contains("login");
            bool registerUrl = request.RequestUri.AbsoluteUri.Contains("register");
            bool refreshTokenUrl = request.RequestUri.AbsoluteUri.Contains("refresh-token");

            if (loginUrl || registerUrl || refreshTokenUrl) return await base.SendAsync(request, cancellationToken);

            var result = await base.SendAsync(request, cancellationToken);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                //Lấy Token từ localStorage
                var stringToken = await LocalStorageService.GetToken();
                if (string.IsNullOrEmpty(stringToken)) return result;
                //Kiểm tra nếu header chứa token 
                string token = string.Empty;
                try
                {
                    token = request.Headers.Authorization!.Parameter!;
                }
                catch { }

                var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);


                if (string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", deserializeToken.Token!);
                    return await base.SendAsync(request, cancellationToken);
                }
                //Gọi đến RefreshToken
                var newJwtToken = await GetRefreshToken(deserializeToken.RefreshToken);
                if (string.IsNullOrEmpty(newJwtToken)) return result;
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newJwtToken!);
                return await base.SendAsync(request, cancellationToken);
            }
            return result;
        }

        private async Task<string> GetRefreshToken(string? refreshToken)
        {
            var result = await accountService.RefreshToken(new RefreshToken(){ Token = refreshToken });
                string serializeToken = Serializations.SerializeObj(new UserSession()
                {
                    Token = result.Token,
                    RefreshToken = result.RefreshToken
                });
            await LocalStorageService.SetToken(serializeToken);
            return result.Token;
        }
    }
}

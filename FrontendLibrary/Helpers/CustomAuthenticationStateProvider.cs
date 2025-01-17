
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BaseLibrary.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace FrontendLibrary.Helpers
{
    public class CustomAuthenticationStateProvider(LocalStorageService localStorageService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity()); 
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
           var stringToken = await localStorageService.GetToken();
            if (string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(anonymous));

            var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
            if (deserializeToken == null) return await Task.FromResult(new AuthenticationState(anonymous));

            var getUserClamis = DecryptToken(deserializeToken.Token!);
            if (getUserClamis == null) return await Task.FromResult(new AuthenticationState(anonymous));    

            var clamisPrincipal = SetClaminsPrincipal(getUserClamis);
            return await Task.FromResult(new AuthenticationState(clamisPrincipal));
        }

        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (userSession.Token != null || userSession.RefreshToken != null)
            {
                var serializeToken = Serializations.SerializeObj(userSession);
                await localStorageService.SetToken(serializeToken);
                var getUserClamis = DecryptToken(userSession.Token!);
                claimsPrincipal = SetClaminsPrincipal(getUserClamis);
            }
            else
            {
                await localStorageService.RemoveToken();
            }
           NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal))); 
        }






        private static CustomUserClaims? DecryptToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return new CustomUserClaims();
            var handler = new JwtSecurityTokenHandler();
            var tokens = handler.ReadJwtToken(token);
            var userId = tokens.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userName = tokens.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var email = tokens.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);  
            var role = tokens.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);    
            return new CustomUserClaims(userId!.Value, userName!.Value, email!.Value, role!.Value);
        }
        private static ClaimsPrincipal SetClaminsPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, claims.Id),
                new Claim(ClaimTypes.Name, claims.Name),
                new Claim(ClaimTypes.Email, claims.Email),
                new Claim(ClaimTypes.Role, claims.Role)
            }, "JwtAuth"));


          
        }



    }
}

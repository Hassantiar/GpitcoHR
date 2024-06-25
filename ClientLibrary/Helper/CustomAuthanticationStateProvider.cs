using Base.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClientLibrary.Helper
{
    public class CustomAuthanticationStateProvider(LocalStorgService localStorgService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var stringToken = await localStorgService.GetToken();
            if (string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(anonymous));

            var deseriaizedtoken = Serialization.Deserializjsonsrting<UserSession>(stringToken);
            if(deseriaizedtoken == null)return await Task.FromResult(new AuthenticationState(anonymous));

            var getUserClaim = DecryptToken(deseriaizedtoken.Token!);
            if (getUserClaim == null) return await Task.FromResult(new AuthenticationState(anonymous));

            var claimsPrancipal = SetCleimsPrincipal(getUserClaim);
            return await Task.FromResult(new AuthenticationState(claimsPrancipal));
        }
        public static CustomUserClaims DecryptToken(string jwtToken)
        {
            if(string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();
            var handler = new JwtSecurityTokenHandler();
            var token =handler.ReadJwtToken(jwtToken);
            var userid = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var name = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(x=>x.Type== ClaimTypes.Email);
            var role=token.Claims.FirstOrDefault(x=>x.Type== ClaimTypes.Role);
            return new CustomUserClaims(userid!.Value!,name!.Value,email!.Value,role!.Value);
        }
        public static ClaimsPrincipal SetCleimsPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,claims.id!),
                new(ClaimTypes.Name,claims.name!),
                new(ClaimTypes.Email,claims.Email!),
                new(ClaimTypes.Role,claims.Role) 
            },"JwtAuth"));
        }
        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            var claimPrincepal = new ClaimsPrincipal();
            if(userSession.Token != null || userSession.RefreshToken != null)
            //if(string.(userSession.Token)||string.IsNullOrEmpty(userSession.RefreshToken))
            {
                var serializeSession = Serialization.Serializeobj(userSession);
                await localStorgService.SetToken(serializeSession);
                var getuserClaims= DecryptToken(userSession.Token);
                claimPrincepal = SetCleimsPrincipal(getuserClaims);
            }
            else
            {
                await localStorgService.RemoveToken();
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimPrincepal)));
        }
    }
}

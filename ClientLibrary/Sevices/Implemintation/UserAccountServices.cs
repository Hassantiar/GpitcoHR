using Base.DTOs;
using Base.Responses;
using ClientLibrary.Helper;
using ClientLibrary.Sevices.Contact;
using System.Net.Http.Json;


namespace ClientLibrary.Sevices.Implemintation
{
    public class UserAccountServices(GetHttpClient getHttpclient) : IUserAccountServices
    {
        public const string AuthUrl = "https://localhost:7125/api/Authantication";
        public async Task<GeneralRespons> CreateAsync(Reguster user)
        {
            var httpclien = getHttpclient.GetPublicHttpClient();
            var result = await httpclien.PostAsJsonAsync($"{AuthUrl}/requst",user);
            if(!result.IsSuccessStatusCode)return new GeneralRespons(false,"Error occured");

            return await result.Content.ReadFromJsonAsync<GeneralRespons>()!;
        }
        public async Task<LoginRespons> SigninAsync(Login user)
        {
            var httpclien = getHttpclient.GetPublicHttpClient();
            var result = await httpclien.PostAsJsonAsync($"{AuthUrl}/Login", user);
            if (!result.IsSuccessStatusCode) return new LoginRespons(false, "Error occured");

            return await result.Content.ReadFromJsonAsync<LoginRespons>()!;
        }
        public async Task<LoginRespons> RefreshTokenAsync(RefreshToken token)
        {
            var httpclien = getHttpclient.GetPublicHttpClient();
            var result = await httpclien.PostAsJsonAsync($"{AuthUrl}/refresh-Token", token);
            if (!result.IsSuccessStatusCode) return new LoginRespons(false, "Error occured");

            return await result.Content.ReadFromJsonAsync<LoginRespons>()!;
        }
        public async Task<WeatherForecast[]> GetWitherForceCast()
        {
            var httpclien =await getHttpclient.GetPrivateHttpClient();
            var result = await httpclien.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7125/api/WeatherForecast/Freezing");
            return result;
        }
    }
}

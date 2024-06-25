using Base.DTOs;
using Base.Responses;


namespace ClientLibrary.Sevices.Contact
{
    public interface IUserAccountServices
    {
        Task<GeneralRespons> CreateAsync(Reguster user);
        Task<LoginRespons> SigninAsync(Login user);
        Task<LoginRespons> RefreshTokenAsync(RefreshToken token);
        Task<WeatherForecast[]> GetWitherForceCast();
    }
}

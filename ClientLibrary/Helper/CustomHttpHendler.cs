using Base.DTOs;
using ClientLibrary.Sevices.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Helper
{
    public class CustomHttpHendler(GetHttpClient getHttpClient,LocalStorgService localstorgservice,IUserAccountServices accountServices) :DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool loginurl = request.RequestUri.AbsolutePath.Contains("Login");
            bool regesterurl = request.RequestUri.AbsolutePath.Contains("requst");
            bool refreshtokenurl = request.RequestUri.AbsolutePath.Contains("refresh-Token");
           if(loginurl||regesterurl||refreshtokenurl) return await base.SendAsync(request, cancellationToken);

           var result =await base.SendAsync(request,cancellationToken);
            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //get token from localstorge 
                var stringtoken = await localstorgservice.GetToken();
                if (stringtoken == null) return result;

                //check if header contian token
                string token = string.Empty;
                try
                {
                    token = request.Headers.Authorization!.Parameter!;
                }
                catch 
                {

                   
                }
                var desrilaizertoken = Serialization.Deserializjsonsrting<UserSession>(stringtoken);
                if (string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",desrilaizertoken.Token);
                    return await base.SendAsync(request, cancellationToken);
                }
                //call refresh token 
                var newtoken = await GetRefreshToken(desrilaizertoken.RefreshToken);
                if(string.IsNullOrEmpty(newtoken)) return result;
                request.Headers.Authorization=new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",newtoken);
                return await base.SendAsync(request,cancellationToken);
            }
            return result;
        }

        private async Task<string> GetRefreshToken(string refreshToken)
        {
            var result = await accountServices.RefreshTokenAsync(new RefreshToken() { Token = refreshToken });
            string serilizetoke = Serialization.Serializeobj(new UserSession() { Token=result.Token,RefreshToken=result.RefrashToken});
            await localstorgservice.SetToken(serilizetoke);
            return result.Token;
        }


    }
}

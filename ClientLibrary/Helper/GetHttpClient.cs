using Base.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace ClientLibrary.Helper
{
    public class GetHttpClient(IHttpClientFactory httpClientFactory,LocalStorgService localStorgService)
    {
        private const string HeaderKey = "Authorization";
        public async Task <HttpClient> GetPrivateHttpClient()
        {
            var client = httpClientFactory.CreateClient("SystemApiClient");
            var stringtoken = await localStorgService.GetToken();
            if (string.IsNullOrEmpty(stringtoken)) return client;

            var deserialized = Serialization.Deserializjsonsrting<UserSession>(stringtoken);
            if(deserialized == null) return client;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",deserialized.Token);
            return client;
        }
        public HttpClient GetPublicHttpClient()
        {
            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Remove(HeaderKey);
            return client;
        }
    }
}

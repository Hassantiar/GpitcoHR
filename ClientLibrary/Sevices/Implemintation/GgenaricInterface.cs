using Base.Responses;
using ClientLibrary.Helper;
using ClientLibrary.Sevices.Contact;
using System.Net.Http.Json;

namespace ClientLibrary.Sevices.Implemintation
{
    public class GgenaricInterface<T>(GetHttpClient gethttpClient) : IGgenaricInterface<T>
    {
        //Create
        public async Task<GeneralRespons> Insert(T item, string BaseUrl)
        {
            var http = await gethttpClient.GetPrivateHttpClient();
            var respons = await http.PostAsJsonAsync($"{BaseUrl}/add", item);
            var result = await respons.Content.ReadFromJsonAsync<GeneralRespons>();
            return result!;
        }
        //read all
        public async Task<List<T>> GetAll(string BaseUrl)
        {
            var http = await gethttpClient.GetPrivateHttpClient();
            var result= await http.GetFromJsonAsync<List<T>>($"{BaseUrl}/all");
            return result!;
        }
        //read
        public async Task<T>GetById(int id, string BaseUrl)
        {
            var http= await gethttpClient.GetPrivateHttpClient();
            var result= await http.GetFromJsonAsync<T>($"{BaseUrl}/Single/{id}");
            return result!;
        }
        //update
        public async Task<GeneralRespons>Update(T item, string BaseUrl)
        {
            var http =await gethttpClient.GetPrivateHttpClient();
            var respons = await http.PutAsJsonAsync($"{BaseUrl}/update", item);
            var result = await respons.Content.ReadFromJsonAsync<GeneralRespons>();
            return result!;
        }
        //delete
        public async Task<GeneralRespons> Delete(int id, string BaseUrl)
        {
            var http = await gethttpClient.GetPrivateHttpClient();
            var respons = await http.DeleteAsync($"{BaseUrl}/Delete/{id}");
            var result = await respons.Content.ReadFromJsonAsync<GeneralRespons>();
            return result!;
        }
    }
}

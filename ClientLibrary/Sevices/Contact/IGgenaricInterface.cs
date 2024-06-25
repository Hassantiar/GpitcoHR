using Base.Responses;

namespace ClientLibrary.Sevices.Contact
{
    public interface IGgenaricInterface<T>
    {
        Task<List<T>> GetAll(string BaseUrl);
        Task<T> GetById(int Id, string BaseUrl);
        Task<GeneralRespons>Delete(int Id, string BaseUrl);
        Task<GeneralRespons>Insert(T item,string BaseUrl);
        Task<GeneralRespons>Update(T item,string BaseUrl);
    }
}

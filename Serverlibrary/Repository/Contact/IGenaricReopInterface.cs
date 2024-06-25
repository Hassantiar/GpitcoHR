

using Base.Responses;

namespace Serverlibrary.Repository.Contact
{
    public interface IGenaricReopInterface<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<GeneralRespons> Insert(T item);
        Task<GeneralRespons> Update(T item);
        Task<GeneralRespons> DeleteById(int id);
    }
}

namespace CoffeeBlog.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CoffeeBlog.Data.Models;

    public interface ITagsService
    {
        T GetById<T>(int id);

        Task<IEnumerable<Tag>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}

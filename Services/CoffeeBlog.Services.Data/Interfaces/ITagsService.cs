namespace CoffeeBlog.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CoffeeBlog.Data.Models;

    public interface ITagsService
    {
        Tag GetById(int id);

        T GetById<T>(int id);

        Task<IEnumerable<Tag>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task AddTag(string title);

        Task Edit(Tag tag);

        Task Delete(Tag tag);
    }
}

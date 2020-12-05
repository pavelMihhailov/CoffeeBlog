namespace CoffeeBlog.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CoffeeBlog.Data.Models;

    public interface IPostsService
    {
        T GetById<T>(int id);

        Task<IEnumerable<Post>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task AddProduct(
            string title,
            string content,
            string previewImagePath,
            IEnumerable<int> tagIds);

        Task Edit(Post post, IEnumerable<int> selectedTags);

        Task<IEnumerable<int>> GetPostRelatedTagIds(int postId);

        Task<IEnumerable<Post>> GetAllPostsWithTag(int tagId);
    }
}

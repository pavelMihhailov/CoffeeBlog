namespace CoffeeBlog.Web.ViewModels.Posts
{
    using CoffeeBlog.Data.Models;
    using CoffeeBlog.Services.Mapping;

    public class PostViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        public string PreviewImagePath { get; set; }

        public string Content { get; set; }
    }
}

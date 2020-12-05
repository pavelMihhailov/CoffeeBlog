namespace CoffeeBlog.Web.ViewModels.Posts
{
    using System;

    using CoffeeBlog.Data.Models;
    using CoffeeBlog.Services.Mapping;

    public class PostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PreviewImagePath { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

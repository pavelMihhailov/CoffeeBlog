namespace CoffeeBlog.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class CreatePostInputModel
    {
        public string Title { get; set; }

        public string PreviewImagePath { get; set; }

        public string Content { get; set; }

        public IEnumerable<int> Tags { get; set; }
    }
}

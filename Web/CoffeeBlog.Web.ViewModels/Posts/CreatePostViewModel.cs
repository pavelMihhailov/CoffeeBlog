namespace CoffeeBlog.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CoffeeBlog.Web.ViewModels.Administration.Tags;

    public class CreatePostViewModel
    {
        [Required]
        public string Title { get; set; }

        public string PreviewImagePath { get; set; }

        public string Content { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}

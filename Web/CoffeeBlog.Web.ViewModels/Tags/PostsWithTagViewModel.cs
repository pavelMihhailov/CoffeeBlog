namespace CoffeeBlog.Web.ViewModels.Tags
{
    using System.Collections.Generic;

    using CoffeeBlog.Data.Models;
    using CoffeeBlog.Web.ViewModels.Administration.Tags;

    public class PostsWithTagViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        public int SelectedTagId { get; set; }

        public IEnumerable<TagViewModel> AllTags { get; set; }
    }
}

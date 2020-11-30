namespace CoffeeBlog.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    using CoffeeBlog.Web.ViewModels.Administration.Tags;

    public class PostDetailsViewModel
    {
        public PostViewModel Post { get; set; }

        public IEnumerable<TagViewModel> RelatedTags { get; set; }
    }
}

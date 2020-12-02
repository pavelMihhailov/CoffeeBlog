namespace CoffeeBlog.Web.ViewModels.Tags
{
    using System.Collections.Generic;

    using CoffeeBlog.Data.Models;

    public class PostsWithTagViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        public string TagTitle { get; set; }
    }
}

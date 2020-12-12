namespace CoffeeBlog.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using CoffeeBlog.Data.Models;

    public class HomeViewModel
    {
        public Post BigPost { get; set; }

        public Post MediumPost { get; set; }

        public ICollection<Post> SmallPosts { get; set; }
    }
}

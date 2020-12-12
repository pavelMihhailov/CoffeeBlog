namespace CoffeeBlog.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using CoffeeBlog.Data.Common.Repositories;
    using CoffeeBlog.Data.Models;
    using CoffeeBlog.Web.ViewModels;
    using CoffeeBlog.Web.ViewModels.Home;
    using CoffeeBlog.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public HomeController(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public IActionResult Index()
        {
            var latestPosts = this.postsRepository.All().OrderByDescending(x => x.CreatedOn).ToList();

            var viewModel = new HomeViewModel
            {
                BigPost = latestPosts.Take(1).FirstOrDefault(),
                MediumPost = latestPosts.Skip(1).Take(1).FirstOrDefault(),
                SmallPosts = latestPosts.Skip(2).ToList(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

namespace CoffeeBlog.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using CoffeeBlog.Data.Common.Repositories;
    using CoffeeBlog.Data.Models;
    using CoffeeBlog.Web.ViewModels;

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
            var latestPosts = this.postsRepository.All().OrderByDescending(x => x.CreatedOn).Take(4);

            return this.View(latestPosts);
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

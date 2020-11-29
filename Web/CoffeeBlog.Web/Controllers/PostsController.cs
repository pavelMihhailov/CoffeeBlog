namespace CoffeeBlog.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CoffeeBlog.Data;
    using CoffeeBlog.Data.Common.Repositories;
    using CoffeeBlog.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class PostsController : Controller
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsController(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public IActionResult Index()
        {
            var allPosts = this.postsRepository.All();

            return this.View(allPosts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await this.postsRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
            {
                return this.NotFound();
            }

            return this.View(post);
        }
    }
}
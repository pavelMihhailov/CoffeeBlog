namespace CoffeeBlog.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CoffeeBlog.Services.Data.Interfaces;
    using CoffeeBlog.Web.ViewModels.Administration.Tags;
    using CoffeeBlog.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly ITagsService tagsService;

        public PostsController(IPostsService postsService, ITagsService tagsService)
        {
            this.postsService = postsService;
            this.tagsService = tagsService;
        }

        public async Task<IActionResult> Index()
        {
            var allPosts = await this.postsService.GetAllAsync();

            return this.View(allPosts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = this.postsService.GetById<PostViewModel>(id);

            if (post == null)
            {
                return this.NotFound();
            }

            var relatedTagIds = await this.postsService.GetPostRelatedTagIds(id);
            var allTags = await this.tagsService
                .GetAllAsync<TagViewModel>();

            var viewModel = new PostDetailsViewModel
            {
                Post = post,
                RelatedTags = allTags.Where(x => relatedTagIds.Contains(x.Id)),
            };

            return this.View(viewModel);
        }
    }
}

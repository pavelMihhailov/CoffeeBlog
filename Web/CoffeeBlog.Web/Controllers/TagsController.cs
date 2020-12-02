namespace CoffeeBlog.Web.Controllers
{
    using System.Threading.Tasks;

    using CoffeeBlog.Services.Data.Interfaces;
    using CoffeeBlog.Web.ViewModels.Administration.Tags;
    using CoffeeBlog.Web.ViewModels.Tags;
    using Microsoft.AspNetCore.Mvc;

    public class TagsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly ITagsService tagsService;

        public TagsController(IPostsService postsService, ITagsService tagsService)
        {
            this.postsService = postsService;
            this.tagsService = tagsService;
        }

        public async Task<IActionResult> Index(int id)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var postsRelated = await this.postsService.GetAllPostsWithTag(id);
            var tag = this.tagsService.GetById<TagViewModel>(id);

            var viewModel = new PostsWithTagViewModel
            {
                Posts = postsRelated,
                TagTitle = tag.Title,
            };

            return this.View(viewModel);
        }
    }
}
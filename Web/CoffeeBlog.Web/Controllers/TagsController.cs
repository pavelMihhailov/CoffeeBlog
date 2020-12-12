namespace CoffeeBlog.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CoffeeBlog.Data.Models;
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

        public async Task<IActionResult> Index(int id = 0)
        {
            if (id < 0)
            {
                return this.NotFound();
            }

            IEnumerable<Post> postsRelated;

            if (id == 0)
            {
                postsRelated = await this.postsService.GetAllAsync();
            }
            else
            {
                postsRelated = await this.postsService.GetAllPostsWithTag(id);
            }

            var allTags = await this.tagsService.GetAllAsync<TagViewModel>();

            var viewModel = new PostsWithTagViewModel
            {
                Posts = postsRelated,
                SelectedTagId = id,
                AllTags = allTags,
            };

            return this.View(viewModel);
        }
    }
}
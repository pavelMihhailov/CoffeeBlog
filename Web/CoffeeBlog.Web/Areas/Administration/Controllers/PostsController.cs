namespace CoffeeBlog.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CoffeeBlog.Data.Models;
    using CoffeeBlog.Services.Data.Interfaces;
    using CoffeeBlog.Web.ViewModels.Administration.Tags;
    using CoffeeBlog.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PostsController : AdministrationController
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
            return this.View(await this.postsService.GetAllAsync<PostViewModel>());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var post = this.postsService.GetById(id.Value);

            if (post == null)
            {
                return this.NotFound();
            }

            return this.View(post);
        }

        public async Task<IActionResult> Create()
        {
            var tags = await this.tagsService.GetAllAsync<TagViewModel>();

            var viewModel = new CreatePostViewModel
            {
                Tags = tags,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.postsService.AddProduct(
                    inputModel.Title,
                    inputModel.Content,
                    inputModel.PreviewImagePath,
                    inputModel.Tags);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = this.postsService.GetById<EditPostViewModel>(id);

            if (string.IsNullOrWhiteSpace(viewModel.Title))
            {
                return this.NotFound();
            }

            var allTags = await this.tagsService.GetAllAsync<TagViewModel>();
            var selectedTags = await this.postsService.GetPostRelatedTagIds(id);

            viewModel.Tags = allTags.Select(x =>
                                        new SelectListItem
                                        {
                                            Text = x.Title,
                                            Value = x.Id.ToString(),
                                        });

            viewModel.SelectedTags = selectedTags;

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Title,CreatedOn,PreviewImagePath,Content,Id")] Post post,
            IEnumerable<int> selectedTags)
        {
            if (id != post.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.postsService.Edit(post, selectedTags);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(post);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var post = this.postsService.GetById(id.Value);

            if (post == null)
            {
                return this.NotFound();
            }

            return this.View(post);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = this.postsService.GetById(id);

            await this.postsService.Delete(post);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

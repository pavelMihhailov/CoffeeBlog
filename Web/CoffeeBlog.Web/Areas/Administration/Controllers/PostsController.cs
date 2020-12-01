namespace CoffeeBlog.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CoffeeBlog.Data;
    using CoffeeBlog.Data.Models;
    using CoffeeBlog.Services.Data.Interfaces;
    using CoffeeBlog.Web.ViewModels.Administration.Tags;
    using CoffeeBlog.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class PostsController : AdministrationController
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostsService productsService;
        private readonly ITagsService tagsService;

        public PostsController(
            ApplicationDbContext context,
            IPostsService productsService,
            ITagsService tagsService)
        {
            _context = context;
            this.productsService = productsService;
            this.tagsService = tagsService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this._context.Posts.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var post = await this._context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);

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
                await this.productsService.AddProduct(
                    inputModel.Title,
                    inputModel.Content,
                    inputModel.PreviewImagePath,
                    inputModel.Tags);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var post = await this._context.Posts.FindAsync(id);
            if (post == null)
            {
                return this.NotFound();
            }

            return this.View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,PreviewImagePath,Content,Id")] Post post)
        {
            if (id != post.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(post);
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.PostExists(post.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(post);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var post = await this._context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var post = await this._context.Posts.FindAsync(id);

            this._context.Posts.Remove(post);
            await this._context.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        private bool PostExists(int id)
        {
            return this._context.Posts.Any(e => e.Id == id);
        }
    }
}

namespace CoffeeBlog.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CoffeeBlog.Data.Models;
    using CoffeeBlog.Services.Data.Interfaces;
    using CoffeeBlog.Web.ViewModels.Tags;
    using Microsoft.AspNetCore.Mvc;

    public class TagsController : AdministrationController
    {
        private readonly ITagsService tagsService;

        public TagsController(ITagsService tagsService)
        {
            this.tagsService = tagsService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.tagsService.GetAllAsync());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = this.tagsService.GetById(id.Value);

            if (tag == null)
            {
                return this.NotFound();
            }

            return this.View(tag);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTagInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.tagsService.AddTag(inputModel.Title);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.RedirectToAction(nameof(this.Create));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = this.tagsService.GetById(id.Value);

            if (tag == null)
            {
                return this.NotFound();
            }

            return this.View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,CreatedOn,Id")] Tag tag)
        {
            if (id != tag.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.tagsService.Edit(tag);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(tag);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = this.tagsService.GetById(id.Value);

            if (tag == null)
            {
                return this.NotFound();
            }

            return this.View(tag);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = this.tagsService.GetById(id);

            await this.tagsService.Delete(tag);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

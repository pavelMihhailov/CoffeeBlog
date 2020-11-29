namespace CoffeeBlog.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CoffeeBlog.Data;
    using CoffeeBlog.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this._context.Tags.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = await this._context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);

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
        public async Task<IActionResult> Create([Bind("Title")] Tag tag)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Add(tag);
                await this._context.SaveChangesAsync();

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(tag);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = await this._context.Tags.FindAsync(id);

            if (tag == null)
            {
                return this.NotFound();
            }

            return this.View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Id")] Tag tag)
        {
            if (id != tag.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(tag);
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.TagExists(tag.Id))
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

            return this.View(tag);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = await this._context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);

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
            var tag = await this._context.Tags.FindAsync(id);

            this._context.Tags.Remove(tag);
            await this._context.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        private bool TagExists(int id)
        {
            return this._context.Tags.Any(e => e.Id == id);
        }
    }
}

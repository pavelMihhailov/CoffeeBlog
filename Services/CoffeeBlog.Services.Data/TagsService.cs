namespace CoffeeBlog.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CoffeeBlog.Data.Common.Repositories;
    using CoffeeBlog.Data.Models;
    using CoffeeBlog.Services.Data.Interfaces;
    using CoffeeBlog.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class TagsService : ITagsService
    {
        private readonly IRepository<Tag> tagRepo;

        public TagsService(IRepository<Tag> tagRepo)
        {
            this.tagRepo = tagRepo;
        }

        public T GetById<T>(int id)
        {
            var product = this.tagRepo.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return product;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await this.tagRepo.All().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.tagRepo.All().To<T>().ToListAsync();
        }
    }
}

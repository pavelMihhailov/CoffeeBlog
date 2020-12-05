namespace CoffeeBlog.Services.Data
{
    using System;
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
        private readonly IRepository<PostTag> postTagRepo;

        public TagsService(IRepository<Tag> tagRepo, IRepository<PostTag> postTagRepo)
        {
            this.tagRepo = tagRepo;
            this.postTagRepo = postTagRepo;
        }

        public Tag GetById(int id)
        {
            return this.tagRepo.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public T GetById<T>(int id)
        {
            var tag = this.tagRepo.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return tag;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await this.tagRepo.All().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.tagRepo.All().To<T>().ToListAsync();
        }

        public async Task AddTag(string title)
        {
            var tag = new Tag
            {
                Title = title,
                CreatedOn = DateTime.UtcNow,
            };

            await this.tagRepo.AddAsync(tag);
            await this.tagRepo.SaveChangesAsync();
        }

        public async Task Edit(Tag tag)
        {
            tag.ModifiedOn = DateTime.UtcNow;

            this.tagRepo.Update(tag);
            await this.tagRepo.SaveChangesAsync();
        }

        public async Task Delete(Tag tag)
        {
            var relatedPosts = this.postTagRepo.All().Where(x => x.TagId == tag.Id);

            await relatedPosts.ForEachAsync<PostTag>(x => this.postTagRepo.Delete(x));
            await this.postTagRepo.SaveChangesAsync();

            this.tagRepo.Delete(tag);
            await this.tagRepo.SaveChangesAsync();
        }
    }
}

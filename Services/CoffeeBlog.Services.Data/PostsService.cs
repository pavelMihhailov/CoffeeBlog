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

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postRepo;
        private readonly IDeletableEntityRepository<Tag> tagRepo;
        private readonly IRepository<PostTag> postWithTagsRepo;

        public PostsService(
            IDeletableEntityRepository<Post> postRepo,
            IDeletableEntityRepository<Tag> tagRepo,
            IRepository<PostTag> postWithTagsRepo)
        {
            this.postRepo = postRepo;
            this.tagRepo = tagRepo;
            this.postWithTagsRepo = postWithTagsRepo;
        }

        public T GetById<T>(int id)
        {
            var product = this.postRepo.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return product;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await this.postRepo.All().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.postRepo.All().To<T>().ToListAsync();
        }

        public async Task<Task> AddPostAsync(Post post)
        {
            post.CreatedOn = DateTime.UtcNow;

            await this.postRepo.AddAsync(post);
            await this.postRepo.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task AddProduct(
            string title,
            string content,
            string previewImagePath,
            IEnumerable<int> tagIds)
        {
            var post = new Post
            {
                Title = title,
                Content = content,
                PreviewImagePath = previewImagePath,
            };

            this.AddTagsToPost(tagIds, post);

            await this.AddPostAsync(post);
        }

        public async Task<IEnumerable<int>> GetPostRelatedTagIds(int postId)
        {
            return await this.postWithTagsRepo
                .All()
                .Where(x => x.PostId == postId)
                .Select(x => x.TagId)
                .ToListAsync();
        }

        private void AddTagsToPost(IEnumerable<int> tagIds, Post post)
        {
            foreach (int tagId in tagIds)
            {
                Tag tag = this.tagRepo.All().FirstOrDefault(x => x.Id == tagId);

                bool isTagAlreadyAdded = this.postWithTagsRepo.All()
                    .Any(x => x.TagId == tagId && x.PostId == post.Id);

                if (tag != null && !isTagAlreadyAdded)
                {
                    post.PostTags.Add(
                        new PostTag { Tag = tag, Post = post });
                }
            }
        }
    }
}

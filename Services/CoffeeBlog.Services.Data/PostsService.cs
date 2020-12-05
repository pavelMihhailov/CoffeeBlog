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
        private readonly ITagsService tagsService;
        private readonly IRepository<PostTag> postWithTagsRepo;

        public PostsService(
            IDeletableEntityRepository<Post> postRepo,
            ITagsService tagsService,
            IRepository<PostTag> postWithTagsRepo)
        {
            this.postRepo = postRepo;
            this.tagsService = tagsService;
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

        public async Task Edit(Post post, IEnumerable<int> selectedTags)
        {
            if (selectedTags == null)
            {
                selectedTags = new List<int>();
            }

            await this.RemoveUnselectedTags(post.Id, selectedTags);
            this.AddTagsToPost(selectedTags, post);
            await this.UpdatePostAsync(post);
        }

        public async Task<IEnumerable<int>> GetPostRelatedTagIds(int postId)
        {
            return await this.postWithTagsRepo
                .All()
                .Where(x => x.PostId == postId)
                .Select(x => x.TagId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsWithTag(int tagId)
        {
            var postsIds = this.postWithTagsRepo.All()
                .Where(x => x.TagId == tagId)
                .Select(x => x.PostId);

            var allPosts = await this.GetAllAsync();

            return allPosts.Where(x => postsIds.Contains(x.Id));
        }

        private async Task<Task> AddPostAsync(Post post)
        {
            post.CreatedOn = DateTime.UtcNow;

            await this.postRepo.AddAsync(post);
            await this.postRepo.SaveChangesAsync();

            return Task.CompletedTask;
        }

        private async Task UpdatePostAsync(Post post)
        {
            post.ModifiedOn = DateTime.UtcNow;

            this.postRepo.Update(post);
            await this.postRepo.SaveChangesAsync();
        }

        private void AddTagsToPost(IEnumerable<int> tagIds, Post post)
        {
            foreach (int tagId in tagIds)
            {
                Tag tag = this.tagsService.GetById(tagId);

                bool isTagAlreadyAdded = this.postWithTagsRepo.All()
                    .Any(x => x.TagId == tagId && x.PostId == post.Id);

                if (tag != null && !isTagAlreadyAdded)
                {
                    post.PostTags.Add(
                        new PostTag { Tag = tag, Post = post });
                }
            }
        }

        private async Task RemoveUnselectedTags(int postId, IEnumerable<int> selectedTags)
        {
            List<PostTag> postWithTags = await this.postWithTagsRepo.All()
                .Where(x => x.PostId == postId)
                .ToListAsync();

            foreach (var postWithTag in postWithTags)
            {
                if (!selectedTags.Contains(postWithTag.TagId))
                {
                    this.postWithTagsRepo.Delete(postWithTag);
                }
            }

            await this.postWithTagsRepo.SaveChangesAsync();
        }
    }
}

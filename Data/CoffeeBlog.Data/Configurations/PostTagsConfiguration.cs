namespace CoffeeBlog.Data.Configurations
{
    using CoffeeBlog.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PostTagsConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> postTag)
        {
            postTag.HasKey(x => new { x.PostId, x.TagId });

            postTag.HasOne<Post>(x => x.Post)
                .WithMany(x => x.PostTags)
                .HasForeignKey(x => x.PostId);

            postTag.HasOne<Tag>(x => x.Tag)
                .WithMany(x => x.PostTags)
                .HasForeignKey(x => x.TagId);
        }
    }
}

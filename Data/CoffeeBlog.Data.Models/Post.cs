namespace CoffeeBlog.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CoffeeBlog.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.PostTags = new List<PostTag>();
        }

        [Required]
        public string Title { get; set; }

        public string PreviewImagePath { get; set; }

        public string Content { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}

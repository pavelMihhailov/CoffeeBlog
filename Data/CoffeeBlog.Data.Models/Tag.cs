namespace CoffeeBlog.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CoffeeBlog.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.PostTags = new List<PostTag>();
        }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}

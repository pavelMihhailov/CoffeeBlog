namespace CoffeeBlog.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using CoffeeBlog.Data.Models;
    using CoffeeBlog.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditPostViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        [DisplayName("Preview Image Link")]
        public string PreviewImagePath { get; set; }

        public string Content { get; set; }

        [DisplayName("Created On:")]
        public DateTime CreatedOn { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }

        public IEnumerable<int> SelectedTags { get; set; }
    }
}

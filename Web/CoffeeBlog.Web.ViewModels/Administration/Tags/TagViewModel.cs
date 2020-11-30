namespace CoffeeBlog.Web.ViewModels.Administration.Tags
{
    using CoffeeBlog.Data.Models;
    using CoffeeBlog.Services.Mapping;

    public class TagViewModel : IMapFrom<Tag>
    {
        public string Title { get; set; }

        public int Id { get; set; }
    }
}


namespace BurberDinner.Contracts.Menus
{
    public class CreateMenuResponse
    {
        public string? Id { get; set; }
        public string? HostId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double AverageRating { get; set; }
        public List<MenuSectionResponse>? Sections { get; set; }

        public class MenuSectionResponse
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public List<MenuItemResponse>? Items { get; set; }
        }

        public class MenuItemResponse
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
        }
    }
}

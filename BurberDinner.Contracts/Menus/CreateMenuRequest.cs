
using MediatR;
using ErrorOr;
using BurberDinner.Domain.MenuAggregate.Entities;

namespace BurberDinner.Contracts.Menus
{
    public record CreateMenuRequest : IRequest<ErrorOr<Menu>>
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public double? AverageRating { get; init; } // Nullable
        public List<MenuSection>? Sections { get; init; }
    }

    public record MenuSection(
        string Name,
        string Description,
        List<MenuItem> Items
    );

    public record MenuItem(
        string Name,
        string Description
    );
}

using System.Collections.Generic;
using MediatR;
using ErrorOr;
using BurberDinner.Domain.MenuAggregate.Entities;
using BurberDinner.Domain.HostAggregate.ValueObjects;

namespace BurberDinner.Application.Menus.Commands.CreateMenu
{
    public record CreateMenuCommand(
        HostId HostId,
        string Name,
        string Description,
        List<MenuSectionCommand> Sections
    ) : IRequest<ErrorOr<Menu>>
    {
        public double? AverageRating { get; internal set; }
    }

    public record MenuSectionCommand(
        string Name,
        string Description,
        List<MenuItemCommand> Items
    );

    public record MenuItemCommand(
        string Name,
        string Description
    );
}

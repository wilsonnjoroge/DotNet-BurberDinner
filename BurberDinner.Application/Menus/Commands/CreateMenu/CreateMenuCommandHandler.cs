using BurberDinner.Domain.MenuAggregate.Entities;
using BurberDinner.Application.Menus.Commands.CreateMenu;
using MediatR;
using ErrorOr;
using BurberDinner.Application.Common.Interfaces.Persistence;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // Convert the request's sections and items into domain entities using the factory method
        var sections = request.Sections.ConvertAll(section =>
            MenuSection.Create(
                section.Name,
                section.Description,
                section.Items.Select(item => MenuItem.Create(item.Name, item.Description)).ToList()
            )
        );

        // Assuming Menu has a constructor or a factory method to handle this
        var menu = Menu.Create(
            request.HostId,
            request.Name,
            request.Description,
            request.AverageRating,
            sections
        );

        // Save the menu to the repository (implementation may vary)
        _menuRepository.Add(menu);

        return menu;
    }
}

using BurberDinner.Application.Menus.Commands.CreateMenu;
using BurberDinner.Domain.MenuAggregate;
using BurberDinner.Contracts.Menus;
using Mapster;
using BurberDinner.Domain.HostAggregate.ValueObjects;
using BurberDinner.Domain.MenuAggregate.Entities;

namespace BurberDinner.Api.Common.Mapping
{
    public class MenuMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Existing mapping configuration
            config.NewConfig<(CreateMenuCommand Command, HostId HostId), CreateMenuCommand>()
                .ConstructUsing(src => new CreateMenuCommand(
                    src.HostId,
                    src.Command.Name,
                    src.Command.Description,
                    src.Command.Sections
                ));

            // New mapping configuration for response
            config.NewConfig<Menu, CreateMenuResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.HostId, src => src.HostId.Value)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.AverageRating, src => src.AverageRating)
                .Map(dest => dest.Sections, src => src.Sections.Select(s => new CreateMenuResponse.MenuSectionResponse
                {
                    Name = s.Name,
                    Description = s.Description,
                    Items = s.Items.Select(i => new CreateMenuResponse.MenuItemResponse
                    {
                        Name = i.Name,
                        Description = i.Description
                    }).ToList()
                }).ToList());
        }
    }
}

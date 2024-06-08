using BurberDinner.Application.Menus.Commands.CreateMenu;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BurberDinner.Contracts.Menus;
using BurberDinner.Domain.HostAggregate.ValueObjects;

namespace BurberDinner.Api.Controllers
{
    [Route("hosts/{hostId}/menus")]
    public class MenuController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public MenuController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuRequest request, [FromRoute] string hostId)
        {
            var command = new CreateMenuCommand(
                HostId: HostId.CreateUnique(),
                Name: request.Name,
                Description: request.Description,
                Sections: request.Sections.Select(s => new MenuSectionCommand(
                    s.Name,
                    s.Description,
                    s.Items.Select(i => new MenuItemCommand(i.Name, i.Description)).ToList()
                )).ToList()
            );

            var createMenuResult = await _mediator.Send(command);

            return createMenuResult.Match(menu => Ok(_mapper.Map<CreateMenuResponse>(menu)),
            errors => Problem(errors));
        }
    }
}

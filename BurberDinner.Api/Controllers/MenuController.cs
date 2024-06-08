
using BurberDinner.Application.Menus.Commands.CreateMenu; 
using MapsterMapper; 
using MediatR; 
using Microsoft.AspNetCore.Mvc; 
using BurberDinner.Contracts.Menus; 
using BurberDinner.Domain.HostAggregate.ValueObjects; 

namespace BurberDinner.Api.Controllers
{
    // Define a route for menu creation operations, incorporating the host ID
    [Route("hosts/{hostId}/menus")]
    public class MenuController : ApiController
    {
        private readonly IMapper _mapper; // Dependency for object mapping
        private readonly ISender _mediator; // Dependency for MediatR for command handling

        // Constructor to inject dependencies (mapper and mediator)
        public MenuController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // Action method to handle HTTP POST requests for menu creation
        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuRequest request, [FromRoute] string hostId)
        {          
            // Create a command object to represent the menu creation request
            var command = new CreateMenuCommand(
                HostId: HostId.CreateUnique(), // Assuming a method to create a unique host ID
                Name: request.Name,
                Description: request.Description,
                Sections: request.Sections.Select(s => new MenuSectionCommand(
                    s.Name,
                    s.Description,
                    s.Items.Select(i => new MenuItemCommand(i.Name, i.Description)).ToList()
                )).ToList()
            );

            // Send the command to the application layer using MediatR and await the result
            var createMenuResult = await _mediator.Send(command);

            // Match the result: if successful, return the mapped menu response; if errors occurred, return a problem response
            return createMenuResult.Match(menu => Ok(_mapper.Map<CreateMenuResponse>(menu)),
                errors => Problem(errors));
        }
    }
}

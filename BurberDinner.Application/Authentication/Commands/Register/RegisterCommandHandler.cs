
using BurberDinner.Application.Authentication.Common;
using BurberDinner.Application.Common.Interfaces.Authentication;
using BurberDinner.Application.Common.Interfaces.Persistence;
using BurberDinner.Domain.Common.Errors;
using BurberDinner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace BurberDinner.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : 
          IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            // Validate user !exists in the db
            if(_userRepository.GetUserByEmail(command.Email) is not null)
            {
                return  Errors.User.DuplicateEmail;
            }

            // Create user(Generate unique id) and persist in to DB
            var user = new User{
                FirstName = command.FirstName, 
                LastName = command.LastName, 
                Email = command.Email, 
                Password = command.Password
            };

            _userRepository.AddUser(user);            

            //Generate Token
            var token = _jwtTokenGenerator.GenerateToken( user);


            return new AuthenticationResult(
              user, 
              token
            );
        }
    }
}
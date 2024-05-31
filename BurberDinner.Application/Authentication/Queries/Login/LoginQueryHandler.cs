
using BurberDinner.Application.Authentication.Common;
using BurberDinner.Application.Authentication.Queries.Login;
using BurberDinner.Application.Common.Interfaces.Authentication;
using BurberDinner.Application.Common.Interfaces.Persistence;
using BurberDinner.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace BurberDinner.Application.Authentication.Commands.Login
{
    public class LoginQueryHandler : 
          IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
          // Validate the user exists
            var user = _userRepository.GetUserByEmail(query.Email);
            if(user is null)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // Validate password is correct
            if(user.Password!=  query.Password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // Create jwt token
            var token = _jwtTokenGenerator.GenerateToken( user);
            return new AuthenticationResult(
              user,
              token
              );
        }
    }
}
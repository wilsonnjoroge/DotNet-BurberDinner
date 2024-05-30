
using BurberDinner.Application.Common.Errors;
using BurberDinner.Application.Common.Interfaces.Authentication;
using BurberDinner.Application.Common.Interfaces.Persistence;
using BurberDinner.Application.Services.Authentication.Common;
using BurberDinner.Domain.Common.Errors;
using BurberDinner.Domain.Entities;
using ErrorOr;

namespace BurberDinner.Application.Services.Authentication.Queries
{
    public class AuthenticationQueryService : IAuthenticationQueryService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        
        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            // Validate the user exists
            var user = _userRepository.GetUserByEmail(email);
            if(user is null)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // Validate password is correct
            if(user.Password!=  password)
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
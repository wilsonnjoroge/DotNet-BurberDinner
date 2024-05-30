
using BurberDinner.Application.Common.Errors;
using BurberDinner.Application.Common.Interfaces.Authentication;
using BurberDinner.Application.Common.Interfaces.Persistence;
using BurberDinner.Application.Services.Authentication.Common;
using BurberDinner.Domain.Common.Errors;
using BurberDinner.Domain.Entities;
using ErrorOr;

namespace BurberDinner.Application.Services.Authentication.Commands
{
    public class AuthenticationCommandService : IAuthenticationCommandService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // Validate user !exists in the db
            if(_userRepository.GetUserByEmail(email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            // Create user(Generate unique id) and persist in to DB
            var user = new User{
                FirstName = firstName, 
                LastName = lastName, 
                Email = email, 
                Password = password
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

using BurberDinner.Application.Common.Interfaces.Authentication;
using BurberDinner.Application.Common.Interfaces.Persistence;
using BurberDinner.Domain.Entities;

namespace BurberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            // Validate user !exists in the db
            if(_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with provided email already exists");
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

        
        public AuthenticationResult Login(string email, string password)
        {
            // Validate the user exists
            var user = _userRepository.GetUserByEmail(email);
            if(user is null)
            {
                throw new Exception("User with provided email does not exists");
            }

            // Validate password is correct
            if(user.Password!=  password)
            {
                throw new Exception("Invalid password");
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
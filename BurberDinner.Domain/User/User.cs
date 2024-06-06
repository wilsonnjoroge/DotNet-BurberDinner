
using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.User.ValueObjects;

namespace BurberDinner.Domain.User.Entities
{
    public sealed class User : Entity<UserId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        private string Password { get; set; }  
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        private User(
            UserId userId,
            string firstName,
            string lastName,
            string email,
            string password, 
            DateTime createdDateTime,
            DateTime updatedDateTime) : base(userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;  
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static User Create(
            string firstName,
            string lastName,
            string email,
            string password)  
        {
            return new User(
                UserId.CreateUnique(),
                firstName,
                lastName,
                email,
                password,  
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        public void UpdateName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void UpdatePassword(string password)  
        {
            Password = password;  
            UpdatedDateTime = DateTime.UtcNow;
        }
    }
}

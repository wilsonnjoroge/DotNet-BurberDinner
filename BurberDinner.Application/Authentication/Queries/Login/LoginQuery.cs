
using BurberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BurberDinner.Application.Authentication.Queries.Login
{
  public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
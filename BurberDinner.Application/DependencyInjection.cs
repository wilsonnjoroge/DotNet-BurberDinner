
using BurberDinner.Application.Services.Authentication;
using BurberDinner.Application.Services.Authentication.Commands;
using BurberDinner.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BurberDinner.Application
{
    public static class DependencyInjection
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
      services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
      return services;
    } 
  }
}
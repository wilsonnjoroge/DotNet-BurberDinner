
using System.Reflection;
using BurberDinner.Application.Authentication.Commands.Register;
using BurberDinner.Application.Authentication.Common;
using BurberDinner.Application.Common.Behaviors;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BurberDinner.Application
{
    public static class DependencyInjection
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      services.AddMediatR(typeof(DependencyInjection).Assembly);

      services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

      return services;
    } 
  }
}
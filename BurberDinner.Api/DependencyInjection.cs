
using BurberDinner.Api.Common.Errors;
using BurberDinner.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BurberDinner.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
          services.AddControllers();
          services.AddSingleton<ProblemDetailsFactory, BurberDinnerProblemDetailsFactory>();
          services.AddMapping();

          return services;
        }
    }
}
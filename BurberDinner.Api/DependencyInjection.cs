using BurberDinner.Api.Common.Errors;
using BurberDinner.Api.Common.Mapping;
using BurberDinner.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure; // Add this namespace for the AddApplication method
using Microsoft.Extensions.DependencyInjection; // Add this namespace for IServiceCollection

namespace BurberDinner.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            // Add presentation services
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, BurberDinnerProblemDetailsFactory>();
            services.AddMapping();

            // Add application services
            services.AddApplication(); // Add other application services

            // Add MediatR registration
            services.AddMediatR(typeof(DependencyInjection)); // Register all MediatR handlers in the assembly where the DependencyInjection class resides.

            return services;
        }
    }
}

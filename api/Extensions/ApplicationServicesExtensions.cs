using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddAplicationSettings(this IServiceCollection services)
    {
        // Add services to the container.
        // AddScoped creates an instance of our repository for global use and, when the request is done,
        // then it disposes both the controller and the repository
        // Globally, the methods of IProductRepository implemented in ProductRepository class are available
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Configuring Api behaviour for custom apiController response
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });
        
        return services;
    }
}
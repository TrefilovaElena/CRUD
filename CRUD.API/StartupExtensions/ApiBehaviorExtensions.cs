using CRUD.API.Controllers.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.API.StartupExtensions
{
    public static class ApiBehaviorExtensions
    {
        internal static void RegisterDataAnnotationErrorsHandling(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    var errorResponse = new ErrorResult(string.Join("; ", errors), 400);

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }
    }
}

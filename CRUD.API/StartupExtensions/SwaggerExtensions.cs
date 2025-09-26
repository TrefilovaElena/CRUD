using System.Reflection;

namespace CRUD.API.StartupExtensions
{
    public static class SwaggerExtensions
    {
        public static void RegisterSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
        public static void AppendSwagger(this WebApplication application)
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }
    }
}

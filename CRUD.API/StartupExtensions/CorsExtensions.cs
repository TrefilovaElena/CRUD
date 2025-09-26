namespace CRUD.API.StartupExtensions
{
    public static class CorsExtensions
    {
        private static readonly string _defaultCorsPolicy = "Default";
        public static void AddCorsPolicy(this WebApplicationBuilder builder)
        {
            var allowedHosts = builder.Configuration["AllowedHosts"]?.Split(';');
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: _defaultCorsPolicy,
                                  policy =>
                                  {
                                      policy.WithOrigins(allowedHosts)
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });
        }
        public static void UseCorsPolicy(this WebApplication application)
            => application.UseCors(_defaultCorsPolicy);
    }
}

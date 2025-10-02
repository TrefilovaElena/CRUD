using AutoMapper;
using CRUD.API.Mapping;
using CRUD.API.StartupExtensions;
using CRUD.BLL.Mapping;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers()
           .AddJsonOptions(x =>
           {
               x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
           });

builder.services.AddOpenTelemetry()
    .WithTracing(builder => builder
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation())
    .WithMetrics(builder => builder
        .AddAspNetCoreInstrumentation()); ;



builder.AddCorsPolicy();

builder.RegisterEntityFramework();

builder.RegisterSwagger();

// Inject business services
builder.RegisterProductServices();

builder.RegisterDataAnnotationErrorsHandling();

//IMapper
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<InDTOToManagerRequestsProfile>(); 
    cfg.AddProfile<ManagerResponseToOutDTOProfile>();
    cfg.AddProfile<FilterManagerResponseToOutDTOProfile>();
    cfg.AddProfile<AddRequestToModelProfile>();
    cfg.AddProfile<RequestToServiceParametersProfile>();
});
builder.Services.AddSingleton<IMapper>(new AutoMapper.Mapper(config));

var app = builder.Build();

app.UseCorsPolicy();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();


app.AppendSwagger();

await app.AppendDatabaseMigrationAsync();

app.Run();

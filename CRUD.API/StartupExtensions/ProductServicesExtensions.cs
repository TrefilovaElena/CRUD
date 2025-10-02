using CRUD.BLL.IRepositories;
using CRUD.BLL.Managers.Foos;
using CRUD.BLL.Managers.Foos.Requests.Add;
using CRUD.BLL.Managers.Foos.Requests.Update;
using CRUD.BLL.Managers.Foos.Responses;
using CRUD.BLL.Managers.Interfaces;
using CRUD.BLL.Managers.Interfaces.Foos;
using CRUD.BLL.Models.Foos;
using CRUD.BLL.Services.Abstract;
using CRUD.BLL.Services.Foos;
using CRUD.BLL.Services.Interfaces.Common;
using CRUD.BLL.Services.Interfaces.Foos;
using CRUD.DAL.Repositories.Abstract;

namespace CRUD.API.StartupExtensions
{
    public static class ProductServicesExtensions
    {
        /// <summary>
        /// В этом методе указываются зависимости проекта и конфигурируется приложение
        /// </summary>
        /// <param name="builder"></param>
        internal static void RegisterProductServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddScoped<IFooService, FooService>();
            builder.Services.AddScoped<IFooManager, FooManager>();
            builder.Services.AddScoped<ICRUDManager<Foo, FooAddManagerRequest, FooUpdateManagerRequest, FooManagerResponse>, FooManager>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseEntityRepository<>));            
            builder.Services.AddScoped(typeof(ICRUDService<>), typeof(BaseCRUDService<>));  
        }
    }
}

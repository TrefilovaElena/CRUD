using AutoMapper;
using CRUD.BLL.Managers.Foos.Requests.Filter;
using CRUD.BLL.Services.Foos.Parameters;

namespace CRUD.BLL.Mapping
{
    public class RequestToServiceParametersProfile : Profile
    {
        public RequestToServiceParametersProfile()
        {           
            CreateMap<FooFilterManagerRequest, FooFilterParameters>();
        }
    }
}

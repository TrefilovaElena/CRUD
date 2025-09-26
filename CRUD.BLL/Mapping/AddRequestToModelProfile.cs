using AutoMapper;
using CRUD.BLL.Managers.Foos.Requests.Add;
using CRUD.BLL.Models.Foos;

namespace CRUD.BLL.Mapping
{
    public class AddRequestToModelProfile : Profile
    {
        public AddRequestToModelProfile()
        {
            CreateMap<FooAddManagerRequest, Foo>();
        }
    }
}

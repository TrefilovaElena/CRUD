using AutoMapper;
using CRUD.BLL.Managers.Abstract.Requests;
using CRUD.BLL.Managers.Foos.Requests.Add;
using CRUD.BLL.Managers.Foos.Requests.Filter;
using CRUD.BLL.Managers.Foos.Requests.Update;
using CRUD.DTO.In;
using CRUD.DTO.In.Foos;

namespace CRUD.API.Mapping
{
    public class InDTOToManagerRequestsProfile : Profile
    {
    public InDTOToManagerRequestsProfile()
        {
            this.AllowNullCollections = true;
            CreateMap<FooAddInDto, FooAddManagerRequest>();
            CreateMap<FooUpdateInDto, FooUpdateManagerRequest>();
            CreateMap<FooFilterInDto, FooFilterManagerRequest>();

            CreateMap<DeleteInDto, DeleteManagerRequest>();
        }
    }
}

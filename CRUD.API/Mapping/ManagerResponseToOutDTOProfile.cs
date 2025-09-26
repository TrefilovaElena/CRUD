using AutoMapper;
using CRUD.BLL.Managers.Foos.Responses;
using CRUD.DTO.Out.Foos;

namespace CRUD.API.Mapping
{
    public class ManagerResponseToOutDTOProfile : Profile
    {
        public ManagerResponseToOutDTOProfile()
        {
            CreateMap<FooManagerResponse, FooOutDto>();
            CreateMap<FooManagerResponse, FooTableViewOutDto>();
        }
    }
}

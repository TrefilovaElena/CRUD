using AutoMapper;
using CRUD.BLL.Managers.Abstract.Responses;
using CRUD.BLL.Managers.Foos.Responses;
using CRUD.DTO.Abstract.Filter;
using CRUD.DTO.Out.Foos;

namespace CRUD.API.Mapping
{
    public class FilterManagerResponseToOutDTOProfile : Profile
    {
        public FilterManagerResponseToOutDTOProfile()
        {
            CreateMap<FilterManagerResponse<FooManagerResponse>, FilterOutDto<FooOutDto>>();
            CreateMap<FilterManagerResponse<FooManagerResponse>, FilterOutDto<FooTableViewOutDto>>();
        }
    }

}

using AutoMapper;
using CRUD.API.Controllers.Abstract;
using CRUD.BLL.Managers.Abstract.Requests;
using CRUD.BLL.Managers.Abstract.Responses;
using CRUD.BLL.Managers.Foos.Requests.Add;
using CRUD.BLL.Managers.Foos.Requests.Filter;
using CRUD.BLL.Managers.Foos.Requests.Update;
using CRUD.BLL.Managers.Foos.Responses;
using CRUD.BLL.Managers.Interfaces;
using CRUD.BLL.Managers.Interfaces.Foos;
using CRUD.BLL.Models.Foos;
using CRUD.DTO.Abstract.Filter;
using CRUD.DTO.In;
using CRUD.DTO.In.Foos;
using CRUD.DTO.Out.Foos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.API.Controllers.Administrators
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoosController(ICRUDManager<Foo, FooAddManagerRequest, FooUpdateManagerRequest, FooManagerResponse> _manager,
                               IFooManager _fooManager,
                               IMapper _mapper) : BaseController(_mapper)
    {

        /// <summary>
        /// Получение по фильтру
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("Filter")]
        [ProducesResponseType<ErrorResult>(StatusCodes.Status400BadRequest, "application/json")]
        [ProducesResponseType<FilterOutDto<FooTableViewOutDto>>(StatusCodes.Status200OK, "application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        public async Task<ActionResult<FilterOutDto<FooTableViewOutDto>>> FilterAsync([FromQuery] FooFilterInDto dto)
        {
            var request = _mapper.Map<FooFilterManagerRequest>(dto);
    
            var result = await _fooManager.FilterAsync(request);
            return GetObjectResult<FilterManagerResponse<FooManagerResponse>, FilterOutDto<FooTableViewOutDto>>(result);
        }

        /// <summary>
        /// Создание
        /// </summary>        
        /// <returns></returns>
        [HttpPost()]
        [Consumes("application/json")]
        [ProducesResponseType<ErrorResult>(StatusCodes.Status400BadRequest, "application/json")]
        [ProducesResponseType<FooOutDto>(StatusCodes.Status200OK, "application/json")]
        [AllowAnonymous]
        public async Task<ActionResult<FooOutDto>> AddAsync([FromBody] FooAddInDto dto)
        {
            var request = _mapper.Map<FooAddManagerRequest>(dto);
            var result = await _manager.AddAsync(request);
            return GetObjectResult<FooManagerResponse, FooOutDto>(result);
        }

        /// <summary>
        /// Получение по id
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType<ErrorResult>(StatusCodes.Status404NotFound, "application/json")]
        [ProducesResponseType<ErrorResult>(StatusCodes.Status400BadRequest, "application/json")]
        [ProducesResponseType<FooOutDto>(StatusCodes.Status200OK, "application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        public async Task<ActionResult<FooOutDto>> GetByIdAsync(long id)
        {
            var result = await _manager.GetByIdAsync(id);
            return GetObjectResult<FooManagerResponse, FooOutDto>(result);
        }


        /// <summary>
        /// Обновление
        /// </summary> 
        /// <param name="id">Идентификатор</param>
        /// <param name="dto">Данные</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType<ErrorResult>(StatusCodes.Status404NotFound, "application/json")]
        [ProducesResponseType<ErrorResult>(StatusCodes.Status400BadRequest, "application/json")]
        [ProducesResponseType<FooOutDto>(StatusCodes.Status200OK, "application/json")]
        [AllowAnonymous]
        public async Task<ActionResult<FooOutDto>> UpdateAsync(long id, [FromBody] FooUpdateInDto dto)
        {
            var request = _mapper.Map<FooUpdateManagerRequest>(dto);
            var result = await _manager.UpdateAsync(request, id);
            return GetObjectResult<FooManagerResponse, FooOutDto>(result);
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType<ErrorResult>(StatusCodes.Status404NotFound, "application/json")]
        [ProducesResponseType<ErrorResult>(StatusCodes.Status400BadRequest, "application/json")]
        [ProducesResponseType<bool>(StatusCodes.Status200OK, "application/json")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> DeleteAsync(long id, [FromQuery] DeleteInDto dto)
        {
            var request = _mapper.Map<DeleteManagerRequest>(dto);
            var result = await _manager.DeleteAsync(id, request);
            return GetObjectResult(result);
        }
    }
}
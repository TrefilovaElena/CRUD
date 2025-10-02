using AutoMapper;
using CRUD.BLL.Managers.Abstract.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.API.Controllers.Abstract
{
    public abstract class BaseController : ControllerBase
    {
        protected IMapper _mapper;
        public BaseController(IMapper mapper) 
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected ObjectResult GetObjectResult<managerResultT, outDTO>(ManagerResult<managerResultT> managerResult)
        {
            if (managerResult.IsSuccess)
                return new ObjectResult(_mapper.Map<outDTO>(managerResult.Object)) { StatusCode = (int)managerResult.Status };
            else
                return new ObjectResult(new ErrorResult(managerResult.ErrorMessage, (int)managerResult.Status))
                { StatusCode = (int)managerResult.Status };
        }
        protected ObjectResult GetObjectResult<managerResultT>(ManagerResult<managerResultT> managerResult)
        {
            if (managerResult.IsSuccess)
                return new ObjectResult(managerResult.Object) { StatusCode = (int)managerResult.Status };
            else
                return new ObjectResult(new ErrorResult(managerResult.ErrorMessage, (int)managerResult.Status))
                { StatusCode = (int)managerResult.Status };
        }
    }
}

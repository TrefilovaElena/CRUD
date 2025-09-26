using AutoMapper;
using CRUD.BLL.Managers.Abstract;
using CRUD.BLL.Managers.Abstract.Requests;
using CRUD.BLL.Managers.Abstract.Responses;
using CRUD.BLL.Managers.Foos.Requests.Add;
using CRUD.BLL.Managers.Foos.Requests.Filter;
using CRUD.BLL.Managers.Foos.Requests.Update;
using CRUD.BLL.Managers.Foos.Responses;
using CRUD.BLL.Managers.Interfaces.Foos;
using CRUD.BLL.Mapping.Mapper;
using CRUD.BLL.Models.Foos;
using CRUD.BLL.Services.Foos.Parameters;
using CRUD.BLL.Services.Interfaces.Common;
using CRUD.BLL.Services.Interfaces.Foos;
using System.Linq.Expressions;

namespace CRUD.BLL.Managers.Foos
{
    public class FooManager(
                            ICRUDService<Foo> service,
                            IFooService _fooService,
                            IMapper mapper) : CRUDManager<Foo, FooAddManagerRequest, FooUpdateManagerRequest, FooManagerResponse>(service, mapper),
                                              IFooManager
    {

        protected override void SetPropertiesToJoin()
        {
            _propertiesToJoinGetById = new Expression<Func<Foo, object>>[]
            {
                x => x.FooFoo
            };
        }

        /// <summary>
        /// Получение Foo по фильтру
        /// </summary>
        public async Task<ManagerResult<FilterManagerResponse<FooManagerResponse>>> FilterAsync(FooFilterManagerRequest request)
        {
            var parameters = _mapper.Map<FooFilterParameters>(request);
            return await FilterAsync(parameters);
        }

        private async Task<ManagerResult<FilterManagerResponse<FooManagerResponse>>> FilterAsync(FooFilterParameters parameters)
        {
            var items = await _fooService.FilterAsync(parameters);
            var count = await _fooService.CountAsync(parameters);

            var result = new FilterManagerResponse<FooManagerResponse>()
            {
                Items = await GetResponseAsync(items),
                TotalCount = count,
            };
            return ManagerResult<FilterManagerResponse<FooManagerResponse>>.CreateSuccessResult(ManagerStatus.Ok, result);
        }

        protected override FooManagerResponse GetResponse(Foo model) => FooMapper.GetFoosResponse(model);

        protected async override Task<ManagerResult<bool>> CheckForUpdateAsync(FooUpdateManagerRequest request, Foo? model)
        {
            var baseCheck = await base.CheckForUpdateAsync(request, model);

            if (!baseCheck.IsSuccess)
                return baseCheck;

            var titleCheck = CheckTitle(request.Title);
                if (!titleCheck.IsSuccess)
                    return titleCheck;

            return ManagerResult<bool>.CreateSuccessResult(ManagerStatus.Ok, true);
        }

        protected async override Task<ManagerResult<bool>> CheckForAddAsync(FooAddManagerRequest request)
        {
            var baseCheck = await base.CheckForAddAsync(request);
            if (!baseCheck.IsSuccess)
                return baseCheck;

            var titleCheck = CheckTitle(request.Title);
            if (!titleCheck.IsSuccess)
                return titleCheck;

            return ManagerResult<bool>.CreateSuccessResult(ManagerStatus.Ok, true);
        }

        protected override async Task<ManagerResult<bool>> CheckForForceDeleteAsync(Foo model)
        {
            return ManagerResult<bool>.CreateSuccessResult(ManagerStatus.Ok, true);
        }

        protected override void UpdateModel(Foo model, FooUpdateManagerRequest request)
        {
            if (request.Title != null)
            {
                model.Title = request.Title;
            }
            model.LastModifiedDate = DateTimeOffset.UtcNow;
        }

        private ManagerResult<bool> CheckTitle(string Title)
        {
            if (false)
                return ManagerResult<bool>.CreateErrorResult(ManagerStatus.BadRequest, "Описание ошибки.");
           
            return ManagerResult<bool>.CreateSuccessResult(ManagerStatus.Ok, true);
        }
    }
}

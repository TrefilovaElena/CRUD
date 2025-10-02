using AutoMapper;
using CRUD.BLL.Managers.Abstract.Requests;
using CRUD.BLL.Managers.Interfaces;
using CRUD.BLL.Models;
using CRUD.BLL.Services.Interfaces.Common;
using System.Linq.Expressions;
using System.Transactions;

namespace CRUD.BLL.Managers.Abstract
{
    /// <summary>
    /// Подготовка данных для выполнения задач сервисом
    /// </summary>
    public abstract class CRUDManager<T, RequestADD, RequestUpdate, Response> 
        : ICRUDManager<T, RequestADD, RequestUpdate, Response>
        where T : class, IEntity
    {
        protected readonly ICRUDService<T> _service;
        protected readonly IMapper _mapper;
        /// <summary>
        /// Какие комплексные свойства должны быть использованы при получении по id
        /// </summary>
        protected Expression<Func<T, object>>[] _propertiesToJoinGetById =[];
        /// <summary>
        /// Какие комплексные свойства должны быть использованы при получении списка
        /// </summary>
        protected Expression<Func<T, object>>[] _propertiesToJoinGet = [];

        public CRUDManager(ICRUDService<T> service, IMapper mapper)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            SetPropertiesToJoin();
        }
        public async Task<ManagerResult<Response?>> GetByIdAsync(long id)
        {
            var model = await _service.GetByIdAsync(id, false, _propertiesToJoinGetById);
            var isExist = await CheckIfExistAsync(model);
            if (!isExist.IsSuccess)
                return ManagerResult<Response?>.CreateErrorResult(ManagerStatus.NotFound, isExist.ErrorMessage);
            else
                return ManagerResult<Response?>.CreateSuccessResult(ManagerStatus.Ok, GetResponse(model));
        }

        public async Task<ManagerResult<List<Response>>> GetAsync()
        {
            var result = await _service.GetAsync(_propertiesToJoinGet);
            return ManagerResult<List<Response>>.CreateSuccessResult(ManagerStatus.Ok, await GetResponseAsync(result));
        }

        /// <summary>
        /// Обновление
        /// </summary>
        public async Task<ManagerResult<Response?>> UpdateAsync(RequestUpdate dto, long id)
        {
            var model = await _service.GetByIdWithOutNestedEntitiesAsync(id);
            var checkResult = await CheckForUpdateAsync(dto, model);
            if (!checkResult.IsSuccess)
                return ManagerResult<Response?>.CreateErrorResult(checkResult.Status, checkResult.ErrorMessage);

            UpdateModel(model, dto);

            var checkModelBeforeSaveResult = CheckModelBeforeSave(model);
            if (!checkModelBeforeSaveResult.IsSuccess)
                return ManagerResult<Response?>.CreateErrorResult(checkModelBeforeSaveResult.Status, checkModelBeforeSaveResult.ErrorMessage);

            model = await _service.UpdateEntityAsync(model);

            return ManagerResult<Response?>.CreateSuccessResult(ManagerStatus.Ok, GetResponse(model));
        }

        /// <summary>
        /// Добавление
        /// </summary>
        public async Task<ManagerResult<Response?>> AddAsync(RequestADD dto)
        {
            var newModel = GetNewModel(dto);
            var checkResult = await CheckForAddAsync(dto);
            if (!checkResult.IsSuccess)
                return ManagerResult<Response?>.CreateErrorResult(checkResult.Status, checkResult.ErrorMessage);

            var checkModelBeforeSaveResult = CheckModelBeforeSave(newModel);
            if (!checkModelBeforeSaveResult.IsSuccess)
                return ManagerResult<Response?>.CreateErrorResult(checkModelBeforeSaveResult.Status, checkModelBeforeSaveResult.ErrorMessage);

            var result = await _service.AddEntityAsync(newModel);
            if ((result?.Id ?? 0) == 0)
                return ManagerResult<Response?>.CreateErrorResult(ManagerStatus.BadRequest, "Не сохранено.");
            return ManagerResult<Response?>.CreateSuccessResult(ManagerStatus.Ok, GetResponse(result));
        }
 
        /// <summary>
        /// Удаление
        /// </summary>
        public async Task<ManagerResult<bool>> DeleteAsync(long id, DeleteManagerRequest request)
        {
            var model = await _service.GetByIdAsync(id, false, _propertiesToJoinGetById);
            var checkResult = await CheckForDeleteAsync(model, request);
            if (!checkResult.IsSuccess)
                return ManagerResult<bool>.CreateErrorResult(checkResult.Status, checkResult.ErrorMessage);
            else
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if(!request.IsForced)
                    {
                        var modifyResult = await TryModifyModelBeforeSoftDelete(model);
                        if (!modifyResult.IsSuccess)
                            return modifyResult;
                    }

                    var result = await _service.DeleteAsync(model, request.IsForced);
                    scope.Complete();
                }
                return ManagerResult<bool>.CreateSuccessResult(ManagerStatus.Ok, true);
            }
        }
        protected async virtual Task<ManagerResult<bool>> CheckForUpdateAsync(RequestUpdate dto, T? entity)
        {
            return await CheckIfExistAsync(entity);
        }
        protected async virtual Task<ManagerResult<bool>> CheckForAddAsync(RequestADD request)
        {
            return ManagerResult<bool>.CreateSuccessResult(ManagerStatus.Ok, true);
        }
        protected async virtual Task<ManagerResult<bool>> CheckForDeleteAsync(T? model, DeleteManagerRequest request)
        {
            var isExist = await CheckIfExistAsync(model);

            if (!isExist.IsSuccess || !request.IsForced)
                return isExist;

            return await CheckForForceDeleteAsync(model);
        }

        /// <summary>
        /// This should be used in case if you need to reuse some attached entities by FKs
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async virtual Task<ManagerResult<bool>> TryModifyModelBeforeSoftDelete(T? model)
        {
            return ManagerResult<bool>.CreateSuccessResult(ManagerStatus.Ok, true);
        }
        protected async Task<ManagerResult<bool>> CheckIfExistAsync(T? model)
        {
            if (model == null)
                return ManagerResult<bool>.CreateErrorResult(ManagerStatus.NotFound, $"Не существует.");
            else
                return ManagerResult<bool>.CreateSuccessResult(ManagerStatus.Ok, true);
        }
        protected virtual ManagerResult<bool> CheckModelBeforeSave(T model) => ManagerResult<bool>.CreateSuccessResult(ManagerStatus.Ok, true);
        protected T GetNewModel(RequestADD request) => _mapper.Map<T>(request);

        protected abstract Task<ManagerResult<bool>> CheckForForceDeleteAsync(T model);

        protected abstract void UpdateModel(T model, RequestUpdate request);

        protected abstract Response GetResponse(T model);
        protected virtual async Task<List<Response>> GetResponseAsync(List<T> models) => models
                                                                       ?.Select(x => GetResponse(x)).ToList() 
                                                                       ?? new List<Response>();

        /// <summary>
        /// Установить свойства, которые должны быть отображены при вызове по id или списком
        /// </summary>
        protected virtual void SetPropertiesToJoin()
        {
            _propertiesToJoinGetById = [];
            _propertiesToJoinGet = [];
        }
    }
}

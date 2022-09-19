using AutoLab.Application.ViewModel;
using AutoMapper;
using EstoqueLab.Application.Interfaces;
using EstoqueLab.Application.Parameters;
using EstoqueLab.Domain.Entities;
using EstoqueLab.Domain.Interfaces.Services;
using EstoqueLab.Uteis.Events;
using EstoqueLab.Uteis.Http.Response;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EstoqueLab.Application.Application
{
    public class CategoriaApplication : ICategoriaApplication
    {
        private readonly ICategoriaService _service;
        private readonly ILogger<CategoriaApplication> _logger;
        private readonly IMapper _mapper;

        public CategoriaApplication(ICategoriaService service,
            ILogger<CategoriaApplication> logger,
            IMapper mapper)
        {
            this._service = service;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<CategoriaViewModel>>> ListCategoriaAsync(CategoriaParams paran)
        {
            var response = new BasePaginationResponse<IEnumerable<CategoriaViewModel>>();

            try
            {

                var obj = await _service.GetByParamsAsync(paran.Filter(), paran.OrderBy, paran.Include);

                response.Count = obj.Count();

                if (paran.Skip.HasValue)
                {
                    obj = obj.Skip(paran.Skip.Value);
                }

                if (paran.Take.HasValue && paran.Take.Value > 0)
                {
                    obj = obj.Take(paran.Take.Value);
                }

                response.Data = _mapper.Map<IEnumerable<CategoriaViewModel>>(obj);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, paran), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<CategoriaViewModel>>> ListCategoriaAsync()
        {
            var response = new BaseResponse<IEnumerable<CategoriaViewModel>>();

            try
            {

                var obj = _service.GetAll();

                response.Data = _mapper.Map<IEnumerable<CategoriaViewModel>>(obj);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }

            return response;
        }

        public async Task<BaseResponse<CategoriaViewModel>> Create(CategoriaViewModel paranObj)
        {
            var response = new BaseResponse<CategoriaViewModel>();

            try
            {
                if (paranObj.Nome is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.Nome));
                    return response;
                }

                var existingObj = _service.Get(x => x.Nome == paranObj.Nome).ToList();

                if (existingObj != null && existingObj.Count > 0)
                {
                    response.AddError(Events.INVALID_VALUE, "Categoria");
                    return response;
                }

                var obj = new Categoria
                {
                    Nome = paranObj.Nome
                };             

                _service.Add(obj);
                response.Data = _mapper.Map<CategoriaViewModel>(obj);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, paranObj), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        public async Task<BaseResponse<CategoriaViewModel>> Update(CategoriaViewModel paranObj)
        {
            var response = new BaseResponse<CategoriaViewModel>();

            try
            {
                if (paranObj.Nome is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.Nome));
                    return response;
                }

                var existingObj = _service.Get(x => x.Id == new Categoria { Key = paranObj.Key }.Id).FirstOrDefault();
                if (existingObj == null)
                {
                    response.AddError(Events.INVALID_VALUE, "Categoria");
                    return response;
                }

                existingObj.Nome = paranObj.Nome;
                existingObj.AtualizadoEm = DateTime.Now;
                _service.Update(existingObj);
                response.Data = _mapper.Map<CategoriaViewModel>(existingObj);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, paranObj), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        public async Task<BaseResponse<CategoriaViewModel>> Remove(string key)
        {
            var response = new BaseResponse<CategoriaViewModel>();

            try
            {
                if (key == "")
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, key));
                    return response;
                }

                var obj = new Categoria { Key = key };
                if (obj.Id == 0)
                    obj = new Categoria { Key = key };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    return response;
                }

                _service.Remove(existingObj);

                response.Data = _mapper.Map<CategoriaViewModel>(existingObj);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, key), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            return response;
        }
    }
}
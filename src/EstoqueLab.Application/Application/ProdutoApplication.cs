using AutoLab.Application.ViewModel;
using AutoMapper;
using EstoqueLab.Application.Interfaces;
using EstoqueLab.Application.Parameters;
using EstoqueLab.Domain.Entities;
using EstoqueLab.Domain.Interfaces.Services;
using EstoqueLab.Uteis.Events;
using EstoqueLab.Uteis.Http.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueLab.Application.Application
{
    public class ProdutoApplication : IProdutoApplication
    {
        private readonly IProdutoService _service;
        private readonly ILogger<ProdutoApplication> _logger;
        private readonly IMapper _mapper;

        public ProdutoApplication(IProdutoService service,
            ILogger<ProdutoApplication> logger,
            IMapper mapper)
        {
            this._service = service;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<ProdutoViewModel>>> ListProdutoAsync(ProdutoParams paran)
        {
            var response = new BasePaginationResponse<IEnumerable<ProdutoViewModel>>();

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

                response.Data = _mapper.Map<IEnumerable<ProdutoViewModel>>(obj);
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

        public async Task<BaseResponse<IEnumerable<ProdutoViewModel>>> ListProdutoAsync()
        {
            var response = new BaseResponse<IEnumerable<ProdutoViewModel>>();

            try
            {

                var obj = _service.GetAll();

                response.Data = _mapper.Map<IEnumerable<ProdutoViewModel>>(obj);
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

        public async Task<BaseResponse<ProdutoViewModel>> Create(ProdutoViewModel paranObj)
        {
            var response = new BaseResponse<ProdutoViewModel>();

            try
            {
                if (paranObj.Nome is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.Nome));
                    return response;
                }

                if (paranObj.Categoria is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.Categoria));
                    return response;
                }

                var existingObj = _service.Get(x => x.Nome == paranObj.Nome).ToList();

                if (existingObj != null && existingObj.Count > 0)
                {
                    response.AddError(Events.INVALID_VALUE, "Produto");
                    return response;
                }

                var obj = new Produto
                {
                    Nome = paranObj.Nome,
                    CategoriaId = new Categoria { Key=paranObj.CategoriaKey}.Id,
                    Valor = paranObj.Valor
                };

                _service.Add(obj);
                response.Data = _mapper.Map<ProdutoViewModel>(obj);
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

        public async Task<BaseResponse<ProdutoViewModel>> Update(ProdutoViewModel paranObj)
        {
            var response = new BaseResponse<ProdutoViewModel>();

            try
            {
                if (paranObj.Nome is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.Nome));
                    return response;
                }
                if (paranObj.CategoriaKey is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.CategoriaKey));
                    return response;
                }

                var existingObj = _service.Get(x => x.Id == new Produto { Key = paranObj.Key }.Id).FirstOrDefault();
                if (existingObj == null)
                {
                    response.AddError(Events.INVALID_VALUE, "Produto");
                    return response;
                }

                existingObj.Nome = paranObj.Nome;
                existingObj.CategoriaId = new Categoria { Key = paranObj.CategoriaKey }.Id;
                existingObj.Valor = paranObj.Valor;
                existingObj.AtualizadoEm = DateTime.Now;
                _service.Update(existingObj);
                response.Data = _mapper.Map<ProdutoViewModel>(existingObj);
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

        public async Task<BaseResponse<ProdutoViewModel>> Remove(string key)
        {
            var response = new BaseResponse<ProdutoViewModel>();

            try
            {
                if (key == "")
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, key));
                    return response;
                }

                var obj = new Produto { Key = key };
                if (obj.Id == 0)
                    obj = new Produto { Key = key };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    return response;
                }

                _service.Remove(existingObj);

                response.Data = _mapper.Map<ProdutoViewModel>(existingObj);
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
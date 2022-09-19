using AutoLab.Application.ViewModel;
using EstoqueLab.API.Controllers;
using EstoqueLab.Application.Interfaces;
using EstoqueLab.Application.Parameters;
using EstoqueLab.Uteis.Http.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstoqueLab.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : BaseController
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoApplication _pageApplication;

        public ProdutoController(
            ILogger<ProdutoController> logger,
            IProdutoApplication PageApplication)
        {
            this._logger = logger;
            this._pageApplication = PageApplication;
        }

        /// <summary>
        /// Listar Produto
        /// </summary>
        /// <response code="200">Produtos.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<ProdutoViewModel>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "ProdutoAsync")]
        public async Task<IActionResult> ProdutoAsync([FromQuery] ProdutoParams Produto)
        {
            try
            {
                var result = await _pageApplication.ListProdutoAsync(Produto);
                return new OkObjectResult(result);

            }
            catch (Exception ex)
            {
                var result = new BaseResponse<Object>
                {
                    Data = null,
                    Success = false,
                    Error = true,
                };
                result.AddError(ex.Message);
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Create Produto
        /// </summary>
        /// <response code="200">Produtoe criado com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "ProdutoCreate")]
        public async Task<IActionResult> Create([FromBody] ProdutoViewModel create)
        {
            try
            {
                var result = await _pageApplication.Create(create);
                return new OkObjectResult(result);

            }
            catch (Exception ex)
            {
                var result = new BaseResponse<Object>
                {
                    Data = null,
                    Success = false,
                    Error = true,
                };
                result.AddError(ex.Message);
                return BadRequest(result);
            }
        }


        /// <summary>
        /// Update Produto
        /// </summary>
        /// <response code="200">Produtoe alterada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "ProdutoUpdate")]
        public async Task<IActionResult> Update([FromBody] ProdutoViewModel update)
        {
            try
            {
                var result = await _pageApplication.Update(update);
                return new OkObjectResult(result);

            }
            catch (Exception ex)
            {
                var result = new BaseResponse<Object>
                {
                    Data = null,
                    Success = false,
                    Error = true,
                };
                result.AddError(ex.Message);
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Remover Produto
        /// </summary>
        /// <response code="200">Produtoe removida com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "ProdutoRemove")]
        public async Task<IActionResult> Remove(string key)
        {
            try
            {
                var result = await _pageApplication.Remove(key);
                return new OkObjectResult(result);

            }
            catch (Exception ex)
            {
                var result = new BaseResponse<Object>
                {
                    Data = null,
                    Success = false,
                    Error = true,
                };
                result.AddError(ex.Message);
                return BadRequest(result);
            }
        }
    }
}
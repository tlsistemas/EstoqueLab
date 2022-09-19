using AutoLab.Application.ViewModel;
using EstoqueLab.Application.Interfaces;
using EstoqueLab.Application.Parameters;
using EstoqueLab.Uteis.Http.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstoqueLab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : BaseController
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ICategoriaApplication _pageApplication;

        public CategoriaController(
            ILogger<CategoriaController> logger,
            ICategoriaApplication PageApplication)
        {
            this._logger = logger;
            this._pageApplication = PageApplication;
        }

        /// <summary>
        /// Listar Categoria
        /// </summary>
        /// <response code="200">Categorias.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<CategoriaViewModel>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "CategoriaAsync")]
        public async Task<IActionResult> CategoriaAsync([FromQuery] CategoriaParams Categoria)
        {
            try
            {
                var result = await _pageApplication.ListCategoriaAsync(Categoria);
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
        /// Create Categoria
        /// </summary>
        /// <response code="200">Categoriae criado com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "CategoriaCreate")]
        public async Task<IActionResult> Create([FromBody] CategoriaViewModel create)
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
        /// Update Categoria
        /// </summary>
        /// <response code="200">Categoriae alterada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "CategoriaUpdate")]
        public async Task<IActionResult> Update([FromBody] CategoriaViewModel update)
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
        /// Remover Categoria
        /// </summary>
        /// <response code="200">Categoriae removida com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "CategoriaRemove")]
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

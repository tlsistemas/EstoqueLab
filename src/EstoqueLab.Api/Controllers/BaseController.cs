using EstoqueLab.Uteis.Http.Response;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
        public IActionResult BaseResponse<T>(BaseResponse<T> response)
        {
            return StatusCode((int)response.GetStatusCode(), response);
        }
    }
}

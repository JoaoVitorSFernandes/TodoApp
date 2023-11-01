using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("")]
    public sealed class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
            => StatusCode(200);
    }
}
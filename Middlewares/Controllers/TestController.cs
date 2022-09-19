using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Middlewares.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public int get()
        {
            int a = 1;
            int b = 0;

            int c = a / b;

            return c;
        }
    }
}

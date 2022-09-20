using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middlewares.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        [HttpGet]
        public String Get()
        {
            //throw new Exception("test hatası!");

            return "ok";
        }


        [HttpGet("Student")]
        public Student get()
        {
            return new Student() { Id = 1, Name = "nana" };
        }

        [HttpPost("Student")]
        public String CreateStudent([FromBody] Student student)
        {
            return "Öğrenci oluşturuldu";
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Middlewares
{
    public class RequestHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestHandlerMiddleware> _logger;

        public RequestHandlerMiddleware(RequestDelegate next, ILogger<RequestHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Request

            //Orjinal stream'ın yedeğini al
            var originalBodyStream = httpContext.Response.Body;

            _logger.LogError($"Query keys: {httpContext.Request.QueryString}");
            

            MemoryStream requestBody= new MemoryStream();
            await httpContext.Request.Body.CopyToAsync(requestBody);
            requestBody.Seek(0,SeekOrigin.Begin);
            String requestText = await new StreamReader(requestBody).ReadToEndAsync();
            requestBody.Seek(0, SeekOrigin.Begin);



            var tempStream = new MemoryStream();
            httpContext.Response.Body = tempStream;

            await _next.Invoke(httpContext); //response burada oluşuyor

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            String responseText = await new StreamReader(httpContext.Response.Body,Encoding.UTF8).ReadToEndAsync();
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            await httpContext.Response.Body.CopyToAsync(originalBodyStream);

            _logger.LogInformation($"Request: {requestText}");
            _logger.LogInformation($"Response: {responseText}");
        }
    }
}

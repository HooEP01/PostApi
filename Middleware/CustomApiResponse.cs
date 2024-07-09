using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PostApi.Middleware
{
    public class CustomApiResponseMiddleware(RequestDelegate next, ILogger<CustomApiResponseMiddleware> logger, IActionDescriptorCollectionProvider actionDescriptorProvider)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<CustomApiResponseMiddleware> _logger = logger;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorProvider = actionDescriptorProvider;

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBody = context.Response.Body;
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await _next(context);

                memoryStream.Seek(0, SeekOrigin.Begin);
                var originalContent = new StreamReader(memoryStream).ReadToEnd();

                var response = new ApiResponse<object>
                {
                    Success = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
                    Data = originalContent,
                };

                var json = JsonSerializer.Serialize(response);
                var jsonBytes = Encoding.UTF8.GetBytes(json);

                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.WriteAsync(jsonBytes);
                memoryStream.Seek(0, SeekOrigin.Begin);

                await memoryStream.CopyToAsync(originalBody);
            }

            context.Response.Body = originalBody;
            context.Response.ContentType = "application/json";
        }


        public class ApiResponse<T>
        {
            public bool Success { get; set; }
            public required T Data { get; set; }
            // Add other properties as needed
        }
    }
}
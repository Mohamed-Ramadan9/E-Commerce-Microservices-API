using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace APiGateway_Presentation.Middleware
{
    public class AttatchedSignatureRequest
    {
        private readonly RequestDelegate _next;

        public AttatchedSignatureRequest(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers["Api-Gateway"] = "Signed";
            await _next(context);
        }
    }
}

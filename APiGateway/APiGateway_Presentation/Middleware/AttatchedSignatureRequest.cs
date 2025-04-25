namespace APiGateway_Presentation.Middleware
{
    public class AttatchedSignatureRequest(RequestDelegate next)
    {
        public async Task InvokedAsync(HttpContext context)
        {
            context.Request.Headers["Api-Gateway"] = "Signed";
            await next(context);
        }
    }
}

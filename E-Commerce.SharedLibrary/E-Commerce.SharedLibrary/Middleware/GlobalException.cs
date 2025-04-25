
using System.Net;
using System.Text.Json;
using E_Commerce.SharedLibrary.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.SharedLibrary.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            // Declare default variables

            string message = "sorry, internal server error occured. Kindly try again";
            int statusCode = (int) HttpStatusCode.InternalServerError;
            string title = "Error";

            try
            {
                await next(context);

                // check if Response is Too Many Request 429 status code.
                if(context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "Warning";
                    message = "Too many Request made";
                    statusCode = (int) StatusCodes.Status429TooManyRequests;
                    await ModifiyHeader(context, title, message , statusCode);
                    
                }
                // check if Response UnAuthorized 401 status code.
                if(context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "Alert";
                    message = "You are not Authorized to access";
                    await ModifiyHeader(context, title,message , statusCode);
                }
                // check if Response is Forbidden 403 status code.
                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    title = "Out of Access";
                    message = "You are not Allowed to access";
                    statusCode = StatusCodes.Status403Forbidden;
                    await ModifiyHeader(context, title, message, statusCode);
                }
            }
            catch (Exception ex)
            {
                // Log Original Exceptions 
                LogException.LogExceptions(ex);
                // check if Exception is Timeout 408 status code.
                if(ex is TaskCanceledException || ex is TimeoutException)
                {
                    message = "Request timeout ... try again ";
                    title = "Out of time";
                    statusCode= (int) StatusCodes.Status408RequestTimeout;
                }
                // if none of the exceptions then do the default
                await ModifiyHeader(context, title, message, statusCode);
            }
        }

        private static async Task ModifiyHeader(HttpContext context, string title, string message, int statusCode)
        {
            //display  message to client
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync( JsonSerializer.Serialize(new ProblemDetails()
            { 
            
                Detail = message ,
                Status = statusCode ,
                Title  = title
            
            
            }),CancellationToken.None);
            return;
        }
    }
}

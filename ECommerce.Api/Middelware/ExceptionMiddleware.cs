using ECommerce.Business.ExceptionHandlers;
using System.Net;

namespace ECommerce.Api.Middelware
{

    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AppBadRequestException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new
                {
                    status = 400,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    status = 500,
                    message = ex.Message.ToString()
                });
            }
        }
    }
}

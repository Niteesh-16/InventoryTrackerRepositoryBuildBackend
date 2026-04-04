using System.Net;
using System.Text.Json;


namespace InventoryTracker.API.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        public ExceptionMiddleware (RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch(Exception ex)
            {
                await HandleException(context, ex);

            }
        }
        private static Task HandleException(HttpContext context , Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex.Message switch
            {
                "Invalid credentials" => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };
            var response =new
            {
                message = ex.Message
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

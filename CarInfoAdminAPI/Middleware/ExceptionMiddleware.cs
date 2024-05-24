using CarInfo.DataAccess.Persistence.Exceptions;
using System.Net;

namespace CarInfoAdminAPI.Middleware
{
    public class ExceptionMiddleware
    {
        /// <summary>
        /// // middleware for handling exception and generating consistent error reponses
        /// </summary>
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public ExceptionMiddleware(RequestDelegate requestDelegate, IConfiguration configuration)
        {
            _next = requestDelegate;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        /// <summary>
        /// Method to handle exception and send consistent error reponses
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns>return the Exception Responses</returns>
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var apiId = _configuration["ApiSetting:Api_id"];
            ExceptionModelDTO exResponse = new ExceptionModelDTO();

            //Swtich statement to handle the specific expception types
            switch (exception)
            {
                case BadRequestException badRequestException:

                    exResponse.Api_id = apiId;
                    exResponse.Response_code = (int)HttpStatusCode.BadRequest;
                    exResponse.Response_message = !string.IsNullOrEmpty(badRequestException.Message) ? badRequestException.Message : "Internal application server error";
                    exResponse.dateTime = DateTime.Now;
                    break;

                case NotFoundException notFoundException:

                    exResponse.Api_id = apiId;
                    exResponse.Response_code = (int)HttpStatusCode.NotFound;
                    exResponse.Response_message = notFoundException.Message;
                    exResponse.dateTime = DateTime.Now;
                    break;

                default:

                    // Default case for other exceptions
                    exResponse.Api_id = apiId;
                    exResponse.Response_code = (int)statusCode;
                    exResponse.Response_message = exception.Message;
                    exResponse.dateTime = DateTime.Now;
                    break;

            }
            //Set the Http Status code for the response and write the exception as JSON to the response stream
            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(exResponse);
        }
    }
}


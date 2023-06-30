using Domain.Abstract;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Domain.Middlewares
{
    public class ExceptionMiddleware
    {
        #region Properties
        private const string APPLICATION_JSON = "application/json";
        private readonly RequestDelegate _next;
        #endregion

        #region Builder
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Implementation
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (BusinessException ex)
            {
                SetResponse(context, ex.StatusCode, ex.Message, ex.Errors?.ToArray(), null);
            }
            catch (System.Exception ex)
            {
#if RELEASE
                SetResponse(context, 500,"Error desconocido", new string[] { ex.Message }, ex);
#else
                SetResponse(context, 500, ex.Message, null, ex);
#endif
            }
        }
        #endregion

        #region Privates
        private static async void SetResponse(HttpContext context,
            int code, string description,
            string[]? errors, Exception ex)
        {
            context.Response.Clear();
            context.Response.StatusCode = code;
            context.Response.ContentType = APPLICATION_JSON;

#if RELEASE
        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
        {
            StatusCode = code,
            Message = description,
            Errors = errors
        }, new JsonSerializerSettings 
    { 
        ContractResolver = new CamelCasePropertyNamesContractResolver() 
    }));
#else
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                StatusCode = code,
                Message = description,
                Errors = errors,
                Trace = ex.ToDetailedString()
            }, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
#endif
        }
        #endregion
    }
}

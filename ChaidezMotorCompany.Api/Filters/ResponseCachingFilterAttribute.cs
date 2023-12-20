using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace ChaidezMotorCompany.Api;

public class ResponseCachingFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var request = context.HttpContext.Request;
        var response = context.HttpContext.Response;

        if(request.Method == HttpMethod.Get.Method && response.StatusCode == (int)HttpStatusCode.OK && context.Result is OkObjectResult)
        {
            CreateResponseCaching(context);
        }
    }

    private static void CreateResponseCaching(ActionExecutedContext executedContext)
    {
        if(executedContext.Result == null) return;

        var response =  executedContext.HttpContext.Response;

        string result = JsonSerializer.Serialize((executedContext.Result as ObjectResult).Value);

        var etag = GetHash(result);
        var dateNow = DateTime.UtcNow;

        if(response.Headers.ContainsKey(HeaderNames.CacheControl))
        {
            var cacheControl = response.Headers[HeaderNames.CacheControl];
            var cacheControlHeaders = System.Net.Http.Headers.CacheControlHeaderValue.Parse(cacheControl);
            if(cacheControlHeaders.MaxAge != null)
            {
                DateTime expireDate = dateNow.AddSeconds(cacheControlHeaders.MaxAge.Value.TotalSeconds);
                response.Headers.Append(HeaderNames.Expires, expireDate.ToString("yyyy-MM=dd HH:mm:ss \"GMT\"zzz"));
            }
            cacheControlHeaders.MustRevalidate = true;
            response.Headers[HeaderNames.CacheControl] = cacheControlHeaders.ToString();
        }

        response.Headers.Append(HeaderNames.LastModified, dateNow.ToString("yyyy-MM=dd HH:mm:ss \"GMT\"zzz"));
        response.Headers.Append(HeaderNames.ETag, etag);
    }

    private static string GetHash(string input)
    {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = MD5.HashData(inputBytes);

        return $"\"{Convert.ToHexString(hashBytes)}\"";
    }
}

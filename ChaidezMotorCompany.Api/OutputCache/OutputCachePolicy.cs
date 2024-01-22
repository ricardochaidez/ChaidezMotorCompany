using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Primitives;

namespace ChaidezMotorCompany.Api;

public class OutputCachePolicy : IOutputCachePolicy
{
    private readonly string _policyName;
    private readonly TimeSpan? _duration;
    public OutputCachePolicy(string policyName, TimeSpan? duration)
    {
        _policyName = policyName;
        _duration = duration ?? TimeSpan.FromHours(1);
    }
    public ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        var attempOutputCahing = AttempOutputCahing(context);
        context.EnableOutputCaching = true;
        context.AllowCacheLookup = attempOutputCahing;
        context.AllowCacheStorage = attempOutputCahing;
        context.AllowLocking = true;
        context.Tags.Add(_policyName);
        context.ResponseExpirationTimeSpan = _duration;
        context.CacheVaryByRules.QueryKeys = "*";
        return ValueTask.CompletedTask;
    }

    private static bool AttempOutputCahing(OutputCacheContext context)
    {
        var request = context.HttpContext.Request;
        if(!HttpMethods.IsGet(request.Method) && !HttpMethods.IsHead(request.Method))
        {
            return false;
        }
        if(!StringValues.IsNullOrEmpty(request.Headers.Authorization) || request.HttpContext.User?.Identity?.IsAuthenticated == true)
        {
            return false;
        }
        return true;
    }

    public ValueTask ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellation) => ValueTask.CompletedTask;
    public ValueTask ServeResponseAsync(OutputCacheContext context, CancellationToken cancellation) => ValueTask.CompletedTask;
}
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IMemoryCache _cache;

    public CachingBehavior(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cacheKey = GenerateCacheKey(request);
        if (_cache.TryGetValue(cacheKey, out TResponse response))
        {
            return response;
        }

        response = await next();

        _cache.Set(cacheKey, response, TimeSpan.FromMinutes(5));

        return response;
    }

    private string GenerateCacheKey(TRequest request)
    {
        return $"{request.GetType().Name}-{JsonConvert.SerializeObject(request)}";
    }
}
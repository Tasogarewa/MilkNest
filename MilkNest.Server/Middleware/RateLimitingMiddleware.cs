public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly Dictionary<string, DateTime> _requestTimes = new();
    private const int _requestLimit = 60;
    private static readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(1); 

    public RateLimitingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var clientIp = context.Connection.RemoteIpAddress.ToString();

        lock (_requestTimes)
        {
            if (_requestTimes.TryGetValue(clientIp, out var lastRequestTime))
            {
                if (DateTime.UtcNow - lastRequestTime < _timeWindow)
                {
                    return; 
                }
                _requestTimes[clientIp] = DateTime.UtcNow;
            }
            else
            {
                _requestTimes[clientIp] = DateTime.UtcNow;
            }
        }

        await _next(context);
    }
}
using MediatR;

public class RetryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly int _retryCount;

    public RetryBehavior(int retryCount)
    {
        _retryCount = retryCount;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        int retryAttempt = 0;
        while (true)
        {
            try
            {
                return await next();
            }
            catch (Exception ex) when (retryAttempt < _retryCount)
            {
                retryAttempt++;
                await Task.Delay(TimeSpan.FromSeconds(2 * retryAttempt), cancellationToken);
            }
        }
    }
}
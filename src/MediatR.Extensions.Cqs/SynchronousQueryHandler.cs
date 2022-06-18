namespace MediatR.Extensions.Cqs;

/// <summary>
/// Wrapper class for a handler that synchronously handles a query and returns a response
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public abstract class SynchronousQueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<TResponse> IQueryHandler<TQuery, TResponse>.Handle(TQuery query) => Task.FromResult(Handle(query));

    /// <summary>
    /// Synchronously handle a query.
    /// </summary>
    /// <param name="query">Query</param>
    /// <returns>Response</returns>
    protected abstract TResponse Handle(TQuery query);
}
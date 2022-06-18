namespace MediatR.Extensions.Cqs;

/// <summary>
/// Defines a handler for a query
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    /// <summary>
    /// Handle a query.
    /// </summary>
    /// <param name="query">Query</param>
    /// <returns>Response</returns>
    public Task<TResponse> Handle(TQuery query);

    Task<TResponse> IRequestHandler<TQuery, TResponse>.Handle(TQuery query, CancellationToken _)
        => Handle(query);
}
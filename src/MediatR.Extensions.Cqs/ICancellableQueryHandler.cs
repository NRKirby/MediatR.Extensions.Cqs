namespace MediatR.Extensions.Cqs;

/// <summary>
/// Defines a handler for a query that can be cancelled by caller.
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public interface ICancellableQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    /// <summary>
    /// Handle a query.
    /// Provides a <see cref="CancellationToken"></see> to allow cancelling current handling process.
    /// </summary>
    /// <param name="query">Query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response</returns>
    public new Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);

    Task<TResponse> IRequestHandler<TQuery, TResponse>.Handle(TQuery query, CancellationToken cancellationToken)
        => Handle(query, cancellationToken);
}
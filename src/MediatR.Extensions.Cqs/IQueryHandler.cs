namespace MediatR.Extensions.Cqs;

/// <summary>
/// Defines a handler for a query
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IRequest<TResponse>
{
    /// <summary>
    /// Handles a query
    /// </summary>
    /// <param name="query">The query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the query</returns>
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
}


/// <summary>
/// Defines a handler for a query with a void (<see cref="Unit" />) response.
/// You do not need to register this interface explicitly with a container as it inherits from the base <see cref="IQueryHandler{TQuery, TResponse}" /> interface.
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, Unit>
    where TQuery : IRequest<Unit>
{
}

/// <summary>
/// Wrapper class for a handler that asynchronously handles a query and does not return a response
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
public abstract class AsyncQueryHandler<TQuery> : IRequestHandler<TQuery>
    where TQuery : IQuery
{
    async Task<Unit> IRequestHandler<TQuery, Unit>.Handle(TQuery query, CancellationToken cancellationToken)
    {
        await Handle(query, cancellationToken).ConfigureAwait(false);
        return Unit.Value;
    }

    /// <summary>
    /// Override in a derived class for the handler logic
    /// </summary>
    /// <param name="query">Query</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Response</returns>
    protected abstract Task Handle(TQuery query, CancellationToken cancellationToken);
}

/// <summary>
/// Wrapper class for a handler that synchronously handles a query and returns a response
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public abstract class QueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<TResponse> IRequestHandler<TQuery, TResponse>.Handle(TQuery query, CancellationToken cancellationToken)
        => Task.FromResult(Handle(query));

    /// <summary>
    /// Override in a derived class for the handler logic
    /// </summary>
    /// <param name="query">Query</param>
    /// <returns>Response</returns>
    protected abstract TResponse Handle(TQuery query);
}

/// <summary>
/// Wrapper class for a handler that synchronously handles a query does not return a response
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
public abstract class QueryHandler<TQuery> : IRequestHandler<TQuery>
    where TQuery : IQuery
{
    Task<Unit> IRequestHandler<TQuery, Unit>.Handle(TQuery query, CancellationToken cancellationToken)
    {
        Handle(query);
        return Unit.Task;
    }

    protected abstract void Handle(TQuery query);
}
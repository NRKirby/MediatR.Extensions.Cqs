namespace MediatR.Extensions.Cqs;

/// <summary>
/// Defines a handler for a command
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : IRequest<TResponse>
{
    /// <summary>
    /// Handles a command
    /// </summary>
    /// <param name="command">The command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the command</returns>
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}


/// <summary>
/// Defines a handler for a command with a void (<see cref="Unit" />) response.
/// You do not need to register this interface explicitly with a container as it inherits from the base <see cref="ICommandHandler{TCommand, TResponse}" /> interface.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : IRequest<Unit>
{
}

/// <summary>
/// Wrapper class for a handler that asynchronously handles a command and does not return a response
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
public abstract class AsyncCommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
    async Task<Unit> IRequestHandler<TCommand, Unit>.Handle(TCommand command, CancellationToken cancellationToken)
    {
        await Handle(command, cancellationToken).ConfigureAwait(false);
        return Unit.Value;
    }

    /// <summary>
    /// Override in a derived class for the handler logic
    /// </summary>
    /// <param name="command">Command</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Response</returns>
    protected abstract Task Handle(TCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Wrapper class for a handler that synchronously handles a command and returns a response
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public abstract class CommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<TResponse> IRequestHandler<TCommand, TResponse>.Handle(TCommand command, CancellationToken cancellationToken)
        => Task.FromResult(Handle(command));

    /// <summary>
    /// Override in a derived class for the handler logic
    /// </summary>
    /// <param name="command">Command</param>
    /// <returns>Response</returns>
    protected abstract TResponse Handle(TCommand command);
}

/// <summary>
/// Wrapper class for a handler that synchronously handles a command and does not return a response
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand<Unit>
{
    Task<Unit> IRequestHandler<TCommand, Unit>.Handle(TCommand command, CancellationToken cancellationToken)
    {
        Handle(command);
        return Unit.Task;
    }

    protected abstract void Handle(TCommand command);
}
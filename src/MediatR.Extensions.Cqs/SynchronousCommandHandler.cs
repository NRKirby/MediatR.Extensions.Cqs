namespace MediatR.Extensions.Cqs;

/// <summary>
/// Wrapper class for a handler that synchronously handles a command and returns a response
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public abstract class SynchronousCommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<TResponse> ICommandHandler<TCommand, TResponse>.Handle(TCommand command)
        => Task.FromResult(Handle(command));

    /// <summary>
    /// Override in a derived class for the handler logic
    /// Handles a command
    /// </summary>
    /// <param name="command">Command</param>
    /// <returns>Response</returns>
    protected abstract TResponse Handle(TCommand command);
}

/// <summary>
/// Wrapper class for a handler that synchronously handles a command and does not return a response
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
public abstract class SynchronousCommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task ICommandHandler<TCommand>.Handle(TCommand command)
    {
        Handle(command);
        return Task.CompletedTask;
    }

    protected abstract void Handle(TCommand command);
}
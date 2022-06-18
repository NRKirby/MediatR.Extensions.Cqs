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
    /// <returns>Response from the command</returns>
    Task<TResponse> Handle(TCommand command);

    Task<TResponse> IRequestHandler<TCommand, TResponse>.Handle(TCommand request, CancellationToken _)
        => Handle(request);
}

/// <summary>
/// Defines a handler for a command with a void (<see cref="Unit" />) response.
/// You do not need to register this interface explicitly with a container as it inherits from the base <see cref="ICommandHandler{TCommand, TResponse}" /> interface.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : IRequest<Unit>
{
    /// <summary>
    /// Handles a command
    /// </summary>
    /// <param name="command">The command</param>
    /// <returns>Response from the command</returns>
    Task Handle(TCommand command);

    async Task<Unit> IRequestHandler<TCommand, Unit>.Handle(TCommand request, CancellationToken _)
    {
        await Handle(request);

        return Unit.Value;
    }
}


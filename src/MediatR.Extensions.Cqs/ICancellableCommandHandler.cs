namespace MediatR.Extensions.Cqs;

/// <summary>
/// Defines a handler for a command, providing also a <see cref="CancellationToken"></see> to allow cancelling current handling process.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
public interface ICancellableCommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : IRequest<Unit>
{
    public new Task Handle(TCommand command, CancellationToken cancellationToken);

    async Task<Unit> IRequestHandler<TCommand, Unit>.Handle(TCommand request, CancellationToken cancellationToken)
    {
        await Handle(request, cancellationToken);

        return Unit.Value;
    }
}

/// <summary>
/// Defines a handler for a command, providing also a <see cref="CancellationToken"></see> to allow cancelling current handling process.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public interface ICancellableCommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : IRequest<TResponse>
{
    public new Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);

    Task<TResponse> IRequestHandler<TCommand, TResponse>.Handle(TCommand command, CancellationToken cancellationToken)
        => Handle(command, cancellationToken);
}
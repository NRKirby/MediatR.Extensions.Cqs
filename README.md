# MediatR.Extensions.Cqs

Adds CQS semantics over MediatR's request abstraction. To use, simply define query or command classes that implement `IQuery` or `ICommand` respectively

```csharp
public record PingQuery(string Message) : IQuery<Pong>;

public record PingCommand(string Message) : ICommand<Pong>;
```

Then define their respective handlers

```csharp
public class PingQueryHandler : IQueryHandler<PingQuery, Pong>
{
    public Task<Pong> Handle(PingQuery query, CancellationToken cancellationToken)
        => Task.FromResult(new Pong($"{query.Message} Pong"));
}


public class PingCommandHandler : ICommandHandler<PingCommand, Pong>
{
    public Task<Pong> Handle(PingCommand command, CancellationToken cancellationToken)
        => Task.FromResult(new Pong($"{command.Message} Pong"));
}
```
# MediatR.Extensions.Cqs

Adds [CQS](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation) semantics over MediatR's request abstraction. To use, simply define query or command classes that implement `IQuery` or `ICommand` respectively

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

## Installing MediatR.Extensions.Cqs

You find this package on [NuGet](https://www.nuget.org/packages/MediatR.Extensions.Cqs/)
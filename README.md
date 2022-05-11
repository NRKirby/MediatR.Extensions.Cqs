# MediatR.Extensions.Cqs

A simple library that adds [CQS](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation) semantics over MediatR's `IRequest` abstraction. 

## How can I use the library?

### Queries

```csharp

// Define a query that implements IQuery
public record Query(string Message) : IQuery<Pong>;

// Define a query handler that implements IQueryHandler
public class QueryHandler : IQueryHandler<PingQuery, Pong>
{
    public async Task<Pong> Handle(PingQuery query, CancellationToken cancellationToken)
        => await new Pong($"{query.Message} Pong");
}
```

### Commands

```csharp

// Define a command implements ICommand
public record Command(string Message) : ICommand<Pong>;

// Define a command handler that implements ICommandHandler
public class CommandHandler : ICommandHandler<PingCommand, Pong>
{
    public async Task<Pong> Handle(PingCommand command, CancellationToken cancellationToken)
        => await new Pong($"{command.Message} Pong");
}
```

## Installing MediatR.Extensions.Cqs

You find this package on [NuGet](https://www.nuget.org/packages/MediatR.Extensions.Cqs/)
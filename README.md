# MediatR.Extensions.Cqs

A simple library that adds [CQS](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation) semantics over MediatR's `IRequest` abstraction. 

## How can I use the library?

### Queries

```csharp

// Define a query

public record PingQuery(string Message) : IQuery<Pong>;

// Define a query handler

public class PingQueryHandler : IQueryHandler<PingQuery, Pong>
{
    public async Task<Pong> Handle(PingQuery query, CancellationToken cancellationToken)
        => await new Pong($"{query.Message} Pong");
}
```

### Commands

```csharp

// Define a command

public record PingCommand(string Message) : ICommand<Pong>;

// Define a command handler

public class PingCommandHandler : ICommandHandler<PingCommand, Pong>
{
    public async Task<Pong> Handle(PingCommand command, CancellationToken cancellationToken)
        => await new Pong($"{command.Message} Pong");
}
```

## Installing MediatR.Extensions.Cqs

You find this package on [NuGet](https://www.nuget.org/packages/MediatR.Extensions.Cqs/)
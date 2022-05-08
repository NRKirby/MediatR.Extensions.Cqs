using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace MediatR.Extensions.Cqs.Tests;

public class CommandHandlerTests
{
    [Fact]
    public async Task Command_WithResult_ShouldBeConnectedToCommandHandler()
    {
        var services = new ServiceCollection()
            .AddMediatR(typeof(PingCommand));

        var provider = services.BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();

        var result = await mediator.Send(new PingCommand("Ping"));

        result.ShouldNotBeNull();
        result.Message.ShouldBe("Ping Pong");
    }
    
    [Fact]
    public async Task Command_WithNoResult_ShouldBeConnectedToCommandHandler()
    {
        var services = new ServiceCollection()
            .AddMediatR(typeof(PingCommand));

        var provider = services.BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();

        var result = await mediator.Send(new PingVoidCommand("Ping"));

        result.ShouldBeAssignableTo<Unit>();
    }
}

public record PingCommand(string Message) : ICommand<Pong>;

public class PingCommandHandler : ICommandHandler<PingCommand, Pong>
{
    public Task<Pong> Handle(PingCommand command, CancellationToken cancellationToken)
        => Task.FromResult(new Pong($"{command.Message} Pong"));
}

public record PingVoidCommand(string Message) : ICommand;

public class PingVoidCommandHandler : ICommandHandler<PingVoidCommand>
{
    public Task<Unit> Handle(PingVoidCommand request, CancellationToken cancellationToken)
        => Task.FromResult(Unit.Value);
}
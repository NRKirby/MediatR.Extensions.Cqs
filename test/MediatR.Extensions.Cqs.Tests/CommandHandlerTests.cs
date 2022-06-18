using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MediatR.Extensions.Cqs.Tests;

public class CommandHandlerTests : TestBase
{
    private readonly IMediator _mediator;

    public CommandHandlerTests()
    {
        _mediator = GetMediator();
    }
    
    [Fact]
    public async Task Command_WithResult_ShouldBeConnectedToCommandHandler()
    {
        var result = await _mediator.Send(new PingCommand("Ping"));

        result.ShouldNotBeNull();
        result.Message.ShouldBe("Ping Pong");
    }
    
    [Fact]
    public async Task Command_WithNoResult_ShouldBeConnectedToCommandHandler()
    {
        var result = await _mediator.Send(new PingVoidCommand("Ping"));

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
    public Task Handle(PingVoidCommand request, CancellationToken cancellationToken)
        => Task.Delay(0, cancellationToken);
}
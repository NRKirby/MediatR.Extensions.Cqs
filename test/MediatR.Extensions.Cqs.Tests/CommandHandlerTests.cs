using System;
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

    [Fact]
    public async Task Cancellable_command_handler_with_result_gets_cancel_signal()
    {
        await SendCommandAndThenCancel(new LongPingCommand("Ping"));
    }

    [Fact]
    public async Task Cancellable_command_handler_gets_cancel_signal()
    {
        await SendCommandAndThenCancel(new LongPingVoidCommand("Ping"));
    }

    private async Task SendCommandAndThenCancel(IBaseCommand command)
    {
        using var tokenSource = new CancellationTokenSource();

        var task = _mediator.Send(command, tokenSource.Token);

        tokenSource.Cancel();

        Func<Task> waitingTask = async () => await task;

        await waitingTask.ShouldThrowAsync<TaskCanceledException>();
    }
}

public record PingCommand(string Message) : ICommand<Pong>;

public class PingCommandHandler : ICommandHandler<PingCommand, Pong>
{
    public Task<Pong> Handle(PingCommand command)
        => Task.FromResult(new Pong($"{command.Message} Pong"));
}

public record PingVoidCommand(string Message) : ICommand;

public class PingVoidCommandHandler : ICommandHandler<PingVoidCommand>
{
    public Task Handle(PingVoidCommand command)
        => Task.Delay(0);
}

public record LongPingCommand(string Message) : ICommand<Pong>;

public class LongPingCommandHandler : ICancellableCommandHandler<LongPingCommand, Pong>
{
    public async Task<Pong> Handle(LongPingCommand command, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
        
        return new Pong($"{command.Message} Pong");
    }
}
public record LongPingVoidCommand(string Message) : ICommand;

public class LongPingVoidCommandHandler : ICancellableCommandHandler<LongPingVoidCommand>
{
    public async Task Handle(LongPingVoidCommand command, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
    }
}
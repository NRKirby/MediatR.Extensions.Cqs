using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MediatR.Extensions.Cqs.Tests;

public class QueryHandlerTests : TestBase
{
    private readonly IMediator _mediator;

    public QueryHandlerTests()
    {
        _mediator = GetMediator();
    }
    
    [Fact]
    public async Task Query_WithResult_ShouldBeConnectedToQueryHandler()
    {
        var result = await _mediator.Send(new PingQuery("Ping"));

        result.ShouldNotBeNull();
        result.Message.ShouldBe("Ping Pong");
    }
    
    [Fact]
    public async Task Query_is_processed_by_synchronous_handler()
    {
        var result = await _mediator.Send(new GetUserDetails());

        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<UserDetails>();
    }

    [Fact]
    public async Task Cancellable_query_handler_gets_cancel_signal()
    {
        using var tokenSource = new CancellationTokenSource();

        var task = _mediator.Send(new GetHugeData(), tokenSource.Token);

        tokenSource.Cancel();

        Func<Task> waitingTask = async () => await task;

        await waitingTask.ShouldThrowAsync<TaskCanceledException>();
    }

    public record PingQuery(string Message) : IQuery<Pong>;
    public class PingQueryHandler : IQueryHandler<PingQuery, Pong>
    {
        public Task<Pong> Handle(PingQuery query)
            => Task.FromResult(new Pong($"{query.Message} Pong"));
    }

    public record GetUserDetails : IQuery<UserDetails>;
    private record UserDetails;
    private class GetUserDetailsHandler : SynchronousQueryHandler<GetUserDetails, UserDetails>
    {
        protected override UserDetails Handle(GetUserDetails query) => new();
    }

    public record GetHugeData : IQuery<HugeData>;
    private record HugeData;
    private class GetHugeDataHandler : ICancellableQueryHandler<GetHugeData, HugeData>
    {
        public async Task<HugeData> Handle(GetHugeData query, CancellationToken cancellationToken)
        {
            await Task.Delay(100, cancellationToken);

            return new HugeData();
        }
    }
}


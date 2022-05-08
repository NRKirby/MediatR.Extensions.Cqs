using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
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
    public async Task Query_WithNoResult_ShouldBeConnectedToQueryHandler()
    {
        var result = await _mediator.Send(new PingVoidQuery("Ping"));

        result.ShouldBeAssignableTo<Unit>();
    }
    
    public record PingQuery(string Message) : IQuery<Pong>;

    public class PingQueryHandler : IQueryHandler<PingQuery, Pong>
    {
        public Task<Pong> Handle(PingQuery query, CancellationToken cancellationToken)
            => Task.FromResult(new Pong($"{query.Message} Pong"));
    }

    public record PingVoidQuery(string Message) : IQuery;

    public class PingVoidQueryHandler : IQueryHandler<PingVoidQuery>
    {
        public Task<Unit> Handle(PingVoidQuery query, CancellationToken cancellationToken)
            => Task.FromResult(Unit.Value);
    }
}
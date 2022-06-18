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
    public async Task Synchronous_query_handler_is_correctly_called_by_mediator()
    {
        var result = await _mediator.Send(new GetUserDetails());

        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<UserDetails>();
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
}


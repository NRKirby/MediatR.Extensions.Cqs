using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MediatR.Extensions.Cqs.Tests;

public class RequestHandlerTests : TestBase
{
    private readonly IMediator _mediator;

    public RequestHandlerTests()
    {
        _mediator = GetMediator();
    }
    
    [Fact]
    public async Task Request_ShouldBeConnectedToRequestHandler()
    {
        var result = await _mediator.Send(new PingRequest("Ping"));

        result.ShouldNotBeNull();
        result.Message.ShouldBe("Ping Pong");
    }
    
    public record PingRequest(string Message) : IRequest<Pong>;

    public class PingRequestHandler : IRequestHandler<PingRequest, Pong>
    {
        public Task<Pong> Handle(PingRequest request, CancellationToken cancellationToken)
            => Task.FromResult(new Pong($"{request.Message} Pong"));
    }
}
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace MediatR.Extensions.Cqs.Tests;

public class RequestHandlerTests
{
    [Fact]
    public async Task Request_ShouldBeConnectedToRequestHandler()
    {
        var services = new ServiceCollection()
            .AddMediatR(typeof(PingRequest));

        var provider = services.BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();

        var result = await mediator.Send(new PingRequest("Ping"));

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
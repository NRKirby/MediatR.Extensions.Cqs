using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.Cqs.Tests;

public class TestBase
{
    protected static IMediator GetMediator()
    {
        var services = new ServiceCollection()
            .AddMediatR(typeof(RequestHandlerTests.PingRequest));

        var provider = services.BuildServiceProvider();

        return provider.GetRequiredService<IMediator>();
    }
}
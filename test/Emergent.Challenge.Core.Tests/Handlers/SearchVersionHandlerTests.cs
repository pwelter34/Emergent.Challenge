using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Emergent.Challenge.Core.Commands;
using Emergent.Challenge.Core.Handlers;
using Emergent.Challenge.Core.Services;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Emergent.Challenge.Core.Tests.Handlers
{
    public class SearchVersionHandlerTests
    {
        [Theory]
        [InlineData("8.0", 4)]
        [InlineData("2.0", 5)]
        [InlineData("0.6.0", 8)]
        public async Task SearchByVersionManual(string input, int count)
        {
            var versionMatcher = new VersionMatcher(NullLogger<VersionMatcher>.Instance);
            var handler = new SearchVersionHandler(NullLogger<SearchVersionHandler>.Instance, versionMatcher);

            var command = new SearchVersionCommand { Version = input };

            var results = await handler.Handle(command, CancellationToken.None);
            results.Should().NotBeNullOrEmpty();

            var list = results.ToList();
            list.Count.Should().Be(count);
        }

        [Theory]
        [InlineData("8.0", 4)]
        [InlineData("2.0", 5)]
        [InlineData("0.6.0", 8)]
        public async Task SearchByVersionServices(string input, int count)
        {
            var services = new ServiceCollection()
                .AddChallengeServices()
                .AddScoped(typeof(ILogger<>), typeof(NullLogger<>));

            var provider = services.BuildServiceProvider();

            var mediator = provider.GetRequiredService<IMediator>();

            var command = new SearchVersionCommand { Version = input };
            
            var results = await mediator.Send(command);

            results.Should().NotBeNullOrEmpty();

            var list = results.ToList();
            list.Count.Should().Be(count);
        }
    }
}

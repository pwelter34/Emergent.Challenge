using Emergent.Challenge.Core.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Emergent.Challenge.Core.Tests.Services
{
    public class VersionMatcherTests
    {
        [Theory]
        [InlineData("2.0", "2", false)]
        [InlineData("2.0.1", "2", true)]
        [InlineData("2.1", "2", true)]
        [InlineData("2.1.0", "2.0.1", true)]
        public void IsGreaterThan(string left, string right, bool expected)
        {
            var versionMatcher = new VersionMatcher(NullLogger<VersionMatcher>.Instance);
            var result = versionMatcher.IsGreaterThan(left, right);
            result.Should().Be(expected);
        }


        [Theory]
        [InlineData("2", "2.0.0.0", true)]
        [InlineData("2.1", "2.1.0.0", true)]
        [InlineData("2.0.1", "2.0.1.0", true)]
        [InlineData("2.3.1.4", "2.3.1.4", true)]
        [InlineData("13.2.1.", "13.2.1.0", true)]
        [InlineData("1...1", "1.0.0.1", true)]
        [InlineData("2.3.4.5.6", null, false)]
        [InlineData("a.b.c", null, false)]
        public void TryParse(string source, string output, bool parsed)
        {
            var versionMatcher = new VersionMatcher(NullLogger<VersionMatcher>.Instance);
            var result = versionMatcher.TryParse(source, out var version);
            result.Should().Be(parsed);

            var versionString = version?.ToString();
            versionString.Should().Be(output);
        }
    }
}

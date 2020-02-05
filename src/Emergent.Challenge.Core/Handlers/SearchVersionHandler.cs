using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Emergent.Challenge.Core.Commands;
using Emergent.Challenge.Core.Definitions;
using Emergent.Challenge.Core.Models;
using Emergent.Challenge.Core.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Emergent.Challenge.Core.Handlers
{
    public class SearchVersionHandler : IRequestHandler<SearchVersionCommand, IEnumerable<Software>>
    {
        private readonly ILogger<SearchVersionHandler> _logger;
        private readonly IVersionMatcher _versionMatcher;

        public SearchVersionHandler(ILogger<SearchVersionHandler> logger, IVersionMatcher versionMatcher)
        {
            _logger = logger;
            _versionMatcher = versionMatcher;
        }

        public Task<IEnumerable<Software>> Handle(SearchVersionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogTrace("Processing request '{request}' ...", request);
                var watch = Stopwatch.StartNew();

                var matched = SoftwareManager
                    .GetAllSoftware()
                    .Where(software => _versionMatcher.IsGreaterThan(software.Version, request.Version))
                    .ToList();

                watch.Stop();
                _logger.LogTrace("Processed request '{requestName}': {elapsed} ms", request, watch.ElapsedMilliseconds);

                return Task.FromResult<IEnumerable<Software>>(matched);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing request '{requestName}': {errorMessage}", request, ex.Message);
                throw;
            }
        }
    }
}

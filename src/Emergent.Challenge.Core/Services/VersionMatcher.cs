using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Emergent.Challenge.Core.Definitions;
using Microsoft.Extensions.Logging;

namespace Emergent.Challenge.Core.Services
{
    public class VersionMatcher : IVersionMatcher
    {
        public const string VersionPattern = @"^(\d+)(?:\.(\d*))?(?:\.(\d*))?(?:\.(\d*))?$";
        private static readonly Regex _versionRegex = new Regex(VersionPattern);

        private readonly ILogger<VersionMatcher> _logger;

        public VersionMatcher(ILogger<VersionMatcher> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Determine if left version string is greater than right version string.
        /// </summary>
        /// <param name="left">The left version string</param>
        /// <param name="right">The right version string</param>
        /// <returns><c>true</c> if left version string is greater than right version string; otherwise <c>false</c></returns>
        public bool IsGreaterThan(string left, string right)
        {
            // TODO: how should we handle invalid format? Log for now...
            if (!TryParse(left, out var leftVersion))
            {
                _logger.LogWarning("Invalid left version format: {left}", left);
                return false;
            }

            if (!TryParse(right, out var rightVersion))
            {
                _logger.LogWarning("Invalid right version format: {right}", right);
                return false;
            }

            return leftVersion > rightVersion;
        }

        /// <summary>
        /// Tries to convert the version string to an equivalent <see cref="Version"></see> object.
        /// </summary>
        /// <param name="input">A string that contains a version number to convert.</param>
        /// <param name="result">When this method returns, contains the <see cref="Version"></see> equivalent of the input string.</param>
        /// <returns><c>true</c> if the <paramref name="input">input</paramref> parameter was converted successfully; otherwise, <c>false</c>.</returns>
        /// <remarks>Using custom parse logic because Version.TryParse doesn't support single digit value.</remarks>
        public bool TryParse(string input, out Version result)
        {
            if (string.IsNullOrEmpty(input))
            {
                result = default;
                return false;
            }

            var match = _versionRegex.Match(input);
            if (!match.Success)
            {
                result = default;
                return false;
            }

            // version number supports 4 places
            var parts = new int[4] { 0, 0, 0, 0 };

            // don't loop more than 4 times, use 5 since skipping first index
            var groupsCount = Math.Min(match.Groups.Count, 5);
            
            // skip group 0 as its a the full match
            for (var index = 1; index < groupsCount; index++)
            {
                string value = match.Groups[index].Value;
                if (int.TryParse(value, out int part))
                {
                    parts[index - 1] = part;
                }
            }

            result = new Version(parts[0], parts[1], parts[2], parts[3]);
            return true;
        }
    }
}

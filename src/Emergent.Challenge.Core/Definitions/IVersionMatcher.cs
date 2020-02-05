using System;

namespace Emergent.Challenge.Core.Definitions
{
    public interface IVersionMatcher
    {
        bool IsGreaterThan(string left, string right);
        bool TryParse(string input, out Version result);
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Emergent.Challenge.Core.Models;
using Emergent.Challenge.Core.Services;
using MediatR;

namespace Emergent.Challenge.Core.Commands
{
    public class SearchVersionCommand : IRequest<IEnumerable<Software>>
    {
        [RegularExpression(VersionMatcher.VersionPattern, ErrorMessage = "Invalid Version Number")] 
        public string Version { get; set; }
    }
}

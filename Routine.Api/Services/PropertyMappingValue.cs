using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Routine.Api.Services
{
    public class PropertyMappingValue
    {
        public IEnumerable<string> DestinationProperties { get; set; }
        public bool Revert { get; set; }

        public PropertyMappingValue(IEnumerable<string> destinationProperty, bool revert = false)
        {
            DestinationProperties = destinationProperty ?? throw new ArgumentNullException(nameof(destinationProperty));
            Revert = revert;
        }
    }
}

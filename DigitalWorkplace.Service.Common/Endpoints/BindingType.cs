using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWorkplace.Service.Endpoints
{
    /// <summary>
    /// Defines the possible Http binding types.
    /// </summary>
    public enum HttpBindingType
    {
        /// <summary>
        /// Use the basic Http binding.
        /// </summary>
        BasicHttp,

        /// <summary>
        /// Use the WS-* Http binding.
        /// </summary>
        WSHttp
    }
}

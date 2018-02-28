using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWorkplace.Service.Common
{
    /// <summary>
    /// Defines the required contract for implementing hosted service metadata.
    /// </summary>
    public interface IHostedServiceMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the service type.
        /// </summary>
        Type ServiceType { get; }
        #endregion
    }
}

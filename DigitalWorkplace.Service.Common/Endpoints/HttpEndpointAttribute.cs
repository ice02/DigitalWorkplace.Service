﻿using DigitalWorkplace.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWorkplace.Service.Endpoints
{
    /// <summary>
    /// Describes an endpoint that supports the Http scheme.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class HttpEndpointAttribute : EndpointAttribute
    {
        #region Fields
        private const int DefaultPort = 50001;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="HttpEndpointAttribute"/>.
        /// </summary>
        public HttpEndpointAttribute() : base(DefaultPort)
        {
            EnableGet = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="HttpBindingType"/>.
        /// </summary>
        public HttpBindingType BindingType { get; set; }

        /// <summary>
        /// Gets or sets whether to use Https
        /// </summary>
        public bool UseHttps { get; set; }

        /// <summary>
        /// Gets or sets whether Http(s) GET is enabled.
        /// </summary>
        public bool EnableGet { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates an instance of a <see cref="ServiceEndpoint"/> that represents the endpoint.
        /// </summary>
        /// <param name="description">The contract description.</param>
        /// <param name="meta">The hosted service metadata.</param>
        /// <returns>An instance of <see cref="ServiceEndpoint"/></returns>
        internal override ServiceEndpoint CreateEndpoint(ContractDescription description, IHostedServiceMetadata meta)
        {
            var uri = CreateUri((UseHttps) ? "https" : "http", meta);
            var address = new EndpointAddress(uri);

            var binding = CreateBinding(BindingType);
            return new ServiceEndpoint(description, binding, address);
        }

        /// <summary>
        /// Creates the <see cref="Binding"/> used for the endpoint.
        /// </summary>
        /// <param name="bindingType">The binding type.</param>
        /// <returns>An instance of <see cref="Binding"/>.</returns>
        protected virtual Binding CreateBinding(HttpBindingType bindingType)
        {
            switch (bindingType)
            {
                case HttpBindingType.BasicHttp:
                    return (BindingConfiguration == null)
                               ? new BasicHttpBinding()
                               : new BasicHttpBinding(BindingConfiguration);
                case HttpBindingType.WSHttp:
                    return (BindingConfiguration == null)
                               ? new WSHttpBinding()
                               : new WSHttpBinding(BindingConfiguration);
                default:
                    throw new ArgumentNullException("Unsupported binding type: " + bindingType);
            }
        }

        /// <summary>
        /// Ensures the <see cref="ServiceDescription"/> has a metadata behaviour specified.
        /// </summary>
        /// <param name="description">The service description.</param>
        /// <returns>An instance of <see cref="ServiceMetadataBehavior"/>.</returns>
        private static ServiceMetadataBehavior EnsureServiceMetadataBehavior(ServiceDescription description)
        {
            var behaviour = description.Behaviors
                .OfType<ServiceMetadataBehavior>()
                .SingleOrDefault();

            if (behaviour == null)
            {
                behaviour = new ServiceMetadataBehavior();
                description.Behaviors.Add(behaviour);
            }

            return behaviour;
        }

        /// <summary>
        /// Make any endpoint-specific changes to the service description.
        /// </summary>
        /// <param name="description">The service description.</param>
        internal override void UpdateServiceDescription(ServiceDescription description)
        {
            var metaBehavior = EnsureServiceMetadataBehavior(description);
            if (EnableGet)
            {
                if (UseHttps)
                    metaBehavior.HttpsGetEnabled = true;
                else
                    metaBehavior.HttpGetEnabled = true;
            }
        }
        #endregion
    }
}

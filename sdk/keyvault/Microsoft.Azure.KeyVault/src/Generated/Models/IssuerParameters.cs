// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.KeyVault.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Parameters for the issuer of the X509 component of a certificate.
    /// </summary>
    public partial class IssuerParameters
    {
        /// <summary>
        /// Initializes a new instance of the IssuerParameters class.
        /// </summary>
        public IssuerParameters()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the IssuerParameters class.
        /// </summary>
        /// <param name="name">Name of the referenced issuer object or reserved
        /// names; for example, 'Self' or 'Unknown'.</param>
        /// <param name="certificateType">Type of certificate to be requested
        /// from the issuer provider.</param>
        /// <param name="certificateTransparency">Indicates if the certificates
        /// generated under this policy should be published to certificate
        /// transparency logs.</param>
        public IssuerParameters(string name = default(string), string certificateType = default(string), bool? certificateTransparency = default(bool?))
        {
            Name = name;
            CertificateType = certificateType;
            CertificateTransparency = certificateTransparency;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets name of the referenced issuer object or reserved
        /// names; for example, 'Self' or 'Unknown'.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets type of certificate to be requested from the issuer
        /// provider.
        /// </summary>
        [JsonProperty(PropertyName = "cty")]
        public string CertificateType { get; set; }

        /// <summary>
        /// Gets or sets indicates if the certificates generated under this
        /// policy should be published to certificate transparency logs.
        /// </summary>
        [JsonProperty(PropertyName = "cert_transparency")]
        public bool? CertificateTransparency { get; set; }

    }
}

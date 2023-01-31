// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-10-31 08:07
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 21:01
// ***********************************************************************
//  <copyright file="RemoteMSignOptions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

// ReSharper disable ClassNeverInstantiated.Global

#endregion

namespace MSignHelperDotNet.Models
{
    /// <summary>
    ///     Remote MSign option
    /// </summary>
    /// <remarks></remarks>
    public class RemoteMSignOptions
    {
        /// <summary>
        ///     Gets or sets link/address to remote site for redirect.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string RemoteRedirectAddress { get; set; }

        /// <summary>
        ///     Gets or sets link/address to remote service.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string RemoteServiceClientAddress { get; set; }

        /// <summary>
        ///     Path of service certificate.
        /// </summary>
        public string ServiceCertificatePath { get; set; }

        /// <summary>
        ///     Password for service certificate.
        /// </summary>
        public string ServiceCertificatePassword { get; set; }

        /// <summary>
        ///     Service certificate.
        /// </summary>
        public X509Certificate2 ServiceCertificate { get; set; }

        /// <summary>
        ///     Gets or sets BasicHttpsBinding
        /// </summary>
        public BasicHttpsBinding BasicHttpsBinding { get; set; }

        /// <summary>
        ///     Gets or sets BasicHttpBinding.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public BasicHttpBinding BasicHttpBinding { get; set; }

        /// <summary>
        ///     Gets or sets EndpointAddress
        /// </summary>
        public EndpointAddress EndpointAddress { get; set; }
    }
}
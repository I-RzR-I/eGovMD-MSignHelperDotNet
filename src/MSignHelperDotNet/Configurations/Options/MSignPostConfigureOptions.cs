// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-01 09:14
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 20:34
// ***********************************************************************
//  <copyright file="MSignPostConfigureOptions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#if NETSTANDARD2_0_OR_GREATER || NET

#region U S A G E S

using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using RzR.Extensions.Domain.Primitives;
using Microsoft.Extensions.Options;
using MSignHelperDotNet.Models;
using RzR.Extensions.Domain.Text;

// ReSharper disable CollectionNeverUpdated.Local

#endregion

namespace MSignHelperDotNet.Configurations.Options
{
    /// <summary>
    ///     MSign POST configuration options
    /// </summary>
    public class MSignPostConfigureOptions : IPostConfigureOptions<RemoteMSignOptions>
    {
        /// <inheritdoc />
        public void PostConfigure(string name, RemoteMSignOptions mSignOptions)
        {
            if (mSignOptions.RemoteServiceClientAddress.IsMissing())
                throw new ArgumentException("Please provide a RemoteServiceClientAddress");

            if (mSignOptions.ServiceCertificatePath.IsMissing())
                throw new ArgumentException("Please provide a Certificate Path");

            if (mSignOptions.ServiceCertificatePassword.IsMissing())
                throw new ArgumentException("Please provide a Certificate Password");

            if (mSignOptions.RemoteRedirectAddress.IsMissing())
                throw new ArgumentException("Please provide a RemoteRedirectAddress");

            var certificate = new X509Certificate2Collection(CertificateLoader.Private(
                mSignOptions.ServiceCertificatePath,
                mSignOptions.ServiceCertificatePassword));

            if (certificate.Count.IsZero())
                throw new ApplicationException("Invalid service certificate path or password");

            if (certificate.Count.IsGreaterThanZero())
                mSignOptions.ServiceCertificate = certificate[0];

            mSignOptions.BasicHttpBinding = new BasicHttpBinding
            {
                Security =
                {
                    Transport = new HttpTransportSecurity
                    {
                        ClientCredentialType = HttpClientCredentialType.Certificate
                    },
                    Mode = BasicHttpSecurityMode.Transport
                },
                MaxReceivedMessageSize = 2147483647,
                CloseTimeout = TimeSpan.FromMinutes(20)
            };

            mSignOptions.EndpointAddress = new EndpointAddress(mSignOptions.RemoteServiceClientAddress);
        }
    }
}
#endif
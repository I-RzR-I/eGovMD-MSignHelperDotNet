// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2023-01-16 21:16
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-16 21:16
// ***********************************************************************
//  <copyright file="MSignPostConfigureOptionsNetFramework.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#if NET45

using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using DomainCommonExtensions.DataTypeExtensions;
using MSignHelperDotNet.Models;

namespace MSignHelperDotNet.Configurations.Options
{
    /// <summary>
    ///     Post sign configuration
    /// </summary>
    /// <remarks></remarks>
    internal static class MSignPostConfigureOptionsNetFramework
    {
        internal static RemoteMSignOptions InitOptions()
        {
            var remoteServiceClientAddress = ConfigurationManager.AppSettings["RemoteServiceClientAddress"];
            if (remoteServiceClientAddress.IsNullOrEmpty())
                throw new ArgumentException("Please provide a RemoteServiceClientAddress");

            var serviceCertificatePath = ConfigurationManager.AppSettings["ServiceCertificatePath"];
            if (serviceCertificatePath.IsNullOrEmpty())
                throw new ArgumentException("Please provide a ServiceCertificatePath");

            var serviceCertificatePassword = ConfigurationManager.AppSettings["ServiceCertificatePassword"];
            if (serviceCertificatePassword.IsNullOrEmpty())
                throw new ArgumentException("Please provide a ServiceCertificatePassword");

            var remoteRedirectAddress = ConfigurationManager.AppSettings["RemoteRedirectAddress"];
            if (remoteRedirectAddress.IsNullOrEmpty())
                throw new ArgumentException("Please provide a RemoteRedirectAddress");

            var mSignOptions = new RemoteMSignOptions()
            {
                RemoteServiceClientAddress = remoteServiceClientAddress,
                ServiceCertificatePath = serviceCertificatePath,
                ServiceCertificatePassword = serviceCertificatePassword,
                RemoteRedirectAddress = remoteRedirectAddress,
                BasicHttpBinding = new BasicHttpBinding
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
                    CloseTimeout = TimeSpan.FromMinutes(5)
                }
            };
            
            var certificate = new X509Certificate2Collection(CertificateLoader.Private(
                mSignOptions.ServiceCertificatePath,
                mSignOptions.ServiceCertificatePassword));

            if (certificate.Count.IsZero())
                throw new ApplicationException("Invalid service certificate path or password");

            if (certificate.Count.IsGreaterThanZero())
                mSignOptions.ServiceCertificate = certificate[0];
            
            mSignOptions.EndpointAddress = new EndpointAddress(mSignOptions.RemoteServiceClientAddress);

            return mSignOptions;
        }
    }
}

#endif
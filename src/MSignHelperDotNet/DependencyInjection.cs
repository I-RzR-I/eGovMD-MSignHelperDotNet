// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-02 22:18
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 20:46
// ***********************************************************************
//  <copyright file="DependencyInjection.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#if NETSTANDARD2_0_OR_GREATER || NET

#region U S A G E S

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MSignHelperDotNet.Abstractions;
using MSignHelperDotNet.Configurations.Clients;
using MSignHelperDotNet.Configurations.Options;
using MSignHelperDotNet.Models;
using MSignHelperDotNet.Services;
using MSignRemoteService;

#endregion

namespace MSignHelperDotNet
{
    /// <summary>
    ///     MSign service DI
    /// </summary>
    /// <remarks></remarks>
    public static class DependencyInjection
    {
        /// <summary>
        ///     Add MSign service
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">App configuration</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IServiceCollection AddMSingService(this IServiceCollection services, IConfiguration configuration)
        {
            var mSignOption = configuration.GetSection(nameof(RemoteMSignOptions)).Get<RemoteMSignOptions>();
            services.AddOptions<RemoteMSignOptions>();

            services.Configure<RemoteMSignOptions>(options =>
            {
                options.RemoteServiceClientAddress = mSignOption.RemoteServiceClientAddress;
                options.ServiceCertificatePath = mSignOption.ServiceCertificatePath;
                options.ServiceCertificatePassword = mSignOption.ServiceCertificatePassword;
                options.RemoteRedirectAddress = mSignOption.RemoteRedirectAddress;
            });
            services.PostConfigure<RemoteMSignOptions>(options =>
            {
                options.RemoteServiceClientAddress = mSignOption.RemoteServiceClientAddress;
                options.ServiceCertificatePath = mSignOption.ServiceCertificatePath;
                options.ServiceCertificatePassword = mSignOption.ServiceCertificatePassword;
                options.RemoteRedirectAddress = mSignOption.RemoteRedirectAddress;
            });
            services.AddSingleton(mSignOption);

            services.AddSingleton<IPostConfigureOptions<RemoteMSignOptions>, MSignPostConfigureOptions>();

            services.AddScoped<IMSign, MSignClientInternal>();
            services.AddScoped<IMSignService, MSignService>();

            return services;
        }
    }
}
#endif
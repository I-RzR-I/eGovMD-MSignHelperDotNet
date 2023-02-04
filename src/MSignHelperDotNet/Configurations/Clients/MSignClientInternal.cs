// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-02 22:54
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 20:30
// ***********************************************************************
//  <copyright file="MSignClientInternal.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Extensions.Result.Messages;
using DomainCommonExtensions.CommonExtensions;
using MSignRemoteService;

#if NETSTANDARD2_0_OR_GREATER || NET

using Microsoft.Extensions.Options;
using MSignHelperDotNet.Models;

#endif

#if NET45

using MSignHelperDotNet.Models;
using MSignHelperDotNet.Configurations.Options;

#endif

#endregion

namespace MSignHelperDotNet.Configurations.Clients
{
    /// <inheritdoc />
    public class MSignClientInternal : MSignClient
    {
#if NETSTANDARD2_0_OR_GREATER || NET
        /// <inheritdoc />
        public MSignClientInternal(IOptions<RemoteMSignOptions> options) : this(options.Value.BasicHttpBinding,
            options.Value.EndpointAddress)
        {
            if (!options.IsNull() && !options.Value.IsNull())
                ClientCredentials.ClientCertificate.Certificate = options.Value.ServiceCertificate;
        }

        /// <inheritdoc />
        public MSignClientInternal(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
            Instance = this;
        }

        /// <summary>
        ///     Internal signing service
        /// </summary>
        /// <remarks></remarks>
        // ReSharper disable once InconsistentNaming
        public static MSignClientInternal Instance;
#endif

#if NET45

        /// <inheritdoc />
        // ReSharper disable once MemberCanBePrivate.Global
        public MSignClientInternal(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
            if (!RemoteMSignOptions.IsNull())
                // ReSharper disable once PossibleNullReferenceException
                ClientCredentials.ClientCertificate.Certificate = RemoteMSignOptions.ServiceCertificate;
        }

        /// <summary>
        ///     Remote signing options
        /// </summary>
        private static readonly RemoteMSignOptions RemoteMSignOptions = MSignPostConfigureOptionsNetFramework.InitOptions();

        /// <summary>
        ///     Internal signing service
        /// </summary>
        /// <remarks></remarks>
#pragma warning disable IDE0090 // Use 'new(...)'
        public static readonly MSignClientInternal Instance = new MSignClientInternal(RemoteMSignOptions.BasicHttpBinding, RemoteMSignOptions.EndpointAddress);
#pragma warning restore IDE0090 // Use 'new(...)'
#endif

        /// <summary>
        ///     Ping MSign service
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public Result<PingResponse> Ping()
        {
            var client = Instance;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                if (client.ChannelFactory.State != CommunicationState.Opened &&
                    client.ChannelFactory.State == CommunicationState.Closed)
                { client.Open(); }

                stopwatch.Stop();

                return Result<PingResponse>.Success(new PingResponse
                {
                    Message = "Server is available.",
                    Time = $"{stopwatch.Elapsed}",
                    IsSuccess = true
                });
            }
            catch (Exception e)
            {
                var result = new Result<PingResponse>()
                {
                    IsSuccess = true
                };
                result.AddException(e);
                result.SetResult(new PingResponse
                {
                    Message = "Communication problem: " + e.Message,
                    Time = $"{stopwatch.Elapsed}",
                    IsSuccess = false
                });

                return result;
            }
        }
    }
}
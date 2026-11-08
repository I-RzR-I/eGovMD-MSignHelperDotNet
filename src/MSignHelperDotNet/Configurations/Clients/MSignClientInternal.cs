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
using RzR.ResultMessage;
using RzR.ResultMessage.Extensions.Result.Messages;
using RzR.Extensions.Domain.Primitives;
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
        /// <remarks>
        ///     Checks only the state of this client and sends no request to the service. A Faulted or Closed client is
        ///     reported as not available (the outer and the response flags are false). A Faulted client is aborted.
        ///     A Closed client is not opened.
        /// </remarks>
        public Result<PingResponse> Ping()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                var state = State;
                if (state == CommunicationState.Faulted || state == CommunicationState.Closed)
                {
                    if (state == CommunicationState.Faulted)
                        Abort();

                    stopwatch.Stop();

                    return CreateFailedPing("Server is not available.", stopwatch);
                }

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
                return CreateFailedPing("Communication problem: " + e.Message, stopwatch, e);
            }
        }

        private static Result<PingResponse> CreateFailedPing(string message, Stopwatch stopwatch,
            Exception exception = null)
        {
            var result = new Result<PingResponse>()
            {
                IsSuccess = false
            };
            if (exception.IsNotNull())
                result.AddException(exception);

            result.SetResult(new PingResponse
            {
                Message = message,
                Time = $"{stopwatch.Elapsed}",
                IsSuccess = false
            });

            return result;
        }
    }
}
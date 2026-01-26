// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-09 03:11
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 20:49
// ***********************************************************************
//  <copyright file="BuildSignRedirectPageModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

// ReSharper disable ClassNeverInstantiated.Global

#region U S A G E S

using MSignHelperDotNet.Enums;

#endregion

namespace MSignHelperDotNet.Models.InputRequests.BuildSignRedirectPage
{
    /// <summary>
    ///     Build sign redirect page data
    /// </summary>
    /// <remarks></remarks>
    public class BuildSignRedirectPageModel
    {
        /// <summary>
        ///     Gets or sets sign request id.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string RequestId { get; set; }

        /// <summary>
        ///     Gets or sets sign return URL.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     Gets or sets signer phone number.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Gets or sets type to get necessary data to search and verify signature.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public ReceiveResponseType ReceiveResponseFrom { get; set; }

        /// <summary>
        ///     Gets or sets the signing instrument.
        /// </summary>
        /// <remarks>
        ///     This parameter is not required. <br />
        ///     Available values:
        ///     <list type="bullet">
        ///          <item>
        ///              <description>mobile</description>
        ///          </item>
        ///          <item>
        ///              <description>moldsign</description>
        ///          </item>
        ///          <item>
        ///              <description>nationalid</description>
        ///          </item>
        ///          <item>
        ///              <description>securesign</description>
        ///          </item>
        ///          <item>
        ///              <description>tax</description>
        ///          </item>
        ///     </list>
        /// </remarks>
        /// <value>
        ///     The signing instrument.
        /// </value>
        public string Instrument { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the request identifier as URL path parameter
        ///     should be set.
        /// </summary>
        /// <remarks>
        ///     If is 'true', then the result will be: 'ReturnUrl=url/requestId'; otherwise 'ReturnUrl=url?id=requestId'
        /// </remarks>
        /// <value>
        ///     True if set request identifier as URL path parameter, false if not.
        /// </value>
        public bool SetRequestIdAsUrlPathParam { get; set; } = false;

        /// <summary>
        ///     Gets or sets the state of the custom relay.
        /// </summary>
        /// <value>
        ///     The custom relay state.
        /// </value>
        public string CustomRelayState { get; set; }
    }
}
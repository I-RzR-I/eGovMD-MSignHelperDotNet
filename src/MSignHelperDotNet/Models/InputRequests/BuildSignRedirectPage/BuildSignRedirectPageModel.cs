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
    }
}
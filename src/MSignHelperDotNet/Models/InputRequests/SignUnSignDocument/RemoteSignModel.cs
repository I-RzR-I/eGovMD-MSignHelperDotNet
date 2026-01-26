// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-09 03:09
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-09 03:23
// ***********************************************************************
//  <copyright file="RemoteSignModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using MSignHelperDotNet.Enums;
using MSignRemoteService;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace MSignHelperDotNet.Models.InputRequests.SignUnSignDocument
{
    /// <summary>
    ///     Sign documents model
    /// </summary>
    /// <remarks></remarks>
    public class RemoteSignModel
    {
        /// <summary>
        ///     Gets or sets sign content description.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string ContentDescription { get; set; }

        /// <summary>
        ///     Gets or sets sign short content description.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string ShortContentDescription { get; set; }

        /// <summary>
        ///     Gets or sets why u sign the document/s.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string SignatureReason { get; set; }

        /// <summary>
        ///     Gets or sets type of signing content (for the moment HASH or PDF).
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public ContentType ContentType { get; set; }

        /// <summary>
        ///     Gets or sets who sign document/s (physical person identifier code/id/IDNP).
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string SignerId { get; set; }

        /// <summary>
        ///     Gets or sets who sign document/s (company identifier code/id).
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string DelegatorId { get; set; }

        /// <summary>
        ///     Gets or sets type of signer.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public DelegatorType DelegatorType { get; set; }

        /// <summary>
        ///     Gets or sets signer phone number.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Gets or sets sign procedure.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public SignProcedureType SignProcedure { get; set; }

        /// <summary>
        ///     Gets or sets where to return/redirect after signing.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     Gets or sets list of documents to be signed.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public ICollection<SignContentModel> SignContents { get; set; }

        /// <summary>
        /// Gets or sets type to get necessary data to search and verify signature.
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
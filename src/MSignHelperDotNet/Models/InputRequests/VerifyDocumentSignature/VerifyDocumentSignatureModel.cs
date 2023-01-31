// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-09 03:08
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 21:01
// ***********************************************************************
//  <copyright file="VerifyDocumentSignatureModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using MSignRemoteService;

#endregion

// ReSharper disable ClassNeverInstantiated.Global
namespace MSignHelperDotNet.Models.InputRequests.VerifyDocumentSignature
{
    /// <summary>
    ///     Verify signature model
    /// </summary>
    /// <remarks></remarks>
    public class VerifyDocumentSignatureModel
    {
        /// <summary>
        ///     Gets or sets Sign content type.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public ContentType ContentType { get; set; }

        /// <summary>
        ///     Gets or sets contents to verify.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public List<VerifyDocumentSignatureContent> Contents { get; set; }
    }
}
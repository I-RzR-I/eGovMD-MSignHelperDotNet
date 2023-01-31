// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-09 03:08
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 20:54
// ***********************************************************************
//  <copyright file="VerifyDocumentSignatureContent.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

// ReSharper disable ClassNeverInstantiated.Global

namespace MSignHelperDotNet.Models.InputRequests.VerifyDocumentSignature
{
    /// <summary>
    ///     Verify signature content model
    /// </summary>
    /// <remarks></remarks>
    public class VerifyDocumentSignatureContent
    {
        /// <summary>
        ///     Gets or sets document content.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public byte[] Content { get; set; }

        /// <summary>
        ///     Gets or sets document signatures.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public byte[] Signature { get; set; }

        /// <summary>
        ///     Gets or sets correlation id.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string CorrelationId { get; set; }
    }
}
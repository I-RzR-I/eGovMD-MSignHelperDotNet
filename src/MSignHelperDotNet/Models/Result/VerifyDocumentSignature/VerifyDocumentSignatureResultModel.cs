// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-09 03:17
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 21:22
// ***********************************************************************
//  <copyright file="VerifyDocumentSignatureResultModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;

#endregion

namespace MSignHelperDotNet.Models.Result.VerifyDocumentSignature
{
    /// <summary>
    /// </summary>
    /// <remarks></remarks>
    public class VerifyDocumentSignatureResultModel
    {
        /// <summary>
        ///     Gets or sets uniq signed document correlation id.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string CorrelationId { get; set; }

        /// <summary>
        ///     Gets or sets message.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is valid or not.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance ; otherwise, <see langword="false" />.
        /// </value>
        /// <remarks></remarks>
        public bool IsValid { get; set; }

        /// <summary>
        ///     Gets or sets signed certificates.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public IList<VerifyDocumentSignatureCertificateModel> Certificates { get; set; }
    }
}
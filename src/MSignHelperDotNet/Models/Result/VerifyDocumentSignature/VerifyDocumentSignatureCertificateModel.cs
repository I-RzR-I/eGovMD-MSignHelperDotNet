// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-09 03:18
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 21:04
// ***********************************************************************
//  <copyright file="VerifyDocumentSignatureCertificateModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;

#endregion

namespace MSignHelperDotNet.Models.Result.VerifyDocumentSignature
{
    /// <summary>
    ///     Verify document signature certificate model
    /// </summary>
    public class VerifyDocumentSignatureCertificateModel
    {
        /// <summary>
        ///     Gets or sets signed certificate.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public byte[] Certificate { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether signature is valid.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if signature is valid; otherwise, <see langword="false" />.
        /// </value>
        /// <remarks></remarks>
        public bool IdValid { get; set; }

        /// <summary>
        ///     Gets or sets date of signing.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public DateTime? SignedOn { get; set; }

        /// <summary>
        ///     Gets or sets person who signed.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string SignedBy { get; set; }
    }
}
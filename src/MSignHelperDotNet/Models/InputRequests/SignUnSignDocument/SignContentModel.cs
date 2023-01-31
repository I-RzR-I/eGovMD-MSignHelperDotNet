// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-09 03:10
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 20:52
// ***********************************************************************
//  <copyright file="SignContentModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

// ReSharper disable ClassNeverInstantiated.Global

namespace MSignHelperDotNet.Models.InputRequests.SignUnSignDocument
{
    /// <summary>
    ///     Sign content model
    /// </summary>
    /// <remarks></remarks>
    public class SignContentModel
    {
        /// <summary>
        ///     Gets or sets sign document id.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string SignDocId { get; set; }

        /// <summary>
        ///     Gets or sets sign document number.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string SignDocNumber { get; set; }

        /// <summary>
        ///     Gets or sets document.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public byte[] Content { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether on document will be contains multiple signatures.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if ; otherwise, <see langword="false" />.
        /// </value>
        /// <remarks></remarks>
        public bool MultipleSignatures { get; set; }

        /// <summary>
        ///     Gets or sets document name.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string Name { get; set; }
    }
}
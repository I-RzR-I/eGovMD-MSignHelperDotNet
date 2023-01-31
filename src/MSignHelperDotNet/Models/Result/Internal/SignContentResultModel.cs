// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-09 03:22
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-09 03:23
// ***********************************************************************
//  <copyright file="SignContentResultModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace MSignHelperDotNet.Models.Result.Internal
{
    /// <summary>
    ///     Sign content model
    /// </summary>
    internal class SignContentResultModel
    {
        /// <summary>
        ///     Signing document id
        /// </summary>
        internal string SignDocId { get; set; }
        
        /// <summary>
        ///     Signing document number
        /// </summary>
        internal string SignDocNumber { get; set; }
        
        /// <summary>
        ///     Document correlation id (uniq id)
        /// </summary>
        internal string CorrelationId { get; set; }
        
        /// <summary>
        ///     SHA1 content hash
        /// </summary>
        internal string Sha1Hash { get; set; }
    }
}
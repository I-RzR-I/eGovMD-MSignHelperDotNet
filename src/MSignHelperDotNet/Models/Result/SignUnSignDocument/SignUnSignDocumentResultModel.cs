// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-09 03:16
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-09 03:23
// ***********************************************************************
//  <copyright file="SignUnSignDocumentResultModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;

#endregion

namespace MSignHelperDotNet.Models.Result.SignUnSignDocument
{
    /// <summary>
    ///     Result for execution SignUnSignDocument
    /// </summary>
    /// <remarks></remarks>
    public class SignUnSignDocumentResultModel<TUserId, TRecordId>
    {
        /// <summary>
        ///     Gets or sets HTML POST result.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string ResultPostHtmlForm { get; set; }

        /// <summary>
        ///     Gets or sets Redirect URL.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string RedirectUrl { get; set; }

        /// <summary>
        ///     Gets or sets clean MSign redirect URL.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string CleanRedirectUrl { get; set; }

        /// <summary>
        ///     Gets or sets entities for save in local registry.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public IList<Entities.SignRequest<TUserId, TRecordId>> SignRequests { get; set; }
    }
}
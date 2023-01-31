// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-11 17:26
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-11 17:31
// ***********************************************************************
//  <copyright file="BuildSignRedirectPageResultModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace MSignHelperDotNet.Models.Result.BuildSignRedirectPage
{
    /// <summary>
    ///     Result model for build response data for MSign
    /// </summary>
    public class BuildSignRedirectPageResultModel
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
    }
}
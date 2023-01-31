// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-09 03:17
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-09 03:23
// ***********************************************************************
//  <copyright file="CheckDocumentSignStatusResultModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using MSignHelperDotNet.Entities;
using MSignRemoteService;

#endregion

namespace MSignHelperDotNet.Models.Result.CheckDocumentSignStatus
{
    /// <summary>
    ///     Execution for method CheckDocumentSignStatus
    /// </summary>
    /// <remarks></remarks>
    public class CheckDocumentSignStatusResultModel<TUserId, TRecordId>
    {
        /// <summary>
        ///     Gets or sets signing status.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public SignStatus Status { get; set; }

        /// <summary>
        ///     Gets or sets result message.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets edited entities for specific request.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public IList<SignRequest<TUserId, TRecordId>> SignResults { get; set; }
    }
}
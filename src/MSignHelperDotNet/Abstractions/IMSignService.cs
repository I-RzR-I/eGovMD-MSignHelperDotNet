// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-02 02:46
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-09 03:48
// ***********************************************************************
//  <copyright file="IMSignService.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using System.Threading.Tasks;
using AggregatedGenericResultMessage.Abstractions;
using MSignHelperDotNet.Entities;
using MSignHelperDotNet.Models.InputRequests.SignUnSignDocument;
using MSignHelperDotNet.Models.InputRequests.VerifyDocumentSignature;
using MSignHelperDotNet.Models.Result.CheckDocumentSignStatus;
using MSignHelperDotNet.Models.Result.SignUnSignDocument;
using MSignHelperDotNet.Models.Result.VerifyDocumentSignature;
// ReSharper disable CommentTypo

#endregion

namespace MSignHelperDotNet.Abstractions
{
    /// <summary>
    ///     Internal wrapper MSign Service
    /// </summary>
    /// <remarks></remarks>
    public interface IMSignService
    {
        /// <summary>
        ///     Sign or unsign documents
        /// </summary>
        /// <param name="signRequest">Input documents to sign/unsign</param>
        /// <param name="appLanguage">Current application language</param>
        /// <returns>
        ///     The return of this method will be contains 2 objects.
        /// </returns>
        /// <remarks></remarks>
        IResult<SignUnSignDocumentResultModel<TUserId, TRecordId>> SignUnSignDocument<TUserId, TRecordId>(
            RemoteSignModel signRequest, string appLanguage);

        /// <summary>
        ///     Check the sign status of documents
        /// </summary>
        /// <param name="registeredSignRequests">Registered sign requests</param>
        /// <param name="appLanguage">Current application language</param>
        /// <returns></returns>
        IResult<CheckDocumentSignStatusResultModel<TUserId, TRecordId>> CheckDocumentSignStatus<TUserId, TRecordId>(
            IList<SignRequest<TUserId, TRecordId>> registeredSignRequests, string appLanguage);

        /// <summary>
        ///     Validate document signature
        /// </summary>
        /// <param name="validationRequest">Input data to validate</param>
        /// <param name="appLanguage">Current application language</param>
        /// <returns></returns>
        IResult<IList<VerifyDocumentSignatureResultModel>> VerifyDocumentSignature(
            VerifyDocumentSignatureModel validationRequest, string appLanguage);

        /// <summary>
        ///     Sign or unsign documents async
        /// </summary>
        /// <param name="signRequest">Input documents to sign/unsign</param>
        /// <param name="appLanguage">Current application language</param>
        /// <returns>
        ///     The return of this method will be contains 2 objects.
        /// </returns>
        /// <remarks></remarks>
        Task<IResult<SignUnSignDocumentResultModel<TUserId, TRecordId>>> SignUnSignDocumentAsync<TUserId, TRecordId>(RemoteSignModel signRequest,
            string appLanguage);

        /// <summary>
        ///     Check the sign status of documents async
        /// </summary>
        /// <param name="registeredSignRequests">Registered sign requests</param>
        /// <param name="appLanguage">Current application language</param>
        /// <returns></returns>
        Task<IResult<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>> CheckDocumentSignStatusAsync<TUserId, TRecordId>(
            IList<SignRequest<TUserId, TRecordId>> registeredSignRequests, string appLanguage);

        /// <summary>
        ///     Validate document signature
        /// </summary>
        /// <param name="validationRequest">Input data to validate</param>
        /// <param name="appLanguage">Current application language</param>
        /// <returns></returns>
        Task<IResult<IList<VerifyDocumentSignatureResultModel>>> VerifyDocumentSignatureAsync(
            VerifyDocumentSignatureModel validationRequest, string appLanguage);
    }
}
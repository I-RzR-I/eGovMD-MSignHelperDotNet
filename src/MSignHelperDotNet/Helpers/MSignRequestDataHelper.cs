// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-03 19:37
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-03 22:11
// ***********************************************************************
//  <copyright file="MSignRequestDataHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Collections.Generic;
using System.Linq;
using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Extensions.Result.Messages;
using DomainCommonExtensions.DataTypeExtensions;
using MSignHelperDotNet.Models.InputRequests.Internal;
using MSignHelperDotNet.Models.Result.Internal;
using MSignRemoteService;

#endregion

namespace MSignHelperDotNet.Helpers
{
    /// <summary>
    ///     MSign request data helper
    /// </summary>
    /// <remarks></remarks>
    internal static class MSignRequestDataHelper
    {
        /// <summary>
        ///     Generate sign request data
        /// </summary>
        /// <param name="requestData">Input sign data</param>
        /// <returns> SignRequest -> for POST to MSign; List of SignContentResultDto -> for save data in local database</returns>
        /// <remarks></remarks>
        internal static Result<Tuple<SignRequest, IList<SignContentResultModel>>> GenerateSignRequestData(
            GenerateSignRequestDataModel requestData)
        {
            var expectedSigner = GenerateExpectedSigner(requestData.DelegatorType, requestData.PersonIdentifierId,
                requestData.CompanyIdentifierId);
            
            if (!expectedSigner.IsSuccess)
                return Result<Tuple<SignRequest, IList<SignContentResultModel>>>.Failure(expectedSigner.GetFirstError());
            if (!requestData.Contents.Any())
                return Result<Tuple<SignRequest, IList<SignContentResultModel>>>.Failure("Request don't contains any files for signing");

            try
            {
                var signResDto = new List<SignContentResultModel>();

                var request = new SignRequest
                {
                    ContentDescription = requestData.ContentDescription,
                    ShortContentDescription = requestData.ShortContentDescription,
                    SignatureReason = requestData.SignatureReason,
                    ContentType = requestData.ContentType,
                    Contents = new List<SignContent>(),
                    ExpectedSigner = expectedSigner.Response
                };

                foreach (var c in requestData.Contents)
                {
                    var correlationId = Guid.NewGuid().ToString("N");
                    signResDto.Add(new SignContentResultModel
                    {
                        CorrelationId = correlationId,
                        SignDocId = c.SignDocId,
                        SignDocNumber = c.SignDocNumber,
                        Sha1Hash = c.Content.GetHashSha1String()
                    });

                    request.Contents.Add(new SignContent
                    {
                        CorrelationID = correlationId,
                        MultipleSignatures = c.MultipleSignatures,
                        Content = c.Content,
                        Name = c.Name
                    });
                }

                return Result<Tuple<SignRequest, IList<SignContentResultModel>>>.Success(
                    new Tuple<SignRequest, IList<SignContentResultModel>>(request, signResDto));
            }
            catch (Exception e)
            {
                return e;
            }
        }

        /// <summary>
        ///     Generate expected signer
        /// </summary>
        /// <param name="delegatorType">Delegator type</param>
        /// <param name="personIdentifierId">User/Physical person IDNP</param>
        /// <param name="companyIdentifierId">Company IDNO</param>
        /// <returns></returns>
        /// <remarks></remarks>
        // ReSharper disable once IdentifierTypo
        private static Result<ExpectedSigner> GenerateExpectedSigner(DelegatorType delegatorType,
            string personIdentifierId,
            string companyIdentifierId)
        {
            try
            {
                ExpectedSigner result;
                switch (delegatorType)
                {
                    case DelegatorType.None:
                        result = new ExpectedSigner { ID = personIdentifierId, DelegatorType = DelegatorType.None };
                        break;
                    case DelegatorType.Person:
                        if (personIdentifierId.IsNullOrEmpty())
                            return Result<ExpectedSigner>.Failure("Please provide user identifier code (IDNP/ID)");
                        result = new ExpectedSigner 
                        { 
                            ID = personIdentifierId, 
                            DelegatorID = personIdentifierId,
                            DelegatorType = DelegatorType.Person
                        };
                        break;
                    case DelegatorType.Organization:
                        if (companyIdentifierId.IsNullOrEmpty())
                            return Result<ExpectedSigner>.Failure("Please provide company identifier code (IDNO/DelegatorID)");
                        if (personIdentifierId.IsNullOrEmpty())
                            return Result<ExpectedSigner>.Failure("Please provide user identifier code (IDNP/ID)");
                        result = new ExpectedSigner
                        {
                            ID = personIdentifierId,
                            DelegatorID = companyIdentifierId,
                            DelegatorType = DelegatorType.Organization
                        };
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(delegatorType), delegatorType,
                            "Specified parameter is wrong");
                }

                return Result<ExpectedSigner>.Success(result);
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}
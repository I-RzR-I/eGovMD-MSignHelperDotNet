// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-02 02:45
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 21:28
// ***********************************************************************
//  <copyright file="MSignService.cs" company="">
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
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RzR.ResultMessage;
using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Extensions.Result.Messages;
using RzR.Extensions.Domain.Primitives;
using RzR.Extensions.Domain.Text;
using MSignHelperDotNet.Abstractions;
using MSignHelperDotNet.Configurations.Clients;
using MSignHelperDotNet.Constants;
using MSignHelperDotNet.Entities;
using MSignHelperDotNet.Enums;
using MSignHelperDotNet.Helpers;
using MSignHelperDotNet.Models;
using MSignHelperDotNet.Models.InputRequests.BuildSignRedirectPage;
using MSignHelperDotNet.Models.InputRequests.Internal;
using MSignHelperDotNet.Models.InputRequests.SignUnSignDocument;
using MSignHelperDotNet.Models.InputRequests.VerifyDocumentSignature;
using MSignHelperDotNet.Models.Result.BuildSignRedirectPage;
using MSignHelperDotNet.Models.Result.CheckDocumentSignStatus;
using MSignHelperDotNet.Models.Result.SignUnSignDocument;
using MSignHelperDotNet.Models.Result.VerifyDocumentSignature;
using MSignRemoteService;

#if NETSTANDARD2_0_OR_GREATER || NET

using Microsoft.Extensions.Options;

#endif

#if NET45

using MSignHelperDotNet.Configurations.Options;

#endif


#endregion

namespace MSignHelperDotNet.Services
{
    /// <inheritdoc />
    public class MSignService : IMSignService
    {
        private readonly IMSign _mSign;
        private readonly RemoteMSignOptions _mSignOptions;

#if NETSTANDARD2_0_OR_GREATER || NET
        /// <summary>
        ///     Initializes a new instance of the <see cref="MSignHelperDotNet.Services.MSignService" /> class.
        /// </summary>
        /// <param name="mSign">MSign service</param>
        /// <param name="mSignOptions">MSign option</param>
        /// <remarks></remarks>
        public MSignService(IMSign mSign, IOptions<RemoteMSignOptions> mSignOptions)
        {
            _mSign = mSign;
            _mSignOptions = mSignOptions.Value;
        }
#endif

#if NET45

        /// <summary>
        ///     MSign service instance
        /// </summary>
        /// <remarks></remarks>
#pragma warning disable IDE0090 // Use 'new(...)'
        public static readonly MSignService Instance = new MSignService();
#pragma warning restore IDE0090 // Use 'new(...)'

        /// <summary>
        ///     Initializes a new instance of the <see cref="MSignHelperDotNet.Services.MSignService" /> class. 
        /// </summary>
        /// <remarks></remarks>
        public MSignService()
        {
            _mSignOptions = MSignPostConfigureOptionsNetFramework.InitOptions();
            _mSign = MSignClientInternal.Instance;
        }

#endif

        /// <inheritdoc />
        public IResult<SignUnSignDocumentResultModel<TUserId, TRecordId>> SignUnSignDocument<TUserId, TRecordId>(
            RemoteSignModel signRequest, string appLanguage)
        {
            return SignUnSignDocumentsAsync<TUserId, TRecordId>(signRequest, appLanguage)
                .GetAwaiter()
                .GetResult();
        }

        /// <inheritdoc />
        public async Task<IResult<SignUnSignDocumentResultModel<TUserId, TRecordId>>> SignUnSignDocumentAsync<TUserId,
            TRecordId>(RemoteSignModel signRequest,
            string appLanguage)
        {
            return await SignUnSignDocumentsAsync<TUserId, TRecordId>(signRequest, appLanguage, true);
        }

        /// <inheritdoc />
        public IResult<CheckDocumentSignStatusResultModel<TUserId, TRecordId>> CheckDocumentSignStatus<TUserId,
            TRecordId>(
            IList<SignRequest<TUserId, TRecordId>> registeredSignRequests, string appLanguage)
        {
            return CheckDocumentsSignStatusAsync(registeredSignRequests, appLanguage)
                .GetAwaiter()
                .GetResult();
        }

        /// <inheritdoc />
        public async Task<IResult<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>> CheckDocumentSignStatusAsync<
            TUserId, TRecordId>(
            IList<SignRequest<TUserId, TRecordId>> registeredSignRequests, string appLanguage)
        {
            return await CheckDocumentsSignStatusAsync(registeredSignRequests, appLanguage, true);
        }

        /// <inheritdoc />
        public async Task<IResult<IList<VerifyDocumentSignatureResultModel>>> VerifyDocumentSignatureAsync(
            VerifyDocumentSignatureModel validationRequest, string appLanguage)
        {
            return await VerifyDocumentsSignatureAsync(validationRequest, appLanguage, true);
        }

        /// <inheritdoc />
        public IResult<IList<VerifyDocumentSignatureResultModel>> VerifyDocumentSignature(
            VerifyDocumentSignatureModel validationRequest, string appLanguage)
        {
            return VerifyDocumentsSignatureAsync(validationRequest, appLanguage)
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        ///     Check signing documents status
        /// </summary>
        /// <param name="registeredSignRequests">Saved sign requests</param>
        /// <param name="appLanguage">Current app language</param>
        /// <param name="isAsync">Use call async methods</param>
        /// <returns></returns>
        private async Task<IResult<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>>
            CheckDocumentsSignStatusAsync<TUserId, TRecordId>(
                IList<SignRequest<TUserId, TRecordId>> registeredSignRequests, string appLanguage, bool isAsync = false)
        {
            var result = new Result<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>();
            try
            {
                if (!appLanguage.IsNullOrEmpty())
                    if (!RemoteMSignConstants.AvailableCultures.Contains(appLanguage.ToLower()))
                        return Result<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>
                            .Failure("Please provide current application language");

                if (registeredSignRequests.IsNull())
                    return Result<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>
                        .Failure("Please provide data to be checked");

                var signRequestIds = registeredSignRequests.Select(x => x.RequestId).Distinct().ToList();
                if (!signRequestIds.Any() || signRequestIds.Count > 1 || signRequestIds.FirstOrDefault().IsNull())
                    return Result<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>
                        .Failure("Please provide data from the same sign request");

                var client = _mSign as MSignClientInternal;
                if (client.IsNotNull())
                {
                    var ping = client!.Ping();
                    if (!ping.Response.IsSuccess)
                    {
                        return Result<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>
                            .Failure(ping.Response.Message);
                    }
                }

                var signResponse = isAsync.Equals(false)
                    // ReSharper disable once MethodHasAsyncOverload
                    ? _mSign.GetSignResponse(signRequestIds.FirstOrDefault(), appLanguage)
                    : await _mSign.GetSignResponseAsync(signRequestIds.FirstOrDefault(), appLanguage);
                if (signResponse.Results.IsNull())
                    return Result<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>
                        .Failure("No any data found! Check if your documents was signed!");

                var resultListOfSignedDocs = signResponse.Results;
                if (registeredSignRequests.Count != resultListOfSignedDocs.Count)
                    return Result<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>
                        .Failure("Provided list of documents and list of documents received from signing service are not equal!");

                var correlationIds = new HashSet<string>();
                var certificates = new List<string>(registeredSignRequests.Count);
                var signatures = new List<string>(registeredSignRequests.Count);
                foreach (var sr in registeredSignRequests)
                {
                    if (sr.CorrelationId.IsNullOrEmpty())
                        return Result<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>
                            .Failure($"Provides record don't contains {nameof(sr.CorrelationId)}");

                    if (!correlationIds.Add(sr.CorrelationId))
                        return Result<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>
                            .Failure($"Provided list contains duplicate {nameof(sr.CorrelationId)}('{sr.CorrelationId}').");

                    var currentDoc = resultListOfSignedDocs.FirstOrDefault(x => x.CorrelationID == sr.CorrelationId);
                    if (currentDoc.IsNull())
                        return Result<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>
                            .Failure($"The document with specified correlation id('{sr.CorrelationId}') not found.");

                    var isSuccess = signResponse.Status == SignStatus.Success;
                    certificates.Add(isSuccess ? currentDoc!.Certificate.ToBase64String() : null);
                    signatures.Add(isSuccess ? currentDoc.Signature.ToBase64String() : null);
                }

                for (var i = 0; i < registeredSignRequests.Count; i++)
                {
                    var sr = registeredSignRequests[i];

                    sr.ModifiedOn = DateTime.Now;
                    sr.SignStatusId = signResponse.Status;

                    if (signResponse.Status != SignStatus.Success) 
                        continue;

                    sr.SignedOn ??= DateTime.Now;

                    sr.Certificate = certificates[i];
                    sr.Signature = signatures[i];
                }

                return Result<CheckDocumentSignStatusResultModel<TUserId, TRecordId>>
                    .Success(
                        new CheckDocumentSignStatusResultModel<TUserId, TRecordId>
                        {
                            Message = signResponse.Message,
                            Status = signResponse.Status,
                            SignResults = registeredSignRequests
                        });
            }
            catch (FaultException ex)
            {
                result.AddException(ex);
                result.AddError($"Fault code: {ex.Code.Name}; Reason: {ex.Reason}");
            }
            catch (Exception e)
            {
                result.AddException(e);
                result.AddError("An unexpected error occurred while processing the request.");
            }

            return result;
        }

        /// <summary>
        ///     Sign or unsign documents
        /// </summary>
        /// <param name="signRequest">Sign request data</param>
        /// <param name="appLanguage">Current app language</param>
        /// <param name="isAsync">Use call async methods</param>
        /// <returns></returns>
        private async Task<IResult<SignUnSignDocumentResultModel<TUserId, TRecordId>>> SignUnSignDocumentsAsync<TUserId,
            TRecordId>(RemoteSignModel signRequest,
            string appLanguage, bool isAsync = false)
        {
            var result = new Result<SignUnSignDocumentResultModel<TUserId, TRecordId>>();
            try
            {
                if (signRequest.IsNull())
                    return Result<SignUnSignDocumentResultModel<TUserId, TRecordId>>
                        .Failure("Please provide sign data");

                var validateData = ValidateInputRequest(signRequest);
                if (!validateData.IsSuccess)
                    return Result<SignUnSignDocumentResultModel<TUserId, TRecordId>>
                        .Failure(validateData.Messages.FirstOrDefault()?.Message);

                var generateSignRequest = MSignRequestDataHelper.GenerateSignRequestData(
                    new GenerateSignRequestDataModel
                    {
                        CompanyIdentifierId = signRequest.DelegatorId,
                        ContentDescription = signRequest.ContentDescription,
                        ContentType = signRequest.ContentType,
                        DelegatorType = signRequest.DelegatorType,
                        PersonIdentifierId = signRequest.SignerId,
                        ShortContentDescription = signRequest.ShortContentDescription,
                        SignatureReason = signRequest.SignatureReason,
                        Contents = signRequest.SignContents
                    });

                if (!generateSignRequest.IsSuccess)
                    return Result<SignUnSignDocumentResultModel<TUserId, TRecordId>>
                        .Failure(generateSignRequest.GetFirstMessage());

                if (!appLanguage.IsNullOrEmpty())
                    if (!RemoteMSignConstants.AvailableCultures.Contains(appLanguage.ToLower()))
                        return Result<SignUnSignDocumentResultModel<TUserId, TRecordId>>
                            .Failure("Please provide current application language");

                var client = _mSign as MSignClientInternal;
                if (client.IsNotNull())
                {
                    var ping = client!.Ping();
                    if (!ping.Response.IsSuccess)
                    {
                        return Result<SignUnSignDocumentResultModel<TUserId, TRecordId>>
                            .Failure(ping.Response.Message);
                    }
                }

                var requestId = isAsync.Equals(false)
                    // ReSharper disable once MethodHasAsyncOverload
                    ? _mSign.PostSignRequest(generateSignRequest.Response.Item1)
                        : await _mSign.PostSignRequestAsync(generateSignRequest.Response.Item1);

                var redirectResult = BuildSignRedirectPage(new BuildSignRedirectPageModel
                {
                    PhoneNumber = signRequest.PhoneNumber,
                    ReturnUrl = signRequest.ReturnUrl,
                    RequestId = requestId,
                    ReceiveResponseFrom = signRequest.ReceiveResponseFrom,
                    CustomRelayState = signRequest.CustomRelayState,
                    Instrument = signRequest.Instrument,
                    SetRequestIdAsUrlPathParam = signRequest.SetRequestIdAsUrlPathParam
                }, appLanguage);

                if (!redirectResult.IsSuccess)
                    return Result<SignUnSignDocumentResultModel<TUserId, TRecordId>>
                        .Failure(redirectResult.GetFirstMessage());

                var saveRequest = new List<SignRequest<TUserId, TRecordId>>();
                foreach (var sr in generateSignRequest.Response.Item2)
                    saveRequest.Add(new SignRequest<TUserId, TRecordId>
                    {
                        CorrelationId = sr.CorrelationId,
                        RequestId = requestId,
                        CreateOn = DateTime.Now,
                        CreatedIp = signRequest.ClientIp,
                        ProcedureTypeId = signRequest.SignProcedure,
                        SignStatusId = SignStatus.Pending,
                        SignDocId = sr.SignDocId,
                        SignDocNumber = sr.SignDocNumber,
                        Hash = sr.Sha1Hash
                    });

                return Result<SignUnSignDocumentResultModel<TUserId, TRecordId>>
                    .Success(
                        new SignUnSignDocumentResultModel<TUserId, TRecordId>
                        {
                            SignRequests = saveRequest,
                            ResultPostHtmlForm = redirectResult.Response.ResultPostHtmlForm,
                            RedirectUrl = redirectResult.Response.RedirectUrl,
                            CleanRedirectUrl = redirectResult.Response.CleanRedirectUrl
                        });
            }
            catch (FaultException ex)
            {
                result.AddException(ex);
                result.AddError($"Fault code: {ex.Code.Name}; Reason: {ex.Reason}");
            }
            catch (Exception e)
            {
                result.AddException(e);
                result.AddError("An unexpected error occurred while processing the request.");
            }

            return result;
        }

        /// <summary>
        ///     Validate document signature
        /// </summary>
        /// <param name="validationRequest">Data to be validated</param>
        /// <param name="appLanguage">Current application language</param>
        /// <param name="isAsync">Use call async methods</param>
        /// <returns></returns>
        private async Task<IResult<IList<VerifyDocumentSignatureResultModel>>> VerifyDocumentsSignatureAsync(
            VerifyDocumentSignatureModel validationRequest, string appLanguage, bool isAsync = false)
        {
            var result = new Result<IList<VerifyDocumentSignatureResultModel>>();
            try
            {
                if (!appLanguage.IsNullOrEmpty())
                    if (!RemoteMSignConstants.AvailableCultures.Contains(appLanguage.ToLower()))
                        return Result<IList<VerifyDocumentSignatureResultModel>>
                            .Failure("Please provide current application language");

                if (validationRequest.Contents.IsNull() || validationRequest.Contents.Count.IsZero())
                    return Result<IList<VerifyDocumentSignatureResultModel>>
                        .Failure("Please provide content for validation");

                if (!Enum.IsDefined(typeof(ContentType), validationRequest.ContentType))
                    return Result<IList<VerifyDocumentSignatureResultModel>>
                        .Failure("Please provide a valid content type");

                var client = _mSign as MSignClientInternal;
                if (client.IsNotNull())
                {
                    var ping = client!.Ping();
                    if (!ping.Response.IsSuccess)
                    {
                        return Result<IList<VerifyDocumentSignatureResultModel>>
                            .Failure(ping.Response.Message);
                    }
                }

                var validationResponse = isAsync.Equals(false)
                    // ReSharper disable once MethodHasAsyncOverload
                    ? _mSign.VerifySignatures(new VerificationRequest
                    {
                        SignedContentType = validationRequest.ContentType,
                        Language = appLanguage,
                        Contents = validationRequest
                            .Contents
                            .Select(x => new VerificationContent
                            {
                                CorrelationID = x.CorrelationId,
                                Content = x.Content,
                                Signature = x.Signature
                            }).ToList()
                    })
                    : await _mSign.VerifySignaturesAsync(new VerificationRequest
                    {
                        SignedContentType = validationRequest.ContentType,
                        Language = appLanguage,
                        Contents = validationRequest
                        .Contents
                        .Select(x => new VerificationContent
                        {
                            CorrelationID = x.CorrelationId,
                            Content = x.Content,
                            Signature = x.Signature
                        }).ToList()
                    });

                return Result<IList<VerifyDocumentSignatureResultModel>>
                    .Success(validationResponse
                        .Results
                        .Select(x =>
                            new VerifyDocumentSignatureResultModel
                            {
                                CorrelationId = x.CorrelationID,
                                Message = x.Message,
                                IsValid = x.SignaturesValid,
                                Certificates = x.Certificates
                                    .Select(z => new VerifyDocumentSignatureCertificateModel
                                    {
                                        Certificate = z.Certificate,
                                        IdValid = z.SignatureValid,
                                        SignedOn = z.SignedAt,
                                        SignedBy = z.Subject
                                    }).ToList()
                            }).ToList());
            }
            catch (FaultException ex)
            {
                result.AddException(ex);
                result.AddError($"Fault code: {ex.Code.Name}; Reason: {ex.Reason}");
            }
            catch (Exception e)
            {
                result.AddException(e);
                result.AddError("An unexpected error occurred while processing the request.");
            }

            return result;
        }

        /// <summary>
        ///     Validate input request
        /// </summary>
        /// <param name="data">Input date for sign</param>
        /// <returns></returns>
        private static IResult ValidateInputRequest(RemoteSignModel data)
        {
            if (data.SignContents.IsNull())
                return Result.Failure("Please provide sign documents");

            if (data.DelegatorType == DelegatorType.Organization)
                if (data.SignerId.IsNullOrEmpty() || data.DelegatorId.IsNullOrEmpty())
                    return Result.Failure($"Please provide {nameof(data.SignerId)} and {nameof(data.DelegatorId)}");

            if (data.SignProcedure == SignProcedureType.Undefined ||
                !Enum.IsDefined(typeof(SignProcedureType), data.SignProcedure))
                return Result.Failure("Please provide a valid sign procedure");

            if (data.ReturnUrl.IsNullOrEmpty())
                return Result.Failure("Please provide return url");

            if (data.SignatureReason.IsNullOrEmpty())
                return Result.Failure("Please provide a sign reason");

            if (data.ContentDescription.IsNullOrEmpty() || data.ShortContentDescription.IsNullOrEmpty())
                return Result.Failure(
                    $"Please provide {nameof(data.ContentDescription)} and {nameof(data.ShortContentDescription)}");

            foreach (var c in data.SignContents)
                if (c.SignDocId.IsNullOrEmpty() || c.SignDocNumber.IsNullOrEmpty())
                    return Result
                        .Failure($"Please provide {nameof(c.SignDocId)} and {nameof(c.SignDocNumber)}");

            if (!Enum.IsDefined(typeof(ContentType), data.ContentType))
                return Result.Failure("Please provide a valid content type");

            return Result.Success();
        }

        /// <summary>
        ///     Build sign redirect HTML document
        /// </summary>
        /// <param name="request">Input params</param>
        /// <param name="appLanguage">Current application language</param>
        /// <returns></returns>
        private Result<BuildSignRedirectPageResultModel> BuildSignRedirectPage(BuildSignRedirectPageModel request,
            string appLanguage)
        {
            if (!appLanguage.IsNullOrEmpty())
                if (!RemoteMSignConstants.AvailableCultures.Contains(appLanguage.ToLower()))
                    return Result<BuildSignRedirectPageResultModel>
                        .Failure("Please provide current application language");
            try
            {
                if (request.ReceiveResponseFrom == ReceiveResponseType.GET)
                {
                    var escapedReturnUrl = Uri.EscapeDataString(request.ReturnUrl.IfNullThenEmpty());
                    var escapedRequestId = Uri.EscapeDataString(request.RequestId.IfNullThenEmpty());
                    var cleanRedirectUrl = $"{_mSignOptions.RemoteRedirectAddress}/{escapedRequestId}";
                    var redirectUrl = request.SetRequestIdAsUrlPathParam.IsTrue()
                        ? $"{cleanRedirectUrl}?ReturnUrl={escapedReturnUrl}/{escapedRequestId}"
                        : $"{cleanRedirectUrl}?ReturnUrl={escapedReturnUrl}?id={escapedRequestId}";

                    return Result<BuildSignRedirectPageResultModel>
                        .Success(new BuildSignRedirectPageResultModel
                        {
                            RedirectUrl = redirectUrl,
                            CleanRedirectUrl = cleanRedirectUrl
                        });
                }


                var postFormUrl = $"{_mSignOptions.RemoteRedirectAddress}/{request.RequestId}";
                var formValues = new Dictionary<string, string>
                {
                    ["PostFormUrl"] = postFormUrl,
                    ["ReturnUrl"] = request.ReturnUrl,
                    ["MSISDN"] = request.PhoneNumber,
                    ["RelayState"] = request.CustomRelayState.IfNullOrWhiteSpace(request.ReturnUrl.Base64Encode()),
                    ["lang"] = appLanguage?.ToLower(),
                    ["Instrument"] = request.Instrument.IfNullThenEmpty()
                };

                var htmlPage = Regex.Replace(RemoteMSignConstants.MSignHtmlRedirect, @"\{#(\w+)#\}",
                    m => formValues.TryGetValue(m.Groups[1].Value, out var value)
                        ? System.Net.WebUtility.HtmlEncode(value) ?? string.Empty
                        : m.Value);

                return Result<BuildSignRedirectPageResultModel>
                    .Success(new BuildSignRedirectPageResultModel
                    {
                        RedirectUrl = postFormUrl,
                        ResultPostHtmlForm = htmlPage,
                        CleanRedirectUrl = postFormUrl
                    });
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}
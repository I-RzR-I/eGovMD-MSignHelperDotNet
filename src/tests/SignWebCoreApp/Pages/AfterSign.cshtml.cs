// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebCoreApp
//  Author           : RzR
//  Created On       : 2022-11-11 19:46
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 21:54
// ***********************************************************************
//  <copyright file="AfterSign.cshtml.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Linq;
using DomainCommonExtensions.ArraysExtensions;
using DomainCommonExtensions.CommonExtensions;
using DomainCommonExtensions.DataTypeExtensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSignHelperDotNet.Abstractions;
using MSignHelperDotNet.Entities;
using MSignHelperDotNet.Models.InputRequests.VerifyDocumentSignature;
using MSignRemoteService;
using SignWebCoreApp.Abstractions;

#endregion

namespace SignWebCoreApp.Pages
{
    public class AfterSignModel : PageModel
    {
        private readonly IAppContext _ctx;
        private readonly IMSignService _mSignService;

        public AfterSignModel(IMSignService mSignService, IAppContext ctx)
        {
            _mSignService = mSignService;
            _ctx = ctx;
        }

        public void OnGet()
        {
            var signId = Request.Query["id"].ToString();
            if (signId.IsNullOrEmpty()) return;

            //Find all records from your DB related to this id and pass to 'CheckDocumentSignStatus' method
            var listData = _ctx.SignRequestStore.Where(x => x.RequestId == signId)
                .Select(x => new SignRequest<Guid, Guid>
                {
                    Certificate = x.Certificate,
                    Id = x.Id,
                    CorrelationId = x.CorrelationId,
                    CreateOn = x.CreateOn,
                    CreatedIp = x.CreatedIp,
                    DocumentTypeId = x.DocumentTypeId,
                    Hash = x.Hash,
                    ModifiedIp = x.ModifiedIp,
                    ModifiedOn = x.ModifiedOn,
                    ProcedureTypeId = x.ProcedureTypeId,
                    RequestId = x.RequestId,
                    SignDocId = x.SignDocId,
                    SignDocNumber = x.SignDocNumber,
                    SignStatusId = x.SignStatusId,
                    Signature = x.Signature,
                    SignedOn = x.SignedOn,
                    UserId = x.UserId
                }).ToList();

            if (!listData.IsNull() && listData.Any())
            {
                var signResponse = _mSignService.CheckDocumentSignStatus(listData, "ro");
                foreach (var sr in signResponse.Response.SignResults)
                {
                    var record = _ctx.SignRequestStore.FirstOrDefault(x =>
                        x.RequestId == sr.RequestId && x.CorrelationId == sr.CorrelationId);
                    if (!record.IsNull())
                    {
                        record.SignStatusId = sr.SignStatusId;
                        record.ModifiedOn = sr.ModifiedOn;
                        record.SignedOn = sr.SignedOn;
                        record.ModifiedIp = sr.ModifiedIp;
                        record.Certificate = sr.Certificate;
                        record.Signature = sr.Signature;

                        _ctx.SignRequestStore.Update(record);
                        _ctx.SaveChanges();
                    }
                }

                var validateSignature = _mSignService.VerifyDocumentSignature(new VerifyDocumentSignatureModel
                {
                    ContentType = ContentType.Hash,
                    Contents = listData
                        .Select(x => new VerifyDocumentSignatureContent
                        {
                            CorrelationId = x.CorrelationId,
                            Content = "TestHASH".GetHashSha1CryptoProv(),
                            Signature = x.Signature.DeCodeBytesFromBase64()
                        }).ToList()
                }, "ro");

                if (!validateSignature.IsSuccess) return;

                foreach (var sr in listData)
                {
                    var record = _ctx.SignRequestStore.FirstOrDefault(x =>
                        x.RequestId == sr.RequestId && x.CorrelationId == sr.CorrelationId);
                    if (!record.IsNull())
                    {
                        var signedValidDoc = validateSignature.Response
                            .FirstOrDefault(x => x.CorrelationId == sr.CorrelationId);
                        if (!signedValidDoc.IsNull())
                        {
                            record.IsValidSignature = signedValidDoc.IsValid;
                            record.SignSubject = signedValidDoc.Certificates.Select(x => x.SignedBy).ListToString("#");

                            _ctx.SignRequestStore.Update(record);
                            _ctx.SaveChanges();
                        }
                    }
                }
            }
        }

        public void OnPost()
        {
        }
    }
}
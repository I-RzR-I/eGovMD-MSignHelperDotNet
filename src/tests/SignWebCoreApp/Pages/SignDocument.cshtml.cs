// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebCoreApp
//  Author           : RzR
//  Created On       : 2022-11-09 19:19
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 21:54
// ***********************************************************************
//  <copyright file="SignDocument.cshtml.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Collections.Generic;
using AggregatedGenericResultMessage.Abstractions;
using DomainCommonExtensions.DataTypeExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSignHelperDotNet.Abstractions;
using MSignHelperDotNet.Enums;
using MSignHelperDotNet.Models.InputRequests.SignUnSignDocument;
using MSignHelperDotNet.Models.Result.SignUnSignDocument;
using MSignRemoteService;
using SignWebCoreApp.Abstractions;
using SignWebCoreApp.TempData.Entities;

#endregion

namespace SignWebCoreApp.Pages
{
    public class SignDocumentModel : PageModel
    {
        private readonly IAppContext _ctx;
        private readonly IMSignService _mSignService;

        public SignDocumentModel(IMSignService mSignService, IAppContext ctx)
        {
            _mSignService = mSignService;
            _ctx = ctx;
        }

        public void OnGet()
        {
        }
        
        public void OnPostSubmit()
        {
            var hash = "TestHASH".GetHashSha1CryptoProv();
            IResult<SignUnSignDocumentResultModel<Guid, Guid>> sign;
            sign = _mSignService.SignUnSignDocument<Guid, Guid>(new RemoteSignModel
            {
                ReceiveResponseFrom = ReceiveResponseType.GET,
                ContentDescription = "TestSign CD",
                ShortContentDescription = "TestSign SCD",
                ContentType = ContentType.Hash,
                DelegatorId = null,
                DelegatorType = DelegatorType.Person,
                PhoneNumber = "00000000",
                SignProcedure = SignProcedureType.Sign,
                SignatureReason = "Sign for test",
                SignerId = "0000000000000",
                ReturnUrl = "https://localhost:5001/AfterSign",
                SignContents = new List<SignContentModel>
                {
                    new SignContentModel
                    {
                        SignDocNumber = "1",
                        MultipleSignatures = false,
                        SignDocId = "1",
                        Content = hash,
                        Name = null
                    }
                }
            }, "ro");

            if (sign.IsSuccess)
            {
                foreach (var s in sign.Response.SignRequests)
                {
                    var record = SignRequestStore.Map(s);
                    if (record != null)
                    {
                        record.Id = Guid.NewGuid();
                        _ctx.SignRequestStore
                            .Add(record);
                        _ctx.SaveChanges();
                    }
                }

                Response.Redirect(sign.Response.RedirectUrl, false, false);
            }
        }
    }
}
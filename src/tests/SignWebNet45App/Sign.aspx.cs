// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebNet45App
//  Author           : RzR
//  Created On       : 2023-01-17 00:30
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 10:38
// ***********************************************************************
//  <copyright file="Sign.aspx.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Collections.Generic;
using System.Web.UI;
using DomainCommonExtensions.DataTypeExtensions;
using MSignHelperDotNet.Enums;
using MSignHelperDotNet.Models.InputRequests.SignUnSignDocument;
using MSignHelperDotNet.Services;
using MSignRemoteService;
using SignWebNet45App.TempData;
using SignWebNet45App.TempData.Entities;

#endregion

namespace SignWebNet45App
{
    public partial class Sign : Page
    {
        private AppContext _context;

        protected void Page_Load(object sender, EventArgs e)
        {
            _context = new AppContext("SignRequestDb");
        }

        protected void OnClick(object sender, EventArgs e)
        {
            var hash = "TestHASH".GetHashSha1CryptoProv();
            var sign = MSignService.Instance.SignUnSignDocument<Guid, Guid>(new RemoteSignModel
            {
                ReceiveResponseFrom = ReceiveResponseType.GET,
                ContentDescription = "TestSign CD",
                ShortContentDescription = "TestSign SCD",
                ContentType = MSignRemoteService.ContentType.Hash,
                DelegatorId = null,
                DelegatorType = DelegatorType.Person,
                PhoneNumber = "00000000",
                SignProcedure = SignProcedureType.Sign,
                SignatureReason = "Sign for test",
                SignerId = "0000000000000",
                ReturnUrl = "http://localhost:28792/Signed.aspx",
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
                        _context.Set<SignRequestStore>()
                            .Add(record);
                        _context.SaveChanges();
                    }
                }

                Response.Redirect(sign.Response.RedirectUrl);
            }
        }
    }
}
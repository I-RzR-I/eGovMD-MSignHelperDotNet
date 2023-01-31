// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebNet45App
//  Author           : RzR
//  Created On       : 2023-01-17 00:50
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 10:38
// ***********************************************************************
//  <copyright file="Signed.aspx.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using DomainCommonExtensions.ArraysExtensions;
using DomainCommonExtensions.CommonExtensions;
using DomainCommonExtensions.DataTypeExtensions;
using MSignHelperDotNet.Entities;
using MSignHelperDotNet.Models.InputRequests.VerifyDocumentSignature;
using MSignHelperDotNet.Services;
using SignWebNet45App.TempData;
using SignWebNet45App.TempData.Entities;

#endregion

namespace SignWebNet45App
{
    public partial class Signed : Page
    {
        private AppContext _context;

        protected void Page_Load(object sender, EventArgs e)
        {
            var signId = Request.QueryString["id"];
            if (signId.IsNullOrEmpty()) return;

            _context = new AppContext("SignRequestDb");

            if (!IsPostBack)
            {
                //Find all records from your DB related to this id and pass to 'CheckDocumentSignStatus' method
                var listData = _context.Set<SignRequestStore>().Where(x => x.RequestId == signId)
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
                    var signResponse = MSignService.Instance.CheckDocumentSignStatus(listData, "ro");
                    foreach (var sr in signResponse.Response.SignResults)
                    {
                        var record = _context.Set<SignRequestStore>().FirstOrDefault(x =>
                            x.RequestId == sr.RequestId && x.CorrelationId == sr.CorrelationId);
                        if (!record.IsNull())
                        {
                            record.SignStatusId = sr.SignStatusId;
                            record.ModifiedOn = sr.ModifiedOn;
                            record.SignedOn = sr.SignedOn;
                            record.ModifiedIp = sr.ModifiedIp;
                            record.Certificate = sr.Certificate;
                            record.Signature = sr.Signature;

                            _context.Entry(record).State = EntityState.Modified;
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }

        protected void OnClick(object sender, EventArgs e)
        {
            var signId = Request.QueryString["id"];
            if (signId.IsNullOrEmpty()) return;

            _context = new AppContext("SignRequestDb");
            var listData = _context.Set<SignRequestStore>().Where(x => x.RequestId == signId).ToList();
            if (listData.Any())
            {
                var validateSignature = MSignService.Instance.VerifyDocumentSignature(new VerifyDocumentSignatureModel
                {
                    ContentType = MSignRemoteService.ContentType.Hash,
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
                    var record = _context.Set<SignRequestStore>().FirstOrDefault(x =>
                        x.RequestId == sr.RequestId && x.CorrelationId == sr.CorrelationId);
                    if (!record.IsNull())
                    {
                        var signedValidDoc = validateSignature.Response
                            .FirstOrDefault(x => x.CorrelationId == sr.CorrelationId);
                        if (!signedValidDoc.IsNull())
                        {
                            record.IsValidSignature = signedValidDoc.IsValid;
                            record.SignSubject = signedValidDoc.Certificates.Select(x => x.SignedBy).ListToString("#");

                            _context.Entry(record).State = EntityState.Modified;
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
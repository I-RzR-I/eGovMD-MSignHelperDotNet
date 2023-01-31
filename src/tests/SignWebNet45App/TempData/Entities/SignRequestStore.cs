// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebNet45App
//  Author           : RzR
//  Created On       : 2023-01-18 08:06
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 10:37
// ***********************************************************************
//  <copyright file="SignRequestStore.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using MSignHelperDotNet.Entities;
using MSignHelperDotNet.Enums;
using MSignRemoteService;

#endregion

namespace SignWebNet45App.TempData.Entities
{
    public class SignRequestStore
    {
        /// <summary>
        ///     User signing
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        ///     Sign request Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Sign document Id
        /// </summary>
        public string SignDocId { get; set; }

        /// <summary>
        ///     Sign document number
        /// </summary>
        public string SignDocNumber { get; set; }

        /// <summary>
        ///     Signature request Id
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        ///     Signature request documents correlation Id
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        ///     Sign hash
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        ///     Request procedure
        /// </summary>
        public SignProcedureType ProcedureTypeId { get; set; }

        /// <summary>
        ///     Current status of sign request
        /// </summary>
        public SignStatus SignStatusId { get; set; }

        /// <summary>
        ///     Signing document type
        /// </summary>
        public int? DocumentTypeId { get; set; }

        /// <summary>
        ///     Date of creation document
        /// </summary>
        public DateTime CreateOn { get; set; }

        /// <summary>
        ///     Date of modification
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        ///     Date of signing document
        /// </summary>
        public DateTime? SignedOn { get; set; }

        /// <summary>
        ///     User IP created record
        /// </summary>
        public string CreatedIp { get; set; }

        /// <summary>
        ///     User IP modified record
        /// </summary>
        public string ModifiedIp { get; set; }

        /// <summary>
        ///     Signature
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        ///     Signature certificate
        /// </summary>
        public string Certificate { get; set; }

        /// <summary>
        ///     Is valid signature
        /// </summary>
        public bool IsValidSignature { get; set; }

        /// <summary>
        ///     Signer subject
        /// </summary>
        public string SignSubject { get; set; }

        public static SignRequestStore Map(SignRequest<Guid, Guid> request)
        {
            return new SignRequestStore
            {
                UserId = request.UserId,
                Id = request.Id,
                SignDocId = request.SignDocId,
                SignDocNumber = request.SignDocNumber,
                RequestId = request.RequestId,
                CorrelationId = request.CorrelationId,
                Hash = request.Hash,
                ProcedureTypeId = request.ProcedureTypeId,
                DocumentTypeId = request.DocumentTypeId,
                CreateOn = request.CreateOn,
                ModifiedOn = request.ModifiedOn,
                SignedOn = request.SignedOn,
                CreatedIp = request.CreatedIp,
                ModifiedIp = request.ModifiedIp,
                Signature = request.Signature,
                Certificate = request.Certificate,
                IsValidSignature = request.IsValidSignature,
                SignSubject = request.SignSubject,
                SignStatusId = request.SignStatusId
            };
        }
    }
}
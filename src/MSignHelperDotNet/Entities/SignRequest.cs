// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-10-30 23:16
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-10-31 01:13
// ***********************************************************************
//  <copyright file="SignRequest.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EntityMaxLengthTrim.Attributes;
using EntityMaxLengthTrim.Interceptors;
using MSignHelperDotNet.Constants;
using MSignHelperDotNet.Enums;
// ReSharper disable UnusedAutoPropertyAccessor.Global

// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

#endregion

// ReSharper disable ClassNeverInstantiated.Global

namespace MSignHelperDotNet.Entities
{
    /// <summary>
    ///     Sign request data/model
    /// </summary>
    public class SignRequest<TUserId, TRecordId> : INotifyPropertyChanged
    {
        private string _modifiedIp;
        private string _createdIp;
        private string _correlationId;
        private string _requestId;
        private string _signDocNumber;
        private string _signDocId;

        /// <summary>
        ///     User signing
        /// </summary>
        public virtual TUserId UserId { get; set; }

        /// <summary>
        ///     Sign request Id
        /// </summary>
        public virtual TRecordId Id { get; set; }

        /// <summary>
        ///     Sign document Id
        /// </summary>
        [MaxAllowedLength(DbLengthConstrains.SignRequests.SignDocId.MaxLength)]
        public string SignDocId
        {
            get => _signDocId;
            set
            {
                if (_signDocId == value) return;
                _signDocId = value;
                OnPropertyChanged(nameof(SignDocId));
            }
        }

        /// <summary>
        ///     Sign document number
        /// </summary>
        [MaxAllowedLength(DbLengthConstrains.SignRequests.SignDocNumber.MaxLength)]
        public string SignDocNumber
        {
            get => _signDocNumber;
            set
            {
                if (_signDocNumber == value) return;
                _signDocNumber = value;
                OnPropertyChanged(nameof(SignDocNumber));
            }
        }

        /// <summary>
        ///     Signature request Id
        /// </summary>
        [MaxAllowedLength(DbLengthConstrains.SignRequests.RequestId.MaxLength)]
        public string RequestId
        {
            get => _requestId;
            set
            {
                if (_requestId == value) return;
                _requestId = value;
                OnPropertyChanged(nameof(RequestId));
            }
        }

        /// <summary>
        ///     Signature request documents correlation Id
        /// </summary>
        [MaxAllowedLength(DbLengthConstrains.SignRequests.CorrelationId.MaxLength)]
        public string CorrelationId
        {
            get => _correlationId;
            set
            {
                if (_correlationId == value) return;
                _correlationId = value;
                OnPropertyChanged(nameof(CorrelationId));
            }
        }

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
        public MSignRemoteService.SignStatus SignStatusId { get; set; }

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
        [MaxAllowedLength(DbLengthConstrains.SignRequests.CreatedIp.MaxLength)]
        public string CreatedIp
        {
            get => _createdIp;
            set
            {
                if (_createdIp == value) return;
                _createdIp = value;
                OnPropertyChanged(nameof(CreatedIp));
            }
        }

        /// <summary>
        ///     User IP modified record 
        /// </summary>
        [MaxAllowedLength(DbLengthConstrains.SignRequests.ModifiedIp.MaxLength)]
        public string ModifiedIp
        {
            get => _modifiedIp;
            set
            {
                if (_modifiedIp == value) return;
                _modifiedIp = value;
                OnPropertyChanged(nameof(ModifiedIp));
            }
        }

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
        public bool IsValidSignature{ get; set; }

        /// <summary>
        ///     Signer subject
        /// </summary>
        public string SignSubject { get; set; }

        /// <summary>
        ///     Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Property changed event
        /// </summary>
        /// <param name="propertyName">Changed property name</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            StringInterceptor.ApplyStringMaxAllowedLength(this, propertyName);
        }
    }
}
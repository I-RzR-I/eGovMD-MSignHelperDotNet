// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-09 03:14
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-09 03:23
// ***********************************************************************
//  <copyright file="GenerateSignRequestDataModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using MSignHelperDotNet.Models.InputRequests.SignUnSignDocument;
using MSignRemoteService;

#endregion

// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace MSignHelperDotNet.Models.InputRequests.Internal
{
    internal class GenerateSignRequestDataModel
    {
        internal string ContentDescription { get; set; }
        internal string ShortContentDescription { get; set; }
        internal string SignatureReason { get; set; }
        internal ICollection<SignContentModel> Contents { get; set; }
        internal ContentType ContentType { get; set; }
        internal DelegatorType DelegatorType { get; set; }
        internal string PersonIdentifierId { get; set; }
        internal string CompanyIdentifierId { get; set; }
    }
}
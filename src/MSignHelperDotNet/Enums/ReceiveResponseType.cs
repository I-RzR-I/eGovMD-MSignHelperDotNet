// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-11 19:57
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-11 19:57
// ***********************************************************************
//  <copyright file="ReceiveResponseType.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

// ReSharper disable InconsistentNaming
namespace MSignHelperDotNet.Enums
{
    /// <summary>
    ///     How to get response after sign redirect to local APP
    /// </summary>
    /// <remarks></remarks>
    public enum ReceiveResponseType
    {
        /// <summary>
        ///     Use GET method
        /// </summary>
        GET,

        /// <summary>
        ///     Use POST method
        /// </summary>
        POST
    }
}
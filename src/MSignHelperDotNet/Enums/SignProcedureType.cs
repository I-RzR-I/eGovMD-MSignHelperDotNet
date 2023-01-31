// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-10-30 23:31
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-10-30 23:32
// ***********************************************************************
//  <copyright file="SignProcedureType.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace MSignHelperDotNet.Enums
{
    /// <summary>
    ///     Enum of signing procedure
    /// </summary>
    /// <remarks></remarks>
    public enum SignProcedureType
    {
        /// <summary>
        ///     Undefined procedure code
        /// </summary>
        /// <remarks></remarks>
        Undefined = -1,

        /// <summary>
        ///     Sign procedure
        /// </summary>
        /// <remarks></remarks>
        Sign = 1,

        /// <summary>
        ///     UnSign procedure
        /// </summary>
        /// <remarks></remarks>
        UnSign
    }
}
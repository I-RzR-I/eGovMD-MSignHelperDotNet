// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2023-01-17 09:16
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-17 09:16
// ***********************************************************************
//  <copyright file="PingResponse.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace MSignHelperDotNet.Models
{
    /// <summary>
    ///     Ping response result
    /// </summary>
    public class PingResponse
    {
        /// <summary>
        ///     Execution time
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        ///     Ping response message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Ping success status
        /// </summary>
        public bool IsSuccess{ get; set; }
    }
}
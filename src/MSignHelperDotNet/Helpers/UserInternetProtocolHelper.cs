// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-11 21:35
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-12 00:38
// ***********************************************************************
//  <copyright file="UserInternetProtocolHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Net;
using System.Text.RegularExpressions;
using DomainCommonExtensions.DataTypeExtensions;

#endregion

namespace MSignHelperDotNet.Helpers
{
    /// <summary>
    ///     IP Helper
    /// </summary>
    /// <remarks></remarks>
    public static class UserInternetProtocolHelper
    {
        /// <summary>
        ///     Get client public IP Address
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetUserIp()
        {
            var userIp = string.Empty;
            try
            {
                var wc = new WebClient();
                userIp = wc.DownloadString("https://ifconfig.me/ip");
                userIp = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b").Match(userIp).Value;
                wc.Dispose();
            }
            catch (Exception) { /*ignored*/ }

            return userIp.IsNullOrEmpty() ? "127.0.0.1" : userIp;
        }
    }
}
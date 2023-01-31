// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-11-02 02:41
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-06 19:00
// ***********************************************************************
//  <copyright file="RemoteMSignConstants.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;

#endregion

namespace MSignHelperDotNet.Constants
{
    /// <summary>
    ///     Remote MSing constants
    /// </summary>
    /// <remarks></remarks>
    public static class RemoteMSignConstants
    {
        /// <summary>
        ///     MSign POST HTML string
        /// </summary>
        /// <remarks></remarks>
        public static readonly string MSignHtmlRedirect = @"
                                <!DOCTYPE html>
                                <html lang='en'>
                                <head>
                                    <title>redirecting to MSign...</title>
                                </head>
                                <body onload='document.forms[0].submit();'>
                                    <form method='POST' action='{#PostFormUrl#}'>
                                        <input type='hidden' name='ReturnUrl' value='{#ReturnUrl#}' />
                                        <input type='hidden' name='MSISDN' value='{#MSISDN#}' />
                                        <input type='hidden' name='RelayState' value='{#RelayState#}' />
                                        <input type='hidden' name='lang' value='{#lang#}' />
                                        <p>redirecting to MSign...</p>
                                        <noscript>
                                            <p>
                                                <strong>Note:</strong>
                                                Since your browser does not support Javascript, please click the Continue button once to proceed.
                                            </p>
                                            <div>
                                                <input id='submit' type='submit' value='Continue' />
                                            </div>
                                        </noscript>
                                    </form>
                                </body>
                                </html>
        ";

        /// <summary>
        ///     Available site cultures/languages
        /// </summary>
        /// <remarks></remarks>
        public static readonly IEnumerable<string> AvailableCultures = new List<string>
        {
            "ro",
            "ru",
            "en"
        };
    }
}
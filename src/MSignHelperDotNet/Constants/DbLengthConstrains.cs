// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-10-31 01:46
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-10-31 01:46
// ***********************************************************************
//  <copyright file="DbLengthConstrains.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace MSignHelperDotNet.Constants
{
    /// <summary>
    ///     Specific database table constrains
    /// </summary>
    /// <remarks></remarks>
    public static class DbLengthConstrains
    {
        /// <summary>
        ///  Constrains for tbl => SignRequest
        /// </summary>
        public static class SignRequests
        {
            /// <summary>
            ///     Sign document id constrains
            /// </summary>
            public static class SignDocId
            {
                /// <summary>
                ///     Maximum allowed length
                /// </summary>
                public const int MaxLength = 50;
            }

            /// <summary>
            ///     Sign document number constrains
            /// </summary>
            public static class SignDocNumber
            {
                /// <summary>
                ///     Maximum allowed length
                /// </summary>
                public const int MaxLength = 50;
            }

            /// <summary>
            ///     Request Id constrains
            /// </summary>
            public static class RequestId
            {
                /// <summary>
                ///     Maximum allowed length
                /// </summary>
                public const int MaxLength = 50;
            }

            /// <summary>
            ///    Correlation Id constrains
            /// </summary>
            public static class CorrelationId
            {
                /// <summary>
                ///     Maximum allowed length
                /// </summary>
                public const int MaxLength = 50;
            }

            /// <summary>
            ///     Created IP constrains
            /// </summary>
            public static class CreatedIp
            {
                /// <summary>
                ///     Maximum allowed length
                /// </summary>
                public const int MaxLength = 150;
            }

            /// <summary>
            ///     Modified IP constrains
            /// </summary>
            public static class ModifiedIp
            {
                /// <summary>
                ///     Maximum allowed length
                /// </summary>
                public const int MaxLength = 150;
            }
        }
    }
}
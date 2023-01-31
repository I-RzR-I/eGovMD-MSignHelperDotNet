// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebCoreApp
//  Author           : RzR
//  Created On       : 2023-01-19 20:14
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 21:54
// ***********************************************************************
//  <copyright file="DependencyInjection.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignWebCoreApp.Abstractions;
using SignWebCoreApp.TempData;

#endregion

namespace SignWebCoreApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDb(this IServiceCollection services)
        {
            services.AddSingleton<IAppContext, AppContext>();

            return services;
        }
    }
}
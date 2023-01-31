// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebCoreApp
//  Author           : RzR
//  Created On       : 2023-01-19 20:12
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 21:54
// ***********************************************************************
//  <copyright file="IAppContext.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using Microsoft.EntityFrameworkCore;
using SignWebCoreApp.TempData.Entities;

#endregion

namespace SignWebCoreApp.Abstractions
{
    public interface IAppContext
    {
        DbSet<SignRequestStore> SignRequestStore { get; set; }
        int SaveChanges();
    }
}
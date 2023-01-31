// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebCoreApp
//  Author           : RzR
//  Created On       : 2023-01-19 18:26
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 21:54
// ***********************************************************************
//  <copyright file="AppContext.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SignWebCoreApp.Abstractions;
using SignWebCoreApp.TempData.Entities;

#endregion

namespace SignWebCoreApp.TempData
{
    public class AppContext : DbContext, IAppContext
    {
        protected readonly IConfiguration Configuration;

        public AppContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<SignRequestStore> SignRequestStore { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("SignRequestDb"));
        }
    }
}
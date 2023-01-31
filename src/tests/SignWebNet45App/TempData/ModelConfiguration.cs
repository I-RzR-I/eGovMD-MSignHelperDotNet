// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebNet45App
//  Author           : RzR
//  Created On       : 2023-01-17 07:48
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 10:37
// ***********************************************************************
//  <copyright file="ModelConfiguration.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Data.Entity;
using SignWebNet45App.TempData.Entities;

#endregion

namespace SignWebNet45App.TempData
{
    public class ModelConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            ConfigureSignRequest(modelBuilder);
        }

        private static void ConfigureSignRequest(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SignRequestStore>()
                .ToTable("SignRequestStore");
        }
    }
}
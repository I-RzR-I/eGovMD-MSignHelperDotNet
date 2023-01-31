// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebNet45App
//  Author           : RzR
//  Created On       : 2023-01-17 07:50
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 10:37
// ***********************************************************************
//  <copyright file="DbInitializer.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Data.Entity;
using SignWebNet45App.TempData.Entities;
using SQLite.CodeFirst;

#endregion

namespace SignWebNet45App.TempData
{
    public class DbInitializer : SqliteDropCreateDatabaseWhenModelChanges<AppContext>
    {
        public DbInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder, typeof(EntityHistory))
        {
        }

        protected override void Seed(AppContext context)
        {
            // Here you can seed your core data if you have any.
        }
    }
}
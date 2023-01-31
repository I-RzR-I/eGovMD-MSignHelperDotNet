// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebNet45App
//  Author           : RzR
//  Created On       : 2023-01-17 07:43
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 10:37
// ***********************************************************************
//  <copyright file="AppContext.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Data.Common;
using System.Data.Entity;

#endregion

namespace SignWebNet45App.TempData
{
    public class AppContext : DbContext
    {
        public AppContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configure();
        }

        public AppContext(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {
            Configure();
        }

        private void Configure()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelConfiguration.Configure(modelBuilder);
            var initializer = new DbInitializer(modelBuilder);
            Database.SetInitializer(initializer);
        }
    }
}
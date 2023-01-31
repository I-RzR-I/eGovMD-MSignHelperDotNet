// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebCoreApp
//  Author           : RzR
//  Created On       : 2023-01-19 19:52
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 21:54
// ***********************************************************************
//  <copyright file="AppContextModelSnapshot.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using AppContext = SignWebCoreApp.TempData.AppContext;

#endregion

namespace SignWebCoreApp.Migrations
{
    [DbContext(typeof(AppContext))]
    internal class AppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("SignWebCoreApp.TempData.Entities.SignRequestStore", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("TEXT");

                b.Property<string>("Certificate")
                    .HasColumnType("TEXT");

                b.Property<string>("CorrelationId")
                    .HasColumnType("TEXT");

                b.Property<DateTime>("CreateOn")
                    .HasColumnType("TEXT");

                b.Property<string>("CreatedIp")
                    .HasColumnType("TEXT");

                b.Property<int?>("DocumentTypeId")
                    .HasColumnType("INTEGER");

                b.Property<string>("Hash")
                    .HasColumnType("TEXT");

                b.Property<bool>("IsValidSignature")
                    .HasColumnType("INTEGER");

                b.Property<string>("ModifiedIp")
                    .HasColumnType("TEXT");

                b.Property<DateTime?>("ModifiedOn")
                    .HasColumnType("TEXT");

                b.Property<int>("ProcedureTypeId")
                    .HasColumnType("INTEGER");

                b.Property<string>("RequestId")
                    .HasColumnType("TEXT");

                b.Property<string>("SignDocId")
                    .HasColumnType("TEXT");

                b.Property<string>("SignDocNumber")
                    .HasColumnType("TEXT");

                b.Property<int>("SignStatusId")
                    .HasColumnType("INTEGER");

                b.Property<string>("SignSubject")
                    .HasColumnType("TEXT");

                b.Property<string>("Signature")
                    .HasColumnType("TEXT");

                b.Property<DateTime?>("SignedOn")
                    .HasColumnType("TEXT");

                b.Property<Guid>("UserId")
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.ToTable("SignRequestStore");
            });
        }
    }
}
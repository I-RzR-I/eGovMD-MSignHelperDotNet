﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SignWebCoreApp.TempData;

namespace SignWebCoreApp.Migrations
{
    [DbContext(typeof(SignWebCoreApp.TempData.AppContext))]
    [Migration("20230119180136_Add_SignRequestStore")]
    partial class Add_SignRequestStore
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
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
#pragma warning restore 612, 618
        }
    }
}
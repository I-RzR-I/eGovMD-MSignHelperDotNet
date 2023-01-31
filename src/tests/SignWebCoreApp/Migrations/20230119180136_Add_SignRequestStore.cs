// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebCoreApp
//  Author           : RzR
//  Created On       : 2023-01-19 20:01
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 21:54
// ***********************************************************************
//  <copyright file="20230119180136_Add_SignRequestStore.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace SignWebCoreApp.Migrations
{
    public partial class Add_SignRequestStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "SignRequestStore",
                table => new
                {
                    Id = table.Column<Guid>("TEXT", nullable: false),
                    UserId = table.Column<Guid>("TEXT", nullable: false),
                    SignDocId = table.Column<string>("TEXT", nullable: true),
                    SignDocNumber = table.Column<string>("TEXT", nullable: true),
                    RequestId = table.Column<string>("TEXT", nullable: true),
                    CorrelationId = table.Column<string>("TEXT", nullable: true),
                    Hash = table.Column<string>("TEXT", nullable: true),
                    ProcedureTypeId = table.Column<int>("INTEGER", nullable: false),
                    SignStatusId = table.Column<int>("INTEGER", nullable: false),
                    DocumentTypeId = table.Column<int>("INTEGER", nullable: true),
                    CreateOn = table.Column<DateTime>("TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>("TEXT", nullable: true),
                    SignedOn = table.Column<DateTime>("TEXT", nullable: true),
                    CreatedIp = table.Column<string>("TEXT", nullable: true),
                    ModifiedIp = table.Column<string>("TEXT", nullable: true),
                    Signature = table.Column<string>("TEXT", nullable: true),
                    Certificate = table.Column<string>("TEXT", nullable: true),
                    IsValidSignature = table.Column<bool>("INTEGER", nullable: false),
                    SignSubject = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_SignRequestStore", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "SignRequestStore");
        }
    }
}
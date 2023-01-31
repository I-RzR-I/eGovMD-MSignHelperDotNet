// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.SignWebNet45App
//  Author           : RzR
//  Created On       : 2023-01-17 07:53
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-19 10:37
// ***********************************************************************
//  <copyright file="EntityHistory.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using SQLite.CodeFirst;

#endregion

namespace SignWebNet45App.TempData.Entities
{
    public class EntityHistory : IHistory
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Context { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
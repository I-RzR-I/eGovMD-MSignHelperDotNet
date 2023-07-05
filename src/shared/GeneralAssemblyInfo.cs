// ***********************************************************************
//  Assembly         : RzR.Shared.eGovMD.MSignHelperDotNet
//  Author           : RzR
//  Created On       : 2022-10-21 15:47
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-11-17 19:11
// ***********************************************************************
//  <copyright file="GeneralAssemblyInfo.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Reflection;
using System.Resources;

#endregion

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCompany("RzR ®")]
[assembly: AssemblyProduct("e-GOV MD MSignHelperDotNet")]
[assembly: AssemblyCopyright("Copyright © 2022-2023 RzR All rights reserved.")]
[assembly: AssemblyTrademark("® RzR™")]
[assembly: AssemblyDescription("A wrapper for governmental signing service for a quick way to implement it with a few configuration settings. Service is provided by an e-governance agency (https://egov.md/), named `MSign`, available in the Republic of Moldova.")]

[assembly: AssemblyMetadata("TermsOfService", "")]

[assembly: AssemblyMetadata("ContactUrl", "")]
[assembly: AssemblyMetadata("ContactName", "RzR")]
[assembly: AssemblyMetadata("ContactEmail", "ddpRzR@hotmail.com")]

[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.MainAssembly)]

[assembly: AssemblyVersion("1.0.2.0557")]
[assembly: AssemblyFileVersion("1.0.2.0557")]
[assembly: AssemblyInformationalVersion("1.0.2.*")]
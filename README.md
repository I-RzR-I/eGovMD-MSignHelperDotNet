> **Note** This repository is developed using .net framework 4.5, .netstandard2.0, .netstandard2.1

[![NuGet Version](https://img.shields.io/nuget/v/MSignHelperDotNet.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/MSignHelperDotNet/)
[![Nuget Downloads](https://img.shields.io/nuget/dt/MSignHelperDotNet.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/MSignHelperDotNet)

One important reason for developing this repository is to quickly implement the governmental signing service provided by [e-governance agency](https://egov.md/), named `MSign`, available in the Republic of Moldova.<br/>

[![MSign service](assets/msign.png)](https://msign.gov.md)
<br/>

Proceed to the service portal where you can read more about them by clicking [here](https://msign.gov.md).

The current repository appears as a result of numerous implementations in projects from scratch, losing a lot of time and desire for a more easy way of implementation in new projects.
This repository is a wrapper for the currently available service. Using a few configuration parameters from the application settings file `appsettings.json`, `app.config`, or `web.config` you may implement them very easily into your own application.<br/>
Using the wrapper you will no longer be forced to install the application certificate on the current machine/server.
<br/>

Available configuration settings are: 
* `RemoteServiceClientAddress` -> `MSign` signing service URL;
* `ServiceCertificatePath` -> Service/application certificate for the sign on `MSign` (file with *.pfx at the end);
* `ServiceCertificatePassword` -> Service/application certificate password;
* `RemoteRedirectAddress` -> `MSign` site for processing signing requests.

For more information about that, follow the info from using doc.

**In case you wish to use it in your project, u can install the package from <a href="https://www.nuget.org/packages/MSignHelperDotNet" target="_blank">nuget.org</a>** or specify what version you want:


> `Install-Package MSignHelperDotNet -Version x.x.x.x`

## Content
1. [USING](docs/usage.md)
1. [CHANGELOG](docs/CHANGELOG.md)
1. [BRANCH-GUIDE](docs/branch-guide.md)
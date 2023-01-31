> :point_up: From the beginning let's make clear one moment. It is a wrapper for signing service provided by [e-governance agency](https://egov.md/). If you accessed this repository and already installed it or you want or install the package you already contacted the agency and discussed that and obtained the necessary information for clean implementation. :muscle:

<br/>

In curret wrapper are available a few methods like:
* `SignUnSignDocument`, `SignUnSignDocumentAsync` -> Build a request for the sign or unsign, a POST request to signing service and return result available to save into your storage (or create a mapping to your local storage data), results where to redirect user for completing signing data/documents;
* `CheckDocumentSignStatus`, `CheckDocumentSignStatusAsync` -> Verify/check the status for signed documents by `RequestId` obtained from the previous method;
* `VerifyDocumentSignature`, `VerifyDocumentSignatureAsync` -> Verify the signature of persons who signed documents;
* `Ping` -> Check if the signing service is alive and if you can use it,  available from `MSignClientInternal.Instance.Ping()`.

For the <b><i>first three methods</i></b>, please check the documentation obtained from the responsible authorities where you can find all the smallest details necessary for the implementation and understanding of the working flow.
<br/>
In dependece of what type of project you have, in you configuration file please provide available configurable parameters.<br/>

<hr/>

**Configure the application settings file**

In case you use `netstandard2.0+` in your project find a settings file like `appsettings.json` or `appsettings.env.json` and complete it with the following parameters.
```json
"RemoteMSignOptions": {
   "RemoteServiceClientAddress": "singing service url",
   "ServiceCertificatePath": "certificate.pfx",
   "ServiceCertificatePassword": "password",
   "RemoteRedirectAddress": "redirect singing site"
 }
```

If you have the `app/web.config` file (must common for .net framework projects)
```xml
<appSettings>
  <add key="RemoteServiceClientAddress" value="singing service url" />
  <add key="ServiceCertificatePath" value="certificate.pfx" />
  <add key="ServiceCertificatePassword" value="password" />
  <add key="RemoteRedirectAddress" value="redirect singing site" />
</appSettings>
```

* `RemoteServiceClientAddress` -> supply with URL to signing service like '<b>https://host.domain/Service.svc</b>';
* `ServiceCertificatePath` -> provide service certificate, the path with filename (PFX certificate);
* `ServiceCertificatePassword` -> provide the password to service certificate specified upper;
* `RemoteRedirectAddress` -> provide a link/URL to the site/service where the user will interact with the signing system and finish the signing documents format like '<b>https://host.domain</b>'.

<hr/>

**Calling the service**

In case of using the `netstandard2.0+` in your project, after adding configuration data, you must set dependency injection for using functionality. In your project in the file `Startup.cs` add the following part of the code:
```csharp
public void ConfigureServices(IServiceCollection services)
        {
            ...
            
            services.AddMSingService(Configuration);
            
            ...
        }
```

In the service that will be implemented, inject internal service (`IMSignService`).
```csharp
public class SignDocument
    {
        private readonly IMSignService _mSignService;

        public SignDocument(IMSignService mSignService)
        {
            _mSignService = mSignService;
        }
        
        public  IResult<SignUnSignDocumentResultModel<Guid, Guid>> OnSubmit()
        {
            var hash = "TestHASH".GetHashSha1CryptoProv();
            var sign = _mSignService.SignUnSignDocument<Guid, Guid>(new RemoteSignModel
            {
                ReceiveResponseFrom = ReceiveResponseType.GET,
                ContentDescription = "TestSign CD",
                ShortContentDescription = "TestSign SCD",
                ContentType = ContentType.Hash,
                DelegatorId = null,
                DelegatorType = DelegatorType.Person,
                PhoneNumber = "phone number",
                SignProcedure = SignProcedureType.Sign,
                SignatureReason = "Sign for test",
                SignerId = "user identifier code",
                ReturnUrl = "https://applicationHost.domain/Signed",
                SignContents = new List<SignContentModel>
                {
                    new SignContentModel
                    {
                        SignDocNumber = "1",
                        MultipleSignatures = false,
                        SignDocId = "1",
                        Content = hash,
                        Name = null
                    }
                }
            }, "ro");
            
            return sign;
            }
        }
    }
```

**NetFramework** <br/>
For invoking service from .net framework, the request is almost the same with a small difference, call an instance of the class(`MSignService.Instance`).
```csharp
public class SignDocument
    {
        public  IResult<SignUnSignDocumentResultModel<Guid, Guid>> OnSubmit()
        {
            var hash = "TestHASH".GetHashSha1CryptoProv();
            var sign = MSignService.Instance.SignUnSignDocument<Guid, Guid>(new RemoteSignModel
            {
                ReceiveResponseFrom = ReceiveResponseType.GET,
                ContentDescription = "TestSign CD",
                ShortContentDescription = "TestSign SCD",
                ContentType = ContentType.Hash,
                DelegatorId = null,
                DelegatorType = DelegatorType.Person,
                PhoneNumber = "phone number",
                SignProcedure = SignProcedureType.Sign,
                SignatureReason = "Sign for test",
                SignerId = "user identifier code",
                ReturnUrl = "https://applicationHost.domain/Signed.aspx",
                SignContents = new List<SignContentModel>
                {
                    new SignContentModel
                    {
                        SignDocNumber = "1",
                        MultipleSignatures = false,
                        SignDocId = "1",
                        Content = hash,
                        Name = null
                    }
                }
            }, "ro");
            
            return sign;
            }
        }
    }
```

***The same situation is for all available methods*** (`SignUnSignDocument`, `CheckDocumentSignStatus`, `VerifyDocumentSignature`)

Available languages: "ro", "ru", "en". <br/>
For more information about the fields and how to complete them, you can find in integration guide obtained from the service provider.

From the box, after sending the signing request as result, you will receive a pre-completed object with data necessary for saving in the application context/database type of `SignRequest<TUserId, TRecordId>`. 
* TUserId -> data type for user id;
* TRecordId -> data type for record id (save in DB, primary key).

In dependence on what you need, you can extend with your own properties, then create a map for your DB entity. 

For returned object model is set length restriction for some string properties like: '*SignDocId*', '*SignDocNumber*', '*RequestId*', '*CorrelationId*', '*CreatedIp*', '*ModifiedIp*' and in the case when the length is exceeded, the value will be truncated(using `EntityMaxLengthTrim` and `INotifyPropertyChanged`) at the maximum allowed length (set in the '*MSignHelperDotNet.Constants.SignRequests*').

In addition at clean implementation in the input parameters of sign requests is added:
* `ReceiveResponseFrom` - allow generating how the user will be redirected to the signing site with the allowed value `GET` and `POST`;
* `PhoneNumber` - used for autocomplete input dedicated to users who use mobile signature.

For a more clear understanding of the implementation and using this service, please check the projects `SignWebNet45App` and `SignWebCoreApp` from the current solution. There is presented only one method (a simple method with store temp data, the signing requests into database `SQLite`) of implementation, but you can extend and use it in your own way.

An idea for extending and implementing into your project `VerifyDocumentSignature` more accurately as multiple persons can sign a document, you can create a new entity store and link with `SignRequests` by `RequestId` and `CorrelationId` or link with FK `SignRequests.Id`, and for every person who signed the document you can save data about his signature and validity. A simple example of an entity model:

```csharp
public class SignRequestValidity
    {
        public Guid Id { get; set; }
        public string RequestId { get; set; }
        public string CorrelationId { get; set; }
        public string Certificate { get; set; }
        public DateTime? SignedOn { get; set; }
        public string SignedBy { get; set; }
        public bool IdValid { get; set; }
    }
```

```csharp
public class SignRequestValidity
    {
        public Guid Id { get; set; }
        
        //FK Tbl => SignRequest
        public Guid SignRequestId { get; set; }
        public SignRequest SignRequest { get; set; }
        
        public string Certificate { get; set; }
        public DateTime? SignedOn { get; set; }
        public string SignedBy { get; set; }
        public bool IdValid { get; set; }
    }
```






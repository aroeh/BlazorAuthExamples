# BlazorAuthExamples
Repository containing examples for implementing authentication and authorization in Blazor and APIa

# Dependencies
- Azure
- Microsoft Entra Identity Tenant
- API App Registration
- Client App Registration

# Examples

## AspHostedBlazorAuth
This project is an example of a Blazor WASM application hosted in an asp.net application.  There is an API and a Client app contained in the solution and running all on the same domain.

This project was created using the following command

```
dotnet new blazorwasm -au SingleOrg --api-client-id "<api-client-id>" --app-id-uri "<api-client-id>" --client-id "<app-client-id>" --default-scope "<api-scope>" --domain "<azure-tenant-domain>.onmicrosoft.com" -ho -o <solution-name> --tenant-id "<azure-tenant-id>"
```

> At the time of writing, the project template uses .Net 7.0 for the Client, Server, and Shared projects
> Those projects in this solution have been updated to .Net 8.0 and all nuget packages updated to align


## SeparateClientAndAPI
This project contains individual API and Blazor WASM apps that are intended to be hosted independent of each other.  The same base configuration for Microsoft Entra ID will work, but the Blazor Client app will need to have a custom handler to add the token to the http requests.
 > See [Attach Tokens to Outgoing Requests](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/additional-scenarios?view=aspnetcore-8.0#attach-tokens-to-outgoing-requests) for more details


# Setup

## AspHostedBlazorAuth
1. Build the solution or each project individually
2. Update the Client and Server project appsettings.json and set values for your Azure Tenant and App Registration Client Ids

### Client
1. Update the appsettings.json sections for AzureAd and Scope
2. For AzureAd point to your Azure Tenant and App Registration for the Client application
```
"AzureAd": {
    "Authority": "https://login.microsoftonline.com/22222222-2222-2222-2222-222222222222",
    "ClientId": "33333333-3333-3333-33333333333333333",
    "ValidateAuthority": true
}
```

3. Use the scope level permissions defined on the API App Registration.  Replace <client-id> and <scope-name> with values from the API app registration
```
api://<client-id>/<scope-name>
```

### Server
1. Update the appsettings.json and point to your Azure Tenant and App Registration for the Client application
```
"AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "qualified.domain.name",
    "TenantId": "22222222-2222-2222-2222-222222222222",
    "ClientId": "11111111-1111-1111-11111111111111111",
    "Scopes": "access_as_user",
    "CallbackPath": "/signin-oidc"
}
```

## SeparateClientAndAPI

### ProtectedBlzaorWasm
1. Update the appsettings.json sections for AzureAd and Scope
2. For AzureAd point to your Azure Tenant and App Registration for the Client application
```
"AzureAd": {
    "Authority": "https://login.microsoftonline.com/22222222-2222-2222-2222-222222222222",
    "ClientId": "33333333-3333-3333-33333333333333333",
    "ValidateAuthority": true
}
```

3. Use the scope level permissions defined on the API App Registration.  Replace <client-id> and <scope-name> with values from the API app registration
```
api://<client-id>/<scope-name>
```

### ProtectedWebApi
1. Update the appsettings.json and point to your Azure Tenant and App Registration for the Client application
```
"AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "qualified.domain.name",
    "TenantId": "22222222-2222-2222-2222-222222222222",
    "ClientId": "11111111-1111-1111-11111111111111111",
    "Scopes": "access_as_user",
    "CallbackPath": "/signin-oidc"
}
```

# References
- [Microsoft Identity Platform Docs](https://learn.microsoft.com/en-us/entra/identity-platform/)
- [Microsoft Identity Code Samples](https://learn.microsoft.com/en-us/entra/identity-platform/sample-v2-code)
- [Quickstart Create Microsoft Entra Tenant](https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-create-new-tenant)
- [Configure Group Claims](https://learn.microsoft.com/en-us/entra/identity/hybrid/connect/how-to-connect-fed-group-claims)
- [Entra Identity WebApi](https://learn.microsoft.com/en-us/entra/identity-platform/index-web-api)
- [Protected WebApi Tutorial](https://learn.microsoft.com/en-us/entra/identity-platform/tutorial-web-api-dotnet-register-app)
- [WebApi Turotial](https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-web-api-aspnet-core-protect-api)
- [Secure Blazor WASM Overview](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/?view=aspnetcore-8.0)
- [Secure Hosted ASP.NET Blazor WASM with ME-ID](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/hosted-with-microsoft-entra-id?view=aspnetcore-7.0&viewFallbackFrom=aspnetcore-8.0&source=recommendations)
- [Role Based Access Control](https://learn.microsoft.com/en-us/entra/external-id/customers/how-to-use-app-roles-customers)
- [Question Role Based Auth Azure B2C](https://learn.microsoft.com/en-us/answers/questions/1056490/roled-based-authorization-in-azure-ad-b2c)
- [ASP.NET Core Policy Based Authorization](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-8.0)
- [Blazor Graph API](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/graph-api?pivots=graph-sdk-5&view=aspnetcore-8.0)
- [Postman Oauth2 Documentation](https://learning.postman.com/docs/sending-requests/authorization/oauth-20/)
- [Token Decoding](https://jwt.ms/)
- [dotnet playbook](https://dotnetplaybook.com/secure-a-net-core-api-using-bearer-authentication/)
- [Blazor Help](https://blazorhelpwebsite.com/ViewBlogPost/55)
- [Attach Tokens to Outgoing Requests](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/additional-scenarios?view=aspnetcore-8.0#attach-tokens-to-outgoing-requests)
- [Self Hosted Webassembly Auth](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/hosted-with-microsoft-entra-id?view=aspnetcore-7.0&viewFallbackFrom=aspnetcore-8.0&viewFallbackForm=aspnetcore-8.0&source=recommendations)
- [Webassembly Entra Ids and Roles](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/microsoft-entra-id-groups-and-roles?view=aspnetcore-8.0&pivots=graph-sdk-5)
- [Webassembly Graph Api](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/graph-api?view=aspnetcore-8.0&pivots=graph-sdk-5)
- [ASP.NET Core Microsoft Entra Code Samples](https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/tree/master)
- [Graph API List Member Groups](https://learn.microsoft.com/en-us/graph/api/user-list-memberof?view=graph-rest-1.0&tabs=csharp)
- [Graph API Samples](https://github.com/microsoftgraph/msgraph-sample-blazor-clientside/tree/main)

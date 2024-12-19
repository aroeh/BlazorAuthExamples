# BlazorServerAuth
The Blazor Server Auth is a demonstration of how to authenticate a user on the client application and make HTTP requests to a protected web api.  The Blazor server app is also a small demonstration on distributing functionality between the server and client.


# Dependencies
- Azure
- Microsoft Entra Identity Tenant
- API App Registration
- Client App Registration
- Docker


# App Registration Setup and configuration

1. Login to the Azure Portal or create a new account
2. Create a new App Registration for the API
3. Configure the API App Registration
2. Create a new App Registration for the Client App
3. Configure the Client App Registration

## Create an App Registration

1. Navigate to Microsoft Entra ID

2. Under the left menu, expand Manage and click App Registrations

3. Click New registration
    
    3a. Provide a name for the registration
        > Make sure to use a clear name to indicate environment and usage.  ex: dev-demo-api or dev-demo-client

    3b. For Supported Account Type, use the default selection: Accounts in this organizational directory only (Default Directory only - Single Tenant)

    3c. Click Register


## API App Registration Configuration

1. Create an API app registration using the instructions above and then view it

2. Click Expose an API to configure scopes for the API

> All APIs must have at least one scope published.  For the purposes of this demo there will be 2.

3. Add a scope with the following properties

    3a. Scope name: API.Read

    3b. Who can consent: Admins and users

    3c. Admin consent display name: Read only on API

    3d. Admin consent description: Allow the app to Read the API

    3e. User consent display name: Read via API

    3f. User consent description: Allow the app to Read via the API

4. Add a scope with the following properties

    4a. Scope name: API.ReadWrite

    4b. Who can consent: Admins and users

    4c. Admin consent display name: Read and Write on API

    4d. Admin consent description: Allow the app to Read and Write to the API

    4e. User consent display name: Read and Write via API

    4f. User consent description: Allow the app to Read and Write via the API

5. Click App Roles

> It is a best practice to create an App role to establish permissions for client applications

6. Create an app role with the following properties

    6a. Display name: API.Read.All

    6b. Allowed member types: Applications

    6c. Value: API.Read.All

    6d. Description: Allow the app to read the api

    6e. Do you want to enable this app role? Yes

7. Create an app role with the following properties

    7a. Display name: API.ReadWrite.All

    7b. Allowed member types: Applications

    7c. Value: API.ReadWrite.All

    7d. Description: Allow the app to read and write the api

    7e. Do you want to enable this app role? Yes

8. Configure tokens to assist in acquisition and identity

    8a. Add an optional claim

    8b. Token type: Access

    8c. Select claim: idtyp  description: Signals whether the token is an app-only token

9. Update the Manifest

    9a. If the property named accessTokenAcceptedVersion is available, then set the value to 2
    ```
    "accessTokenAcceptedVersion": 2,
    ```


## Client App Registration Configuration

1. Create an API app registration using the instructions above and then view it

2. Click Authentication to setup sign-in and sign-out endpoint and how to issue tokens

    2a. Add a Web platform with the following Redirect URIs
        * http://localhost/signin-oidc
        * https://localhost/signin-oidc
    
    2b. Front-channel logout URL: https://localhost/signout-oidc

        > The demo uses localhost as the domain, but in a real scenario this would be your application domain ex: contoso.com
    
    2c. Implicit grant and hybrid flows: Select ID tokens

    2d. Supported account types: Accounts in this organizational directory only (Default Directory only - Single tenant)

    2e. Allow public client flows: No

3. Create a client secret

    3a. Description: demo-client-secret

    3b. Expires: Recommended: 180 days (6 months)

    > Make sure to adhere to enterprise practices on the expiration and secret rotations.  And always store and retrieve secrets from a secure vault.

    > Grab the secret value upon creation as that will no longer be visible from the Azure Portal UI

4. Configure tokens to add more information on the user

    8a. Add an optional claim

    8b. Token type: Access

    8c. Select claim: acct  description: User's account status in the tenant

5. Configure API Permissions

> The client app is responsible for signing in users.  Permissions need to be configured and delegated to access the API

    5a. Click add a permission and select My APIs

    5b. Select the API app registration created previously

    5c. Select Delegated permissions

    5d. Select both API.Read and API.ReadWrite

        > It's a best practice in real scenarios to only select permissions needed and not grant more or higher levels of permissions

# Project Configuration

## BlazorServer

1. Update the appsettings.json sections for AzureAd and Scope only for the Server app.  Do not update the appsettings.json for BlazorServer.Client

2. Add the following section for AzureAd configuration and update values to align with your tenant
```
"AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "qualified.domain.name",
    "TenantId": "22222222-2222-2222-2222-222222222222",
    "ClientId": "11111111-1111-1111-11111111111111111",
    "CallbackPath": "/signin-oidc",
    "ResponseType": "code",
    "ClientSecret": "client-secret"
}
```

> The best practice is to retrieve sensitive values from a secure vault.  Do not hardcode those values or commit them to a code repository

3. Add the following section to configure downstream api setup and usage
```
"RestuarantApi": {
    "Scopes": [ "api://<api-client-id>/ServerApp.Read", "api://<api-client-id>/ServerApp.ReadWrite" ],
    "BaseUrl": "http://localhost:5004/",
    "RelativePath": "api"
  }
```

> Scopes can be retrieved and copied from the App Registration configured for the API

## ProtectedApi

1. Update the appsettings.json and point to your Azure Tenant and App Registration for the Client application
```
"AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "qualified.domain.name",
    "TenantId": "22222222-2222-2222-2222-222222222222",
    "ClientId": "11111111-1111-1111-11111111111111111"
}
```

# Run the solution

> Docker and docker compose are the preferred method for running the solution and is how everything has been setup and configured.

1. Start Docker Desktop

2. Build the images
```
docker compose build
```

3. Run the solution
```
docker compose up -d
```

# References
- [Blazor Web App with Entra](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/blazor-web-app-with-entra?view=aspnetcore-9.0)
- [Blazor Sample - Microsoft Entra](https://github.com/dotnet/blazor-samples/tree/main/9.0/BlazorWebAppEntra)
- [fudfomo Blazor Auth Demo](https://github.com/fudfomo/BlazorAuthDemo/tree/main)
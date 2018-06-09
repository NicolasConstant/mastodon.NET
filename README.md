# mastodon.NET
 [Mastodon](https://github.com/tootsuite/mastodon) API Wrapper in C# .NET

 Still work in progress, insights and pull requests welcome!

## Install

Find it on [Nuget](https://www.nuget.org/packages/Mastodon/0.1.0)

```
Install-Package Mastodon 
```

## Current state

As most other mastodon .NET wrappers aren't working anymore, this lib is 100% functionnal for the set of API functionnalities it covers at the date of 09 June 2018.

## API coverage

[WIP]

## How to

 Register app
```csharp
using(var appHandler = new AppHandler("InstanceName")){
    var scopes = AppScopeEnum.Read | AppScopeEnum.Write | AppScopeEnum.Follow;
    var appData = await appHandler.CreateAppAsync("MyAppName", scopes, "ProjectUri");
}
```

 See [scope definition](https://github.com/tootsuite/documentation/blob/master/Using-the-API/OAuth-details.md)

 Retrieve OAuth Token per email/password
```csharp
using(var authHandler = new AuthHandler("InstanceName")){
    var tokenInfo = await authHandler.GetTokenInfoAsync("ClientId", "ClientSecret", "UserEmail", "UserPassword", AppScopeEnum.Read);
}
```
Retrieve OAuth Token per Oauth Code Workflow
```csharp
using(var authHandler = new AuthHandler("InstanceName")){
    var oauthCodeUrl = authHandler.GetOauthCodeUrl("ClientId", AppScopeEnum.Read);
    
    //Open browser/gui to open the oauth url and retrieve the oauth code
    var code = GetCodeFromBrowser(oauthCodeUrl);
    
    var tokenInfo = await authHandler.GetTokenInfoAsync("ClientId", "ClientSecret", code);
}
```


 Access API via Client
```csharp
using(var client = new MastodonClient("InstanceUri")){
    var timeline = await client.GetHomeTimelineAsync("access_token");
}
```

 See [Mastodon API](https://github.com/tootsuite/documentation/blob/master/Using-the-API/API.md)

## Author
 Nicolas Constant ([mastodon](https://mastodon.partipirate.org/@NicolasConstant))

## License 
 mastodon.NET is available under the MIT license. See the LICENSE file for more info.
  

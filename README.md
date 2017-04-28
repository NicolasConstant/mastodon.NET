# mastodon.NET
 [Mastodon](https://github.com/tootsuite/mastodon) API Wrapper in C# .NET

 Still work in progress, insights and pull requests welcome!

## Install mastodon.NET

Find it on [Nuget](https://www.nuget.org/packages/Mastodon/0.1.0)

```
Install-Package Mastodon 
```

## How to

 Register app
```csharp
var appHandler = new AppHandler("InstanceUri");
var scopes = AppScopeEnum.Read | AppScopeEnum.Write | AppScopeEnum.Follow;
var appData = appHandler.CreateApp("MyAppName", "RedirectUri", scopes, "WebsiteUri");
```

 See [scope definition](https://github.com/tootsuite/documentation/blob/master/Using-the-API/OAuth-details.md)

 Retrieve OAuth Token
```csharp
var authHandler = new AuthHandler("InstanceUri");
var tokenInfo = authHandler.GetTokenInfo("ClientId", "ClientSecret", "UserLogin", "UserPassword", AppScopeEnum.Read);
```

 Access API via Client
```csharp
var client = new MastodonClient("InstanceUri");
var timeline = client.GetHomeTimeline("access_token");
```

 See [Mastodon API](https://github.com/tootsuite/documentation/blob/master/Using-the-API/API.md)

## Author
 Nicolas Constant ([mastodon](https://mastodon.partipirate.org/@NicolasConstant))

## License 
 mastodon.NET is available under the MIT license. See the LICENSE file for more info.

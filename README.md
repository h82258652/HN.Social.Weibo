# HN.Social.Weibo
新浪微博 .net API for Desktop and UWP  
[![Build status](https://github.com/h82258652/HN.Social.Weibo/workflows/CI/badge.svg)](https://github.com/h82258652/HN.Social.Weibo/workflows/CI/badge.svg)

| Package                 | Nuget                                                                                                                          | 框架要求                                          |
| -                       | -                                                                                                                              | -                                                 |
| HN.Social.Weibo.Core    | [![Nuget](https://img.shields.io/nuget/v/HN.Social.Weibo.Core.svg)](https://www.nuget.org/packages/HN.Social.Weibo.Core)       | .Net Standard 2.0/.Net Standard 2.1/.Net Core 5.0 |
| HN.Social.Weibo.Desktop | [![Nuget](https://img.shields.io/nuget/v/HN.Social.Weibo.Desktop.svg)](https://www.nuget.org/packages/HN.Social.Weibo.Desktop) | .Net Framework 4.6.1/.Net Core 3.1                |
| HN.Social.Weibo.Uwp     | [![Nuget](https://img.shields.io/nuget/v/HN.Social.Weibo.Uwp.svg)](https://www.nuget.org/packages/HN.Social.Weibo.Uwp)         | UWP 16299 或以上                                  |

本文档编写于 **2020 年 4 月 1 日**

已封装基本的 API，没有的 API 可使用扩展方法来封装  
官方 API 参考  
https://open.weibo.com/wiki/%E5%BE%AE%E5%8D%9AAPI  
https://open.weibo.com/apps/替换为你的应用Id/privilege  

# 使用方法：
### 1、初始化
```C#
IWeiboClient client = new WeiboClientBuilder()
    .WithConfig(options =>
    {
        options.AppKey = "";// 应用信息 - 基本信息 - 应用基本信息 - App Key
        options.AppSecret = "";// 应用信息 - 基本信息 - 应用基本信息 - App Secret
        options.RedirectUri = "";// 应用信息 - 高级信息 - OAuth2.0授权设置 - 授权回调页
        options.Scope = "all";// 可选，参考 https://open.weibo.com/wiki/Scope
    })
    .UseDefaultAuthorizationProvider()// 使用当前平台默认授权器（Desktop 使用 Winform 的 WebBrowser，UWP 使用 WebView）
    .UseMemoryAccessTokenStorage()// 使用内存 access token 存储
    // .UseDefaultAccessTokenStorage()// 或使用当前平台默认 access token 存储（Desktop 使用独立存储，UWP 使用 PasswordVault）
    .Build();
```

### 2、处理错误

除初始化配置缺失的情况外，正常情况下本库所抛出的异常基类均为 ``WeiboException``。建议全局捕获处理。  

Desktop 参考：  
https://github.com/h82258652/HN.Social.Weibo/blob/master/demo/DesktopDemo/App.xaml.cs

UWP 参考：  
https://github.com/h82258652/HN.Social.Weibo/blob/master/demo/UwpDemo/App.xaml.cs

若调用微博接口返回错误，则会转为 ``WeiboApiException``（继承自 ``WeiboException``）异常抛出，开发者可以根据错误码进行处理。

### 3、调用方法
#### 3.1、登入
```C#
try
{
    await client.SignInAsync();
}
catch (UserCancelAuthorizationException)
{
    // TODO 用户取消了授权
}
catch (HttpErrorAuthorizationException)
{
    // TODO 授权期间网络错误
}
```
#### 3.2、登出
```C#
await client.SignOutAsync();
```
#### 3.3、获取是否登入
```C#
bool isSignIn = client.IsSignIn;
```
#### 3.4、获取用户信息
```C#
var user = await client.GetUserAsync(client.UserId);
```
#### 3.5、分享（[参考](https://open.weibo.com/wiki/2/statuses/share)）
```C#
var status = await client.ShareAsync("测试 https://wpdn.bohan.co/az/hprichbg/rb/MandelaMonument_EN-US8903823453_1920x1080.jpg");
```

### 详细请参考 Demo 的代码

返回的错误码请参考微博的文档  
http://open.weibo.com/wiki/Error_code

### 后续计划

1.封装更多常用接口  

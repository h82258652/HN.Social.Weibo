# HN.Social.Weibo
新浪微博 .net API for Desktop and UWP
#### 版本要求
Desktop：.net framework 4.6.1  
UWP：16299

本文档编写于 **2018 年 7 月 17 日**

已封装基本的 API，后续会补充其余 API 的封装，没有的 API 可使用扩展方法来封装

# 使用方法：
### 1、创建 WeiboClientBuilder，执行 WithConfig 方法，并设置验证 Provider 和 AccessToken 的存储服务。
```C#
IWeiboClient client = new WeiboClientBuilder()
    .WithConfig(appKey: "393209958", appSecret: "3c2387aa56497a4ed187f146afc8cb34", redirectUri: "http://bing.coding.io/")
    .UseDefaultAuthorizationProvider()
    .UseDefaultAccessTokenStorage()
    .Build();
```

### 2、调用方法
#### 2.1、登入
```C#
try
{
    await client.SignInAsync();
}
catch (UserCancelAuthorizationException)
{
    // TODO 取消了授权
}
catch (Exception ex) when (ex is HttpErrorAuthorizationException || ex is HttpRequestException)
{
    // TODO 网络错误
}
```
#### 2.2、登出
```C#
await client.SignOutAsync();
```
#### 2.3、获取是否登入
```C#
await client.IsSignIn();
```
#### 2.4、获取用户信息
```C#
try
{
    var userInfo = await client.GetCurrentUserInfoAsync();
    if (userInfo.Success())
    {
        // TODO 获取成功
    }
    else
    {
        // TODO 获取失败
    }
}
catch (HttpRequestException)
{
    // TODO 网络错误
}
```
#### 2.5、分享（可不先登入，第一次调用会自动调起）
```C#
try
{
    var status = await client.ShareAsync("测试 https://wpdn.bohan.co/az/hprichbg/rb/MandelaMonument_EN-US8903823453_1920x1080.jpg");
    if (status.Success())
    {
        // TODO 分享成功
    }
    else
    {
        // TODO 分享失败
    }
}
catch (HttpRequestException)
{
    // TODO 网络错误
}
```

### 详细请参考 Demo 的代码

返回的错误码请参考微博的文档
http://open.weibo.com/wiki/Error_code

### 后续计划

1.封装更多接口  
2.补充文档注释  
3.添加 HttpMessageHandler 的注入  
4.制作 nuget 包

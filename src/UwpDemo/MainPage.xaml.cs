using System;
using System.Net.Http;
using HN.Social.Weibo;
using HN.Social.Weibo.Models;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace UwpDemo
{
    public sealed partial class MainPage
    {
        private readonly IWeiboClient _client;

        public MainPage()
        {
            _client = new WeiboClientBuilder()
                .WithConfig(appKey: "393209958", appSecret: "3c2387aa56497a4ed187f146afc8cb34", redirectUri: "http://bing.coding.io/")
                .UseDefaultAuthorizationProvider()
                .UseDefaultAccessTokenStorage()
                .Build();

            InitializeComponent();
        }

        private async void CheckSignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (await _client.IsSignIn())
            {
                await new MessageDialog("已登录").ShowAsync();
            }
            else
            {
                await new MessageDialog("未登录").ShowAsync();
            }
        }

        private async void GetCurrentUserInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var userInfo = await _client.GetCurrentUserInfoAsync();
            if (!userInfo.Success())
            {
                await new MessageDialog(userInfo.ErrorMessage).ShowAsync();
                return;
            }

            await new MessageDialog("昵称：" + userInfo.Nickname).ShowAsync();
        }

        private async void SendStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var status = await _client.ShareAsync("测试 https://wpdn.bohan.co/az/hprichbg/rb/MandelaMonument_EN-US8903823453_1920x1080.jpg");
            await new MessageDialog(status.Success() ? "发送成功" : status.ErrorMessage).ShowAsync();
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _client.SignInAsync();
            }
            catch (UserCancelAuthorizationException)
            {
                await new MessageDialog("取消了授权").ShowAsync();
            }
            catch (Exception ex) when (ex is HttpErrorAuthorizationException || ex is HttpRequestException)
            {
                await new MessageDialog("网络错误").ShowAsync();
            }
        }

        private async void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            await _client.SignOutAsync();
            await new MessageDialog("登出完成").ShowAsync();
        }
    }
}

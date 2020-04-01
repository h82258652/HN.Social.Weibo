using System;
using HN.Social.Weibo;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using HN.Social.Weibo.Authorization;

namespace UwpDemo
{
    public sealed partial class MainPage
    {
        private readonly IWeiboClient _client;

        public MainPage()
        {
            _client = new WeiboClientBuilder()
                .WithConfig(options =>
                {
                    options.AppKey = "393209958";
                    options.AppSecret = "3c2387aa56497a4ed187f146afc8cb34";
                    options.RedirectUri = "http://bing.coding.io/";
                    options.Scope = "all";// optional
                })
                .UseDefaultAuthorizationProvider()
                .UseDefaultAccessTokenStorage()
                .Build();

            InitializeComponent();
        }

        private async void CheckSignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (_client.IsSignIn)
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
            var user = await _client.GetUserAsync(_client.UserId);
            await new MessageDialog("昵称：" + user.ScreenName).ShowAsync();
        }

        private async void SendStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var status = await _client.ShareAsync("测试 https://wpdn.bohan.co/az/hprichbg/rb/MandelaMonument_EN-US8903823453_1920x1080.jpg");
            await new MessageDialog($"发送成功，内容：{status.Text}").ShowAsync();
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _client.SignInAsync();
            }
            catch (UserCancelAuthorizationException)
            {
                await new MessageDialog("用户取消了授权").ShowAsync();
                return;
            }
            catch (HttpErrorAuthorizationException)
            {
                await new MessageDialog("授权期间网络异常").ShowAsync();
                return;
            }

            await new MessageDialog("登录成功").ShowAsync();
        }

        private async void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            await _client.SignOutAsync();
            await new MessageDialog("登出完成").ShowAsync();
        }
    }
}

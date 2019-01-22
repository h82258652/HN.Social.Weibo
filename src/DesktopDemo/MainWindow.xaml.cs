using System;
using System.Net.Http;
using System.Windows;
using HN.Social.Weibo;
using HN.Social.Weibo.Authorization;
using HN.Social.Weibo.Models;

namespace DesktopDemo
{
    public partial class MainWindow
    {
        private readonly IWeiboClient _client;

        public MainWindow()
        {
            _client = new WeiboClientBuilder()
                .WithConfig(appKey: "393209958", appSecret: "3c2387aa56497a4ed187f146afc8cb34", redirectUri: "http://bing.coding.io/")
                .UseDefaultAuthorizationProvider()
                .UseDefaultAccessTokenStorage()
                .Build();

            InitializeComponent();
        }

        private void CheckSignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (_client.IsSignIn)
            {
                MessageBox.Show("已登录");
            }
            else
            {
                MessageBox.Show("未登录");
            }
        }

        private async void GetCurrentUserInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var userInfo = await _client.GetCurrentUserAsync();
            if (userInfo.ErrorCode == 0)
            {
                MessageBox.Show(userInfo.ErrorMessage);
                return;
            }

            MessageBox.Show("昵称：" + userInfo.Nickname);
        }

        private async void SendStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var status = await _client.ShareAsync("测试 https://wpdn.bohan.co/az/hprichbg/rb/MandelaMonument_EN-US8903823453_1920x1080.jpg");
            MessageBox.Show(status.Success() ? "发送成功" : status.ErrorMessage);
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _client.SignInAsync();
            }
            catch (UserCancelAuthorizationException)
            {
                MessageBox.Show("取消了授权");
            }
            catch (Exception ex) when (ex is HttpErrorAuthorizationException || ex is HttpRequestException)
            {
                MessageBox.Show("网络错误");
            }
        }

        private async void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            await _client.SignOutAsync();
            MessageBox.Show("登出完成");
        }
    }
}

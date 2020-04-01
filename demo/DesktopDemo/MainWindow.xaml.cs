using System.Windows;
using HN.Social.Weibo;
using HN.Social.Weibo.Authorization;

namespace DesktopDemo
{
    public partial class MainWindow
    {
        private readonly IWeiboClient _client;

        public MainWindow()
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
            var user = await _client.GetUserAsync(_client.UserId);
            MessageBox.Show("昵称：" + user.ScreenName);
        }

        private async void SendStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var status = await _client.ShareAsync("测试 https://wpdn.bohan.co/az/hprichbg/rb/MandelaMonument_EN-US8903823453_1920x1080.jpg");
            MessageBox.Show($"发送成功，内容：{status.Text}");
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _client.SignInAsync();
            }
            catch (UserCancelAuthorizationException)
            {
                MessageBox.Show("用户取消了授权");
                return;
            }
            catch (HttpErrorAuthorizationException)
            {
                MessageBox.Show("授权期间网络异常");
                return;
            }

            MessageBox.Show("登录成功");
        }

        private async void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            await _client.SignOutAsync();
            MessageBox.Show("登出完成");
        }
    }
}

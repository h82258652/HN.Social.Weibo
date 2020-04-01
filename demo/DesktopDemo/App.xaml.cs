using System.Windows;
using HN.Social.Weibo;

namespace DesktopDemo
{
    public partial class App
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is WeiboException ex)
            {
                e.Handled = true;
                if (ex is WeiboApiException apiException)
                {
                    var error = apiException.Error;
                    MessageBox.Show($"错误代码：{error.ErrorCode}，错误消息：{error.ErrorMessage}");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }

                return;
            }
        }
    }
}

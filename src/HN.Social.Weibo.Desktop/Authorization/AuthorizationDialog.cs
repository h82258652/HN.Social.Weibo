using System;
using System.Web;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace HN.Social.Weibo.Authorization
{
    /// <summary>
    /// 授权对话框。
    /// </summary>
    public partial class AuthorizationDialog : Form
    {
        private readonly Uri _authorizeUri;

        /// <summary>
        /// 初始化 <see cref="AuthorizationDialog" /> 类的新实例。
        /// </summary>
        /// <param name="authorizeUri">授权地址。</param>
        public AuthorizationDialog([NotNull] Uri authorizeUri)
        {
            if (authorizeUri == null)
            {
                throw new ArgumentNullException(nameof(authorizeUri));
            }

            _authorizeUri = authorizeUri;

            InitializeComponent();
        }

        /// <summary>
        /// 授权码。
        /// </summary>
        public string AuthorizationCode
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否出现 Http 错误。
        /// </summary>
        public bool IsHttpError { get; private set; }

        private void AuthorizationDialog_Shown(object sender, EventArgs e)
        {
            ((SHDocVw.WebBrowser)webBrowser.ActiveXInstance).NavigateError += WebBrowser_NavigateError;

            webBrowser.Navigate(_authorizeUri);
        }

        private bool CheckUrlQuery(Uri url)
        {
            var queryString = url.Query;
            var lastIndex = queryString.LastIndexOf('?');
            if (lastIndex < 0)
            {
                return false;
            }

            queryString = queryString.Substring(lastIndex);
            var query = HttpUtility.ParseQueryString(queryString);
            var code = query.Get("code");
            if (code == null)
            {
                return false;
            }

            AuthorizationCode = code;
            DialogResult = DialogResult.OK;
            return true;
        }

        private void WebBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            CheckUrlQuery(e.Url);
        }

        private void WebBrowser_NavigateError(object pDisp, ref object URL, ref object Frame, ref object StatusCode, ref bool Cancel)
        {
            if (CheckUrlQuery(new Uri((string)URL)))
            {
                return;
            }

            IsHttpError = true;
            DialogResult = DialogResult.Abort;
        }
    }
}

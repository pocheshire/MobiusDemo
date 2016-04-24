using System;
using Android.Webkit;
using Android.Content;
using Android.Util;
using Android.Runtime;
using Demo.API;

namespace Demo.Android.Controls
{
    [Register("MvxWebView")]
    public class MvxWebView : WebView
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (string.IsNullOrEmpty(value)) 
                    return;

                _text = value;

                var client = new WebViewClient();
                client.ShouldOverrideUrlLoading(this, null);

                SetWebViewClient(client);

                this.Settings.JavaScriptCanOpenWindowsAutomatically = true;
                this.Settings.JavaScriptEnabled = true;

                LoadData(_text, "text/html; charset=UTF-8", null);
            }
        }

        public event Action Successed;
        public event Action Failed;

        public MvxWebView (Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.Download += HandleLoadFinished;
        }

        private void HandleLoadFinished(object sender, DownloadEventArgs e)
        {
            var url = e.Url.ToString();

            if (url.Contains(AppData.YaSuccessUri))
                Successed?.Invoke();

            if (url.Contains(AppData.YaFailUri))
                Failed?.Invoke();
        }
    }
}


using System;
using UIKit;
using Demo.API;
using Foundation;
using System.IO;

namespace Demo.iOS.Controls
{
    public class YaMoneyView : UIWebView
    {
        public event Action Successed;
        public event Action Failed;

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                if (!value.IsNullOrEmpty())
                    LoadHtmlString (value, new NSUrl (Path.Combine (NSBundle.MainBundle.BundlePath, "Content/"), true));
            }
        }

        public YaMoneyView()
        {
            LoadFinished += (s, e) => HandleLoadFinished(AppData.YaSuccessUri, AppData.YaFailUri);
        }

        void HandleLoadFinished(string  successUrl, string failUrl)
        {
            var url = Request.Url.ToString();

            if (url.Contains(successUrl))
            {
                OnSuccess();
            }

            if (url.Contains(failUrl))
            {
                OnFail();
            }
        }

        protected virtual void OnSuccess()
        {
            var handler = Successed;
            if (handler != null)
                handler();
        }

        protected virtual void OnFail()
        {
            var handler = Failed;
            if (handler != null)
                handler();
        }
    }
}


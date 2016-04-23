using System;
using Demo.Core.ViewModels.Pay;
using Android.App;
using Android.OS;
using Demo.Android.Controls;

namespace Demo.Android.Views.Pay
{
    [Activity]
    public class PayActivity : CommonActivity<PayViewModel>
    {
        public PayActivity()
            : base (Resource.Layout.Activity_Pay, "Оплата")
        {
            ShowBackButton = true;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var webView = FindViewById<MvxWebView>(Resource.Id.webView);
            webView.Successed += () => ViewModel.PaymentSucceeded();
            webView.Failed += () => ViewModel.PaymentFailed();
        }
    }
}


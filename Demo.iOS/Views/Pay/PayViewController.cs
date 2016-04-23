using System;

using UIKit;
using Demo.Core.ViewModels.Pay;
using Demo.iOS.Controls;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;

namespace Demo.iOS.Views.Pay
{
    public partial class PayViewController : CommonViewController<PayViewModel>
    {
        private YaMoneyView _webView;

        public PayViewController()
        {
        }

        protected override void BindControls()
        {
            var set = this.CreateBindingSet<PayViewController, PayViewModel>();
            set.Bind(_webView).For("Text").To(vm => vm.HtmlText);
            set.Apply();

            _webView.Successed += ViewModel.PaymentSucceeded;
            _webView.Failed += ViewModel.PaymentFailed;
        }

        protected override void InitializeControls()
        {
            Title = "Оплата";

            _webView = new YaMoneyView () {
                Frame = new CoreGraphics.CGRect (0f, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 64f)
            };

            View.AddSubview (_webView);
        }
    }
}



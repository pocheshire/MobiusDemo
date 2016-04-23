using System;

using UIKit;
using Demo.Core.ViewModels.Thanks;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;

namespace Demo.iOS.Views.Thanks
{
    public partial class ThanksViewController : CommonViewController<ThanksViewModel>
    {
        private UILabel _shopName;
        private UILabel _productName;
        private UILabel _price;
        private UILabel _pinCode;

        public ThanksViewController()
        {
        }

        protected override void BindControls()
        {
            var set = this.CreateBindingSet<ThanksViewController, ThanksViewModel>();
            set.Bind(_shopName).To(vm => vm.ShopName);
            set.Bind(_productName).To(vm => vm.ProductName);
            set.Bind(_price).To(vm => vm.Price);
            set.Bind(_pinCode).To(vm => vm.PinCode);
            set.Apply();
        }

        protected override void InitializeControls()
        {
            Title = "Спасибо!";

            var thanks = new UILabel (new CGRect (10f, 70f, UIScreen.MainScreen.Bounds.Width - 20f, 50f)) {
                Text = "Оплата совершена.\nСпасибо за покупку!",
                Lines = 2,
                Font = UIFont.SystemFontOfSize (16f),
                TextAlignment = UITextAlignment.Center
            };

            _shopName = new UILabel (new CGRect (10f, thanks.Frame.Bottom + 30f, UIScreen.MainScreen.Bounds.Width - 20f, 20f));
            _shopName.SetTitle ();

            _productName = new UILabel (new CGRect (10f, _shopName.Frame.Bottom + 5f, UIScreen.MainScreen.Bounds.Width - 20f, 20f));
            _productName.SetTitle ();

            _price = new UILabel (new CGRect (10f, _productName.Frame.Bottom + 5f, UIScreen.MainScreen.Bounds.Width - 20f, 20f));
            _price.SetTitle ();

            _pinCode = new UILabel (new CGRect (10f, _price.Frame.Bottom + 30f, UIScreen.MainScreen.Bounds.Width - 20f, 60f)) {
                Lines = 3,
                Font = UIFont.SystemFontOfSize (16f),
                TextAlignment = UITextAlignment.Center
            };

            View.AddSubviews (thanks, _shopName, _productName, _price, _pinCode);
        }
    }
}



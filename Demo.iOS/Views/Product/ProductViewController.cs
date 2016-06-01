using System;

using UIKit;
using Demo.Core.ViewModels.Product;
using CoreGraphics;
using AudioToolbox;
using MvvmCross.Binding.BindingContext;

namespace Demo.iOS.Views.Product
{
    public partial class ProductViewController : CommonViewController<ProductViewModel>
    {
        private UILabel _shopName;
        private UILabel _productName;
        private UILabel _price;
        private UIButton _basketButton;

        protected override void BindControls()
        {
            var set = this.CreateBindingSet<ProductViewController, ProductViewModel>();
            set.Bind(_shopName).To(vm => vm.ShopName);
            set.Bind(_productName).To(vm => vm.ProductName);
            set.Bind(_price).To(vm => vm.Price);
            set.Bind(_basketButton).To(vm => vm.BasketCommand);
            set.Apply();
        }

        protected override void InitializeControls()
        {
            Title = "Предложение";

            UILabel label = new UILabel (new CGRect (0f, 0f, UIScreen.MainScreen.Bounds.Width, 44f)) {
                Text = "Поиск завершен.\nНайдено предложение!",
                Lines = 2,
                BackgroundColor = UIColor.FromRGB (240, 234, 228),
                TextColor = UIColor.Black,
                Font = UIFont.SystemFontOfSize (14f),
                TextAlignment = UITextAlignment.Center
            };
            View.AddSubview (label);

            //Вибрация на событие
            SystemSound.Vibrate.PlayAlertSound ();

            _shopName = new UILabel (new CGRect (10f, label.Frame.Bottom + 10f, UIScreen.MainScreen.Bounds.Width - 20f, 20f));
            _shopName.SetTitle ();

            _productName = new UILabel (new CGRect (10f, _shopName.Frame.Bottom + 10f, UIScreen.MainScreen.Bounds.Width - 20f, 20f));
            _productName.SetTitle ();

            _price = new UILabel (new CGRect (10f, _productName.Frame.Bottom + 10f, UIScreen.MainScreen.Bounds.Width - 20f, 26f));
            _price.SetTitle();

            _basketButton = new UIButton (new CGRect (10f, _price.Frame.Bottom + 10f, UIScreen.MainScreen.Bounds.Width - 20f, 40f)) {
                BackgroundColor = UIColor.FromRGB (232, 100, 90)
            };
            _basketButton.SetTitle ("КУПИТЬ ТОВАР", UIControlState.Normal);
            _basketButton.SetTitleColor (UIColor.White, UIControlState.Normal);
            _basketButton.Layer.CornerRadius = 2f;

            View.AddSubviews (
                _shopName, _productName, _price, _basketButton
            );
        }
    }
}



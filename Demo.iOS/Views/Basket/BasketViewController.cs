using System;

using UIKit;
using Demo.Core.ViewModels.Basket;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;

namespace Demo.iOS.Views.Basket
{
    public partial class BasketViewController : CommonViewController<BasketViewModel>
    {
        private UILabel _shopName;
        private UILabel _productName;
        private UILabel _price;

        private UITextField _name;
        private UITextField _phone;

        private UIScrollView _dataView;

        UIButton _payButton;

        public BasketViewController()
        {
            RegisterKeyboardActions = true;
        }

        protected override void BindControlls()
        {
            var set = this.CreateBindingSet<BasketViewController, BasketViewModel>();
            set.Bind(_shopName).To(vm => vm.ShopName);
            set.Bind(_productName).To(vm => vm.ProductName);
            set.Bind(_price).To(vm => vm.Price);
            set.Bind(_name).To(vm => vm.UserName);
            set.Bind(_phone).For("PhoneBinding").To(vm => vm.UserPhone);
            set.Bind(_payButton).To(vm => vm.PayCommand);
            set.Apply();
        }

        protected override void InitializeControlls()
        {
            Title = "Оформление";

            _dataView = new UIScrollView (new CGRect (new CGPoint (0, 0), new CGSize (UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 64)));
            View.AddSubview (_dataView);

            var titleName = new UILabel (new CGRect (10f, 26f, UIScreen.MainScreen.Bounds.Width - 20f, 26f)) {
                Text = "Вы покупаете:",
                Font = UIFont.SystemFontOfSize (20f),
                TextAlignment = UITextAlignment.Center
            };

            UIImage billImage = UIImage.FromFile ("Images/bill.png");
            var billImageView = new UIImageView (new CGRect (new CGPoint ((UIScreen.MainScreen.Bounds.Width - billImage.Size.Width) / 2, titleName.Frame.Bottom + 20f), 
                billImage.Size)) {
                Image = billImage
            };

            _shopName = new UILabel (new CGRect (billImageView.Frame.X + 10f, billImageView.Frame.Y + 20f, billImageView.Frame.Width - 20f, 20f));
            _shopName.SetTitle ();

            _productName = new UILabel (new CGRect (_shopName.Frame.X, _shopName.Frame.Bottom + 5f, _shopName.Frame.Width, 20f));
            _productName.SetTitle ();

            _price = new UILabel (new CGRect (_shopName.Frame.X, _productName.Frame.Bottom + 10f, _shopName.Frame.Width, 26f));
            _price.SetTitle();

            _name = new UITextField (new CGRect (10f, billImageView.Frame.Bottom + 20f, UIScreen.MainScreen.Bounds.Width - 20f, 40f));
            _name.SetTextField (UIKeyboardType.Default, "Ваше имя");            

            _phone = new UITextField (new CGRect (10f, _name.Frame.Bottom + 10f, UIScreen.MainScreen.Bounds.Width - 20f, 40f));
            _phone.SetTextField (UIKeyboardType.NumberPad, "Ваш номер телефона ");

            _payButton = new UIButton (new CGRect (10f, _phone.Frame.Bottom + 20f, UIScreen.MainScreen.Bounds.Width - 20f, 40f)) {
                BackgroundColor = UIColor.Black
            };
            _payButton.SetTitle ("ОПЛАТИТЬ", UIControlState.Normal);
            _payButton.SetTitleColor (UIColor.White, UIControlState.Normal);
            _payButton.Layer.CornerRadius = 2f;

            _dataView.AddSubviews (titleName, billImageView, _shopName, _productName, _price, _name, _phone, _payButton);

            _dataView.ContentSize = new CGSize(UIScreen.MainScreen.Bounds.Width, _payButton.Frame.Bottom + 20);
        }
    }
}



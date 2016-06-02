using System;

using UIKit;
using Demo.Core.ViewModels.Product;
using MvvmCross.Binding.BindingContext;

namespace Demo.iOS.Views.Product
{
    public partial class ProductViewController : CommonViewController<ProductViewModel>
    {
        public ProductViewController()
            : base("ProductViewController", null)
        {
            Title = "Предложение";
        }

        protected override void BindControls()
        {
            var set = this.CreateBindingSet<ProductViewController, ProductViewModel>();
            set.Bind(_shopName).To(vm => vm.ShopName);
            set.Bind(_productName).To(vm => vm.ProductName);
            set.Bind(_price).To(vm => vm.Price);
            set.Bind(_basketButton).To(vm => vm.BasketCommand);
            set.Apply();
        }
    }
}



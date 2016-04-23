using System;
using Android.Content.PM;
using Android.App;
using Demo.Core.ViewModels.Product;

namespace Demo.Android.Views.Product
{
    [Activity]
    public class ProductActivity : CommonActivity<ProductViewModel>
    {
        public ProductActivity()
            : base(Resource.Layout.Activity_Product, "Предложение")
        {
            ShowBackButton = true;
        }
    }
}


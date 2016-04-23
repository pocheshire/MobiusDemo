using System;
using Demo.Core.ViewModels.Basket;
using Android.App;

namespace Demo.Android.Views.Basket
{
    [Activity]
    public class BasketActivity : CommonActivity<BasketViewModel>
    {
        public BasketActivity()
            : base (Resource.Layout.Activity_Basket, "Корзина")
        {
            ShowBackButton = true;
        }
    }
}


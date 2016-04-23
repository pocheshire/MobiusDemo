using System;

using UIKit;
using Demo.Core.ViewModels.Main;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;

namespace Demo.iOS.Views.Main
{
    public partial class MainViewController : CommonViewController<MainViewModel>
    {
        protected override void BindControls()
        {
            
        }

        protected override void InitializeControls()
        {
            base.InitializeControls();

            Title = "Поиск";

            View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("Images/main.jpg"));
        }
    }
}



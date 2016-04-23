using System;
using Android.App;
using Demo.Core.ViewModels.Thanks;

namespace Demo.Android.Views.Thanks
{
    [Activity]
    public class ThanksActivity : CommonActivity<ThanksViewModel>
    {
        public ThanksActivity()
            : base (Resource.Layout.Activity_Thanks, "Спасибо")
        {
        }
    }
}


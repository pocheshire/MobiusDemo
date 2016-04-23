using System;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Core.ViewModels;
using Android.Views;
using Android.Content;
using Android.Util;
using MvvmCross.Binding.Droid.BindingContext;
using Android.OS;
using Android.App;
using Android.Content.PM;
using Android.Support.V7.Widget;

namespace Demo.Android.Views
{
    public class CommonActivity<T> : MvxAppCompatActivity<T>
        where T : class, IMvxViewModel
    {
        private int _layoutId = -1;
        private string _title;

        protected bool ShowBackButton { get; set; }

        public CommonActivity()
        {
            
        }

        public CommonActivity(int layoutId, string title)
        {
            _layoutId = layoutId;
            _title = title;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (_layoutId != -1)
                SetContentView(_layoutId);

            var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);

                SupportActionBar.Title = _title;

                SupportActionBar.SetDisplayHomeAsUpEnabled (ShowBackButton);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == global::Android.Resource.Id.Home)
                OnBackPressed();

            return base.OnOptionsItemSelected(item);
        }
    }
}


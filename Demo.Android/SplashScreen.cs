using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace Demo.Android
{
    [Activity(NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.Activity_Splash)
        {
        }
    }
}

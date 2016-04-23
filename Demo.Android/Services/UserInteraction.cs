using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Widget;
using AlertDialog = Android.Support.V7.App.AlertDialog/*.App.AlertDialog*/;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Platform;
using Demo.Core.Services;

namespace Demo.Android.Services
{
    public class UserInteraction : IUserInteraction
    {
        protected Activity CurrentActivity
        {
            get { return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity; }
        }

        public void Alert(string message, Action done = null, string title = "", string okButton = "OK")
        {
            Application.SynchronizationContext.Post(ignored =>
                {
                    if (CurrentActivity == null)
                        return;
                    try
                    {
                        var dialog = new AlertDialog.Builder(CurrentActivity)
                            .SetMessage(message)
                            .SetTitle(title)
                            .SetPositiveButton(okButton, delegate
                            {
                                if (done != null)
                                    done();
                            }).Create();
                        dialog.Show();
                    }
                    catch (Exception ex)
                    {

                    }
                }, null);
        }
    }
}


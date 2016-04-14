using System;
using UIKit;
using Foundation;
using System.Threading.Tasks;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using Demo.Core.Services;

namespace Demo.iOS.Services
{
    public class ExtendedUserInteraction : IExtendedUserInteraction
    {
        public void Alert(string message, Action done = null, string title = "", string okButton = "OK")
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    var alert = new UIAlertView(title ?? string.Empty, message, null, okButton);
                    if (done != null)
                    {
                        alert.Clicked += (sender, args) => done();
                    }
                    alert.Show();
                });
        }
    }
}


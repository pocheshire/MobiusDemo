using System;
using Demo.Core.Services;
using Demo.API.Models.Beacons;
using System.Collections.Generic;
using Android.App;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Platform;
using Demo.Android.Views.Main;

namespace Demo.Android.Services
{
    public class BeaconService : IBeaconService
    {
        protected Activity TopActivity 
        {
            get { return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity; }
        }

        #region IBeaconService implementation

        public void Start(List<BeaconModel> beacons)
        {
            (TopActivity as MainActivity).StartRagingBeacons(beacons);
        }

        public void Stop()
        {
            (TopActivity as MainActivity).StopRagingBeacons();
        }

        #endregion
    }
}


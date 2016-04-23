using System;
using Demo.Core.ViewModels.Main;
using Android.App;
using Android.Content.PM;
using System.Collections.Generic;
using Demo.API.Models.Beacons;
using AltBeaconOrg.BoundBeacon;
using System.Linq;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Demo.Core.Messages;

namespace Demo.Android.Views.Main
{
    [Activity(MainLauncher = true)]
    public class MainActivity : CommonActivity<MainViewModel>, IBeaconConsumer
    {
        public MainActivity()
            : base(Resource.Layout.Activity_Main, "Поиск")
        {
            
        }

        #region Beacon

        private const int BEACONS_UPDATES_IN_SECONDS = 5;
        private const long BEACONS_UPDATES_IN_MILLISECONDS = BEACONS_UPDATES_IN_SECONDS * 1000;

        private Region _rangingRegion;

        private RangeNotifier _rangeNotifier;

        private List<BeaconModel> _listOfBeacons;
        private BeaconManager _beaconManager;

        private void RangingBeaconsInRegion(object sender, ICollection<Beacon> beacons)
        {
            if (beacons != null && beacons.Count > 0)
            {
                var foundBeacons = beacons.ToList();
                foreach (var beacon in beacons)
                {
                    Mvx.Resolve<IMvxMessenger>().Publish<BeaconFoundMessage>(
                        new BeaconFoundMessage(
                            this, 
                            beacon.Id1.ToString(), 
                            (ushort)Convert.ToInt32(beacon.Id2.ToString()), 
                            (ushort)Convert.ToInt32(beacon.Id3.ToString()))
                    );
                }
            }

        }

        public void StartRagingBeacons(List<BeaconModel> beacons)
        {
            _beaconManager = BeaconManager.GetInstanceForApplication(this);

            _rangeNotifier = new RangeNotifier();
            _listOfBeacons = beacons;

            //iBeacon
            _beaconManager.BeaconParsers.Add(new BeaconParser().SetBeaconLayout("m:2-3=0215,i:4-19,i:20-21,i:22-23,p:24-24"));

            _beaconManager.Bind(this);
        }

        public void StopRagingBeacons()
        {
            _beaconManager.StopRangingBeaconsInRegion(_rangingRegion);
            _beaconManager.Unbind(this);
        }

        public class RangeNotifier : Java.Lang.Object, IRangeNotifier
        {
            public event EventHandler<ICollection<Beacon>> DidRangeBeaconsInRegionComplete;

            public void DidRangeBeaconsInRegion(ICollection<Beacon> beacons, Region region)
            {
                DidRangeBeaconsInRegionComplete?.Invoke(this, beacons);
            }
        }

        #endregion

        #region IBeaconConsumer implementation

        public void OnBeaconServiceConnect()
        {
            _beaconManager.SetForegroundScanPeriod(BEACONS_UPDATES_IN_MILLISECONDS);
            _beaconManager.SetForegroundBetweenScanPeriod(BEACONS_UPDATES_IN_MILLISECONDS);

            _beaconManager.SetBackgroundScanPeriod(BEACONS_UPDATES_IN_MILLISECONDS);
            _beaconManager.SetBackgroundBetweenScanPeriod(BEACONS_UPDATES_IN_MILLISECONDS);

            _beaconManager.UpdateScanPeriods();

            _rangeNotifier.DidRangeBeaconsInRegionComplete += RangingBeaconsInRegion;
            _beaconManager.SetRangeNotifier(_rangeNotifier);

            _rangingRegion = new AltBeaconOrg.BoundBeacon.Region("region_uid", null, null, null);
            _beaconManager.StartRangingBeaconsInRegion(_rangingRegion);
        }

        #endregion
    }
}


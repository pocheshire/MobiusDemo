using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using CoreLocation;
using Demo.API.Models.Beacons;
using Demo.Core.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Messenger;
using Demo.Core.Messages;

namespace Demo.iOS.Services
{
    public class BeaconService : IBeaconService
	{
		#region Fields

		private readonly CLLocationManager _locationMgr;
		private readonly List<CLBeaconRegion> _listOfCLBeaconRegion;

		#endregion

		#region Constructor

		public BeaconService ()
		{
			_locationMgr = new CLLocationManager ();
			_listOfCLBeaconRegion = new List<CLBeaconRegion> ();
		}

		#endregion

        #region IBeaconService implementation

        public void Start (List<BeaconRegionModel> beacons)
        {
            _locationMgr.RequestAlwaysAuthorization ();

            _locationMgr.DidRangeBeacons += HandleDidRangeBeacons;
            _locationMgr.DidDetermineState += HandleDidDetermineState;
            _locationMgr.PausesLocationUpdatesAutomatically = false;    
            _locationMgr.StartUpdatingLocation ();
            _locationMgr.RequestAlwaysAuthorization ();

            //Начинаем мониторинг
            foreach (var ibeacon in beacons) 
            {
                var clBeaconRegion = new CLBeaconRegion (new NSUuid (ibeacon.UUID), ibeacon.Major, ibeacon.Minor, ibeacon.ID.ToString ());
                clBeaconRegion.NotifyEntryStateOnDisplay = true;
                clBeaconRegion.NotifyOnEntry = true;
                clBeaconRegion.NotifyOnExit = true;

                _listOfCLBeaconRegion.Add (clBeaconRegion);

                _locationMgr.StartMonitoring (clBeaconRegion);
                _locationMgr.StartRangingBeacons (clBeaconRegion);

                Mvx.Resolve<IMvxTrace>().Trace (MvxTraceLevel.Diagnostic, "Beacon", "Start debug " + ibeacon.ID);
            }

//            #if DEBUG
//            var beacon = _listOfCLBeaconRegion.First();
//            string uuid = beacon.ProximityUuid.AsString ();
//            var major = (ushort)beacon.Major;
//            var minor = (ushort)beacon.Minor;
//
//            Mvx.Resolve<IMvxMessenger>().Publish<BeaconChangeProximityMessage> (
//                new BeaconChangeProximityMessage (this, uuid, major, minor)
//            );
//            #endif
        }

        public void Stop()
        {
            foreach (var beaconRegion in _listOfCLBeaconRegion)
            {
                _locationMgr.StopRangingBeacons(beaconRegion);
                _locationMgr.StopMonitoring(beaconRegion);
            }

            _listOfCLBeaconRegion.Clear();

            _locationMgr.DidRangeBeacons -= HandleDidRangeBeacons;
            _locationMgr.StopUpdatingLocation();
        }

        #endregion

        #region Beacon monitoring

		private void HandleDidDetermineState (object sender, CLRegionStateDeterminedEventArgs e)
		{
            Mvx.Resolve<IMvxTrace>().Trace (MvxTraceLevel.Diagnostic, "Beacon", "HandleDidDetermineState");
		}

		private void HandleDidRangeBeacons (object sender, CLRegionBeaconsRangedEventArgs e)
		{
            foreach (var beacon in e.Beacons)
                SendBeaconChangeProximity(beacon);
		}

		private  void SendBeaconChangeProximity (CLBeacon beacon)
		{
            Mvx.Resolve<IMvxTrace>().Trace (MvxTraceLevel.Diagnostic, "Beacon", string.Format("Founded Beacon {1}:{2}:{3} - {4} - {0}", beacon.Proximity, beacon.ProximityUuid.AsString(), beacon.Major, beacon.Minor, beacon.Rssi));

			if (beacon.Proximity == CLProximity.Unknown) 
            {
                Mvx.Resolve<IMvxTrace>().Trace (MvxTraceLevel.Diagnostic, "Beacon", "Location Unknown" + beacon.Description);
				return;
			}

			string uuid = beacon.ProximityUuid.AsString ();
            var major = (ushort)beacon.Major;
            var minor = (ushort)beacon.Minor;

            Mvx.Resolve<IMvxMessenger>().Publish<BeaconChangeProximityMessage> (
                new BeaconChangeProximityMessage (this, uuid, major, minor)
            );

//            LocalNotification();
		}

		private void LocalNotification ()
		{
			var notification = new UILocalNotification ();

			// configure the alert stuff
			notification.AlertAction = "Персональное предложение";
			notification.AlertBody = "Специально для вас товар со скидкой";

			// modify the badge
			//notification.ApplicationIconBadgeNumber = 1;

			// set the sound to be the default sound
			notification.SoundName = UILocalNotification.DefaultSoundName;

            UIApplication.SharedApplication.PresentLocalNotificationNow (notification);
		}

        #endregion
    }
}


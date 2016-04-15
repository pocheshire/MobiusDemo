using System;
using Demo.Core.Services;
using MvvmCross.Platform;
using Demo.API.Services;
using Demo.Core.Messages;
using System.Collections.Generic;
using Demo.API.Models.Beacons;
using System.Linq;
using Demo.Core.ViewModels.Product;
using MvvmCross.Platform.Platform;

namespace Demo.Core.ViewModels.Main
{
    public class MainViewModel : CommonViewModel
    {
        private static MvvmCross.Plugins.Messenger.MvxSubscriptionToken _beaconToken;

        private IBeaconService _beaconService;

        internal List<BeaconRegionModel> Beacons { get; private set; }

        private bool _loading;
        public bool Loading
        {
            get
            {
                return _loading;
            }
            set
            {
                _loading = value;
                RaisePropertyChanged(() => Loading);
            }
        }

        public MainViewModel()
        {
            Hint.NavigationType = NavigationType.ClearAndPush;

            _beaconService = Mvx.Resolve<IBeaconService>();

            _beaconToken = Messenger.Subscribe<BeaconChangeProximityMessage>(OnBeaconIsNear);
        }

        private async void Setup()
        {
            Loading = true;

            try
            {
                Beacons = await Mvx.Resolve<IWebService>().LoadBeacons();
                if (Beacons.IsNullOrEmpty())
                    return;
                
                _beaconService.Start(Beacons);
            }
            catch (Exception ex)
            {
                UserInteractions.Alert(ex.Message ?? "Не удалось найти ни одного маячка рядом, попробуйте в другом месте", title: "Ошибка");
                Loading = false;
            }
        }

        private void OnBeaconIsNear(BeaconChangeProximityMessage msg)
        {
            Loading = false;

            var beacon = Beacons.FirstOrDefault(x => x.UUID.ToLower() == msg.UUID.ToLower() && x.Major == msg.Major && x.Minor == msg.Minor);
            if (beacon != null)
            {
                _beaconService.Stop();
                Messenger.Unsubscribe<BeaconChangeProximityMessage>(_beaconToken);

                Mvx.Resolve<IMvxTrace>().Trace (MvxTraceLevel.Diagnostic, "Beacon", string.Format("Current Beacon {0} - {1} - {2}", beacon.UUID, beacon.Major, beacon.Minor));

                UserInteractions.Alert(
                    "Персональное предложение! Специально для Вас товар со скидкой!", 
                    () => ShowViewModel<ProductViewModel>(new { id = beacon.ID }),
                    "Уведомление");
            }
        }

        public override void Start()
        {
            base.Start();

            Setup();
        }
    }
}


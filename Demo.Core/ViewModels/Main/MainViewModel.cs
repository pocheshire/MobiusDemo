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
        /// <summary>
        /// Токен, который нужен для корректной работы подписки на <see cref="Demo.Core.Messages.BeaconFoundMessage"/>
        /// </summary>
        private MvvmCross.Plugins.Messenger.MvxSubscriptionToken _beaconToken;

        /// <summary>
        /// Список доступных маяков, загруженный с сервера
        /// </summary>
        /// <value>Список <see cref="Demo.API.Models.Beacons.BeaconRegionModel"/></value>
        internal List<BeaconModel> Beacons { get; private set; }

        /// <summary>
        /// Запускает загрузку доступных маячков и начинает поиск ближайших
        /// </summary>
        private async void StartLoadAndSearchBeacons()
        {
            //Отображаем индикатор загрузки
            Loading = true;

            try
            {
                //Получаем экземпляр сервиса IWebService и загружаем доступные маячки
                Beacons = await Mvx.Resolve<IWebService>().LoadBeacons();
                if (Beacons.IsNullOrEmpty())
                    return;

                //Подписываемся на сообщение о том, что рядом был обнаружен маячок
                _beaconToken = Messenger.Subscribe<BeaconFoundMessage>(OnBeaconIsNear);

                //Начинаем поиск маячков рядом с пользователем
                Mvx.Resolve<IBeaconService>().Start(Beacons);
            }
            catch (Exception ex)
            {
                //Что-то пошло не так – отображаем пользователю соответствующий диалог
                UserInteractions.Alert(ex.Message ?? "Не удалось найти ни одного маячка рядом, попробуйте в другом месте", title: "Ошибка");

                //Скрываем индикатор загрузки
                Loading = false;
            }
        }

        /// <summary>
        /// Вызывается, когда рядом находится маячок
        /// </summary>
        /// <param name="msg">Сообщение <see cref="Demo.Core.Messages.BeaconFoundMessage"/>.</param>
        private void OnBeaconIsNear(BeaconFoundMessage msg)
        {
            //Проверяем, что найденный маячок "наш", т.е. находится в списке загруженных с сервера
            var beacon = Beacons.FirstOrDefault(x => x.UUID.ToLower() == msg.UUID.ToLower() && x.Major == msg.Major && x.Minor == msg.Minor);
            if (beacon != null)
            {
                //Скрываем индикатор загрузки, ведь поиск уже закончен
                Loading = false;

                //Останавливаем поиск маячков
                Mvx.Resolve<IBeaconService>().Stop();

                //Отписываемся от подписки на сообщения BeaconFoundMessage
                Messenger.Unsubscribe<BeaconFoundMessage>(_beaconToken);

                //Пишем в output какой маячок мы нашли
                Mvx.Resolve<IMvxTrace>().Trace (MvxTraceLevel.Diagnostic, "Beacon", string.Format("Current Beacon {0} - {1} - {2}", beacon.UUID, beacon.Major, beacon.Minor));

                //Показываем пользователю диалог о том, что мы нашли для него интересное предложение
                UserInteractions.Alert(
                    "Персональное предложение! Специально для Вас товар со скидкой!", 
                    () => ShowViewModel<ProductViewModel>(new { id = beacon.ID }),
                    "Уведомление");
            }
        }

        /// <summary>
        /// Метод, который говорит нам о том, что наша ViewModel отобразилась на экране
        /// </summary>
        public override void Start()
        {
            base.Start();

            //Загружаем доступные маячки и запускаем поиск маяков рядом с пользователем
            StartLoadAndSearchBeacons();
        }
    }
}


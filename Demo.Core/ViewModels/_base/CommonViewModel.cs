using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Demo.Core.Services;

namespace Demo.Core.ViewModels
{
    public class CommonViewModel : MvxViewModel
    {
        private bool _loading;
        /// <summary>
        /// Позволяет отобразить или скрыть индикатор загрузки
        /// </summary>
        /// <value><c>true</c> если необходимо показать индикатор; иначе, <c>false</c>.</value>
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

        /// <summary>
        /// Возвращает сервис, который отвечает за взаимодействие с пользователем через диалоги
        /// </summary>
        /// <value>Сервис, реализующий интерфейс <see cref="Demo.Core.Services.IUserInteraction"/></value>
        protected static IUserInteraction UserInteractions
        {
            get { return Mvx.Resolve<IUserInteraction>(); }
        }

        /// <summary>
        /// Возвращает сервис, который дает возможность отправить или подписаться на опредленный вид сообщений
        /// </summary>
        /// <value>Сервис, реализующий интерфейс <see cref="MvvmCross.Plugins.Messenger.IMvxMessenger"/></value>
        protected static IMvxMessenger Messenger
        {
            get { return Mvx.Resolve<IMvxMessenger>(); }
        }
    }
}


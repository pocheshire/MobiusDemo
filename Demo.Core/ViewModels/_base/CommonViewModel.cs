using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Demo.Core.Services;

namespace Demo.Core.ViewModels
{
    public class CommonViewModel : MvxViewModel, ICommonViewModel
    {
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

        private readonly NavigationHint _hint = NavigationHint.Build();
        public NavigationHint Hint { get { return _hint; } }

        protected static IExtendedUserInteraction UserInteractions
        {
            get { return Mvx.Resolve<IExtendedUserInteraction>(); }
        }

        protected static IMvxMessenger Messenger
        {
            get { return Mvx.Resolve<IMvxMessenger>(); }
        }

        public CommonViewModel()
        {
            Loading = false;
        }

        public virtual void Unbind()
        {
            
        }
    }
}


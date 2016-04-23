using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform.Platform;
using UIKit;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platform;
using Demo.iOS.Services;
using Demo.Core.Services;
using Demo.iOS.BindingControllers;

namespace Demo.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }
        
        public Setup(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
        
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DemoDebugTrace();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<UITextField>("PhoneBinding", view => new PhoneTextBindingController(view));

            base.FillTargetFactories(registry);
        }

        protected override void InitializeLastChance ()
        {
            base.InitializeLastChance();

            Mvx.RegisterSingleton<IUserInteraction> (new UserInteraction ());
            Mvx.RegisterSingleton<IBeaconService> (new BeaconService ());
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.Content;
using Android.Widget;
using Demo.Android.Bindings;
using Demo.Android.Services;
using Demo.Core.Services;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using Widget = Android.Support.V4.Widget;
using WidgetV7 = Android.Support.V7.Widget;

namespace Demo.Android
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
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
            registry.RegisterCustomBindingFactory<EditText>("PhoneBinding", view => new PhoneTextBindingController(view));

            base.FillTargetFactories(registry);
        }

        protected override void InitializeLastChance ()
        {
            base.InitializeLastChance();

            Mvx.RegisterSingleton<IUserInteraction> (new UserInteraction ());
            Mvx.RegisterSingleton<IBeaconService> (new BeaconService ());
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies
        {
            get
            {
                var assemblies = base.AndroidViewAssemblies.ToList();
                assemblies.Add(typeof(WidgetV7.Toolbar).Assembly);
                assemblies.Add(typeof(Widget.DrawerLayout).Assembly);
                return assemblies;
            }
        }
    }
}

using MvvmCross.Platform.IoC;
using Demo.Core.ViewModels.Main;
using MvvmCross.Platform;
using Demo.API.Services;
using Demo.API.Services.Implementations;

namespace Demo.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            Mvx.RegisterSingleton<IWebService>(() => new WebService());
            Mvx.RegisterSingleton<IYaMoneyService>(() => new YaMoneyService());

            RegisterAppStart<MainViewModel>();
        }
    }
}

using System;
using System.Linq;
using UIKit;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.iOS.Views;
using MvvmCross.Platform;
using MvvmCross.Core.ViewModels;
using Demo.Core.ViewModels;

namespace Demo.iOS.Navigation
{
    public class CustomNavigationPresenter : MvxIosViewPresenter
    {
        public CustomNavigationPresenter(UIApplicationDelegate applicationDelegate, UIWindow window) 
            : base(applicationDelegate, window)
        {
            
        }

        public override void Show(IMvxIosView view)
        {
            var viewController = view as UIViewController;
			var viewModel =  (ICommonViewModel)(view.ViewModel ?? Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(view.Request, null));
			view.ViewModel = viewModel;

            if (MasterNavigationController == null)
            {
                base.Show(view);

                return;
            }

            if (viewModel.Hint.NavigationType == NavigationType.ClearAndPush)
                PushRootController(viewController);
            else if (viewModel.Hint.NavigationType == NavigationType.Push)
                MasterNavigationController.PushViewController(viewController, true);
            else if (viewModel.Hint.NavigationType == NavigationType.PresentModal)
                MasterNavigationController.PresentViewControllerAsync(viewController, true);
        }

        public void PushRootController(UIViewController newRootViewController)
        {
            var topNavigationController = MasterNavigationController;
            var oldViewController = topNavigationController.ChildViewControllers [0];

            topNavigationController.PopToRootViewController(false);

            if (newRootViewController.GetType() != oldViewController.GetType())
            {
                ((oldViewController as IMvxIosView).ViewModel as ICommonViewModel).Unbind();
                oldViewController.RemoveFromParentViewController();

                topNavigationController.NavigationBar.TopItem.Title = newRootViewController.Title ?? string.Empty;
                topNavigationController.PushViewController(newRootViewController, false);
            }
        }

        public override void Close(IMvxViewModel toClose)
        {
            var viewModel = (ICommonViewModel)toClose;
            if (viewModel != null)
            {
                if (viewModel.Hint.NavigationType == NavigationType.Push)
                    MasterNavigationController.PopViewController(true);
                if (viewModel.Hint.NavigationType == NavigationType.PresentModal)
                    MasterNavigationController.DismissViewControllerAsync(true);

                viewModel.Unbind();
                return;
            }
        }
    }
}


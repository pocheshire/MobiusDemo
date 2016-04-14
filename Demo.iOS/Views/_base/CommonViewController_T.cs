using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using Demo.Core.ViewModels;
using System.Collections.Generic;

namespace Demo.iOS.Views
{
    public abstract class CommonViewController<T> : MvxViewController<T>
        where T : class, IMvxViewModel
	{
        private NSObject _keyboardObserverWillShow;
        private NSObject _keyboardObserverWillHide;

        public bool RegisterKeyboardActions { get; protected set; }
        public float RegisterKeyboardHeight { get; protected set; }

        protected UIActivityIndicatorView _loading { get; set; }

        protected CommonViewController ()
        {
            
        }

        protected CommonViewController (IntPtr handle)
            : base (handle)
        {
            
        }

        protected CommonViewController (string nibName, Foundation.NSBundle bundle)
            : base (nibName, bundle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.CreateBinding(_loading).For("Visibility").To("Loading").WithConversion("Visibility").Apply();

            if (ViewModel != null)
                BindControlls();
        }

        protected abstract void BindControlls();

        public override void LoadView()
        {
            base.LoadView();

            this.EdgesForExtendedLayout = UIRectEdge.None;

            View.BackgroundColor = UIColor.White;

            _loading = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            _loading.HidesWhenStopped = false;
            _loading.Color = UIColor.Black;
            _loading.Frame = new CGRect((UIScreen.MainScreen.Bounds.Width - _loading.Frame.Width) / 2, (UIScreen.MainScreen.Bounds.Height - _loading.Frame.Height) / 2, _loading.Frame.Width, _loading.Frame.Height);
            _loading.StartAnimating();

            InitializeControlls();

            View.AddSubview(_loading);

            if (RegisterKeyboardActions)
            {
                RegisterHideKeyboardOnSwipe ();
                RegisterForKeyboardNotifications ();
            }
        }

        protected virtual void InitializeControlls ()
        {
            
        }

        private void RegisterHideKeyboardOnSwipe()
        {
            var scrollView = View.Subviews.OfType<UIScrollView>().FirstOrDefault();
            if (scrollView == null)
                return;

            scrollView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.Interactive;
        }

        private void RegisterForKeyboardNotifications ()
        {
            _keyboardObserverWillShow = NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillShowNotification, KeyboardWillShowNotification);
            _keyboardObserverWillHide = NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillHideNotification, KeyboardWillHideNotification);
        }

        private float _keyboardHeight;
        protected virtual void KeyboardWillShowNotification (NSNotification notification)
        {
            var activeView = View.FindFirstResponder ();

            if (activeView == null)
                return;

            var scrollView = View.Subviews.OfType<UIScrollView> ().FirstOrDefault ();
            if (scrollView == null)
                return;

            var keyboardFrame = notification.UserInfo == null ? new CGRect (0, 0, 0, 0) : ((NSValue)notification.UserInfo.ObjectForKey(UIKeyboard.BoundsUserInfoKey)).CGRectValue;

            if (activeView is UITextView)
            {
                _keyboardHeight = RegisterKeyboardHeight == 0f ? (float)keyboardFrame.Height : RegisterKeyboardHeight;

                scrollView.ContentInset = new UIEdgeInsets(0, 0, _keyboardHeight, 0);
                scrollView.ScrollRectToVisible(
                    new CGRect(0, activeView.Frame.Top, activeView.Frame.Width, activeView.Frame.Height),
                    true);
            }
            else
            {
                _keyboardHeight = RegisterKeyboardHeight == 0f ? (float)keyboardFrame.Height : RegisterKeyboardHeight;
                scrollView.ContentInset = new UIEdgeInsets(0, 0, _keyboardHeight, 0);
            }
        }

        protected virtual void KeyboardWillHideNotification(NSNotification notification)
        {
            var activeView = View.FindFirstResponder();
            if (activeView == null)
                return;

            var scrollView = View.Subviews.OfType<UIScrollView>().FirstOrDefault();
            if (scrollView == null)
                return;

            scrollView.ContentInset = new UIEdgeInsets(0, 0, 0, 0);
            scrollView.ScrollIndicatorInsets = new UIEdgeInsets(0, 0, 0, 0);
        }

        private void UnregisterKeyboardNotifications()
        {
            if (_keyboardObserverWillShow != null)
                NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardObserverWillShow);
            if (_keyboardObserverWillHide != null)
                NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardObserverWillHide);
            
            _keyboardObserverWillShow = null;
            _keyboardObserverWillHide = null;
        }

        #region IUnbindable implementation

        protected virtual void CleanUp()
        {

        }

        public void Unbind()
        {
            UnregisterKeyboardNotifications();

            CleanUp();

            var viewModel = ViewModel as ICommonViewModel;
            if (viewModel != null)
                viewModel.Unbind();
        }

        #endregion
	}

}
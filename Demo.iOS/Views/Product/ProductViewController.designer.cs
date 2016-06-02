// WARNING
//
// This file has been generated automatically by Xamarin Studio Business to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Demo.iOS.Views.Product
{
	[Register ("ProductViewController")]
	partial class ProductViewController
	{
		[Outlet]
		UIKit.UIButton _basketButton { get; set; }

		[Outlet]
		UIKit.UILabel _price { get; set; }

		[Outlet]
		UIKit.UILabel _productName { get; set; }

		[Outlet]
		UIKit.UILabel _shopName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_shopName != null) {
				_shopName.Dispose ();
				_shopName = null;
			}

			if (_productName != null) {
				_productName.Dispose ();
				_productName = null;
			}

			if (_price != null) {
				_price.Dispose ();
				_price = null;
			}

			if (_basketButton != null) {
				_basketButton.Dispose ();
				_basketButton = null;
			}
		}
	}
}

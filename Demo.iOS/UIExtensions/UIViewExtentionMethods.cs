using System;
using CoreGraphics;
using UIKit;
using Foundation;
using System.Drawing;
using System.Collections.Generic;

namespace System
{
	public static class UIViewExtentionMethods
	{
		public static UIView FindFirstResponder (this UIView view)
		{
			if (view.IsFirstResponder)
				return view;
			foreach (UIView subView in view.Subviews) {
				var firstResponder = subView.FindFirstResponder();
				if (firstResponder != null)
					return firstResponder;
			}
			return null;
		}

        public static UILabel SetTitle (this UILabel label)
        {
            label.Lines = 1;
            label.BackgroundColor = UIColor.Clear;
            label.TextColor = UIColor.Black;
            label.Font = UIFont.BoldSystemFontOfSize (16f);
            label.TextAlignment = UITextAlignment.Center;
            return label;
        }

        public static UITextField SetTextField (this UITextField textField, UIKeyboardType keyboardType, string placeholder)
        {
            textField.Layer.BorderColor = UIColor.FromRGB (182, 182, 175).CGColor;
            textField.Layer.BorderWidth = 1f;
            textField.BackgroundColor = UIColor.White; 
            textField.TextColor = UIColor.Black; 
            textField.Layer.CornerRadius = 2f;
            textField.KeyboardType = keyboardType;
            textField.Layer.MasksToBounds = false;
            textField.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            textField.Layer.MasksToBounds = false;
            textField.LeftView = new UIView (new RectangleF (0f, 0f, 10f, 20f));
            textField.LeftViewMode = UITextFieldViewMode.Always;
            textField.Placeholder = placeholder;
            textField.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            textField.ClearButtonMode = UITextFieldViewMode.Always;

            return textField;
        }
	}
}


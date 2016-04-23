using System;
using System.Text;
using Foundation;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using UIKit;

namespace Demo.iOS.BindingControllers
{
    public class PhoneTextBindingController: MvxTargetBinding
    {
        const string _filter = @"+7 (###) ###-##-##";

        protected UITextField TextField
        {
            get { return (UITextField) Target; }
        }

        public PhoneTextBindingController (UITextField target)
            : base (target)
        {
        }

        public override void SubscribeToEvents()
        {
            TextField.Text = "+7 (";
            TextField.ShouldChangeCharacters = ShouldChangePhoneNumber;
        }

        public override void SetValue (object value)
        {
            var str = (string)value;
            if (str.IsNullOrEmpty())
                return;
            
            var formatedValue = FilteredPhoneString(str, _filter);
            TextField.Text = formatedValue;
        }

        public override Type TargetType
        {
            get { return typeof(string); }
        }

        public override MvxBindingMode DefaultMode
        {
            get { return MvxBindingMode.TwoWay; }
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                var target = Target as UITextField;
                if (target != null)
                {
                    target.Dispose ();
                }
            }
            base.Dispose(isDisposing);
        }

        #region Phone number formatting methods

        private bool ShouldChangePhoneNumber(UITextField textField, NSRange range, string replacementString)
        {
            var changedString = (range.Location > 0 ? textField.Text.Substring (0, (int)range.Location) : "")
                + replacementString
                + (textField.Text.Substring (
                    (int)(range.Location + range.Length),
                    (int)(textField.Text.Length - range.Location - range.Length)));

            if(range.Length == 1 && replacementString.Length == 0 &&
                !char.IsDigit(textField.Text[(int)range.Location]))
            {
                // Something was deleted.  Delete past the previous number
                int location = changedString.Length-1;
                if(location > 0)
                {
                    for(; location > 0; location--)
                    {
                        if(char.IsDigit(changedString[location]))
                            break;
                    }
                    changedString = changedString.Substring(0, location);
                }
            }

            textField.Text = FilteredPhoneString(changedString, _filter);

            FireValueChanged (textField.Text);

            return false;
        }

        private string FilteredPhoneString(string str, string filter)
        {
            int onOriginal = 0, onFilter = 0, onOutput = 0;
            var outputString = new StringBuilder();
            bool done = false;

            while(onFilter < filter.Length && !done)
            {
                var filterChar = filter[onFilter];
                var originalChar = onOriginal >= str.Length ? (char)0 : str[onOriginal];
                switch (filterChar) 
                {
                    case '#':
                        if(originalChar == 0)
                        {
                            done = true;
                            break;
                        }
                        if(char.IsDigit(originalChar))
                        {
                            outputString.Append(originalChar);
                            onOriginal++;
                            onFilter++;
                            onOutput++;
                        }
                        else
                        {
                            onOriginal++;
                        }
                        break;
                    default:
                        // Any other character will automatically be inserted for the user as they type (spaces, - etc..) or deleted as they delete if there are more numbers to come.
                        outputString.Append(filterChar);
                        onOutput++;
                        onFilter++;
                        if(originalChar == filterChar)
                            onOriginal++;
                        break;
                }
            }
            return outputString.ToString();     
        }

        #endregion
    }
}


using System;

namespace System
{
    public static class StringExtentions
    {
        public static string ToPriceString (this decimal price)
        {
            return string.Format ("{0} руб.", (price.ToString ("### ### ####")).Trim ());
        }
    }
}


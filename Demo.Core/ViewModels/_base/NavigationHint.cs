using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.ViewModels
{
    public class NavigationHint
    {
        public NavigationHint()
        { 
        }

        public static NavigationHint Build(NavigationType type = NavigationType.Push)
        {
            return new NavigationHint()
            {
                NavigationType = type
            };
        }

        public NavigationType NavigationType { get; set; }
    }
}

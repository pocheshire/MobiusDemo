using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Core.ViewModels
{
    public enum NavigationType
    {
        Push = 0,
        ClearAndPush = 1,
        PresentModal = 2,
        DoublePush = 3,
        Tab = 4
    }
}

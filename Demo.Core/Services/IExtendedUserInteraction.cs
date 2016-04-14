using System;
using System.Threading.Tasks;

namespace Demo.Core.Services
{
    public interface IExtendedUserInteraction
    {
        void Alert(string message, Action done = null, string title = "", string okButton = "OK");
    }
}


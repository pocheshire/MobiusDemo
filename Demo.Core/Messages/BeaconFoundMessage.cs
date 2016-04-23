using System;
using MvvmCross.Plugins.Messenger;
using Demo.API.Models.Beacons;

namespace Demo.Core.Messages
{
    /// <summary>
    /// Сообщение о том, что рядом был найден маячок
    /// </summary>
    public class BeaconFoundMessage : MvxMessage
    {
        public string UUID { get; private set; }

        public ushort Major { get; private set; }

        public ushort Minor { get; private set; }

        public BeaconFoundMessage(object sender, string uuid, ushort major, ushort minor)
            : base (sender)
        {
            UUID = uuid;
            Major = major;
            Minor = minor;
        }
    }
}


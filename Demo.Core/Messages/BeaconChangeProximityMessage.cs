using System;
using MvvmCross.Plugins.Messenger;
using Demo.API.Models.Beacons;

namespace Demo.Core.Messages
{
    public class BeaconChangeProximityMessage : MvxMessage
    {
        public string UUID { get; private set; }

        public ushort Major { get; private set; }

        public ushort Minor { get; private set; }

        public BeaconChangeProximityMessage(object sender, string uuid, ushort major, ushort minor)
            : base (sender)
        {
            UUID = uuid;
            Major = major;
            Minor = minor;
        }
    }
}


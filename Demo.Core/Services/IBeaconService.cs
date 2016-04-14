using System;
using System.Collections.Generic;
using Demo.API.Models.Beacons;

namespace Demo.Core.Services
{
    public interface IBeaconService
    {
        void Start (List<BeaconRegionModel> beacons);
        void Stop ();
    }
}


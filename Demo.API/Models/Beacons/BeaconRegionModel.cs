using System.Threading.Tasks;
using System.Collections.Generic;

namespace Demo.API.Models.Beacons
{
	public sealed class BeaconRegionModel
	{
		#region Properties

		public int ID { get; set; }

		public string UUID { get; set; }

        public ushort Major { get; set; }

        public ushort Minor { get; set; }

		#endregion
	}
}


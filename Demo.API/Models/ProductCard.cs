using System.Threading.Tasks;
using System.Collections.Generic;

namespace Demo.API.Models
{
	public sealed class ProductCard
	{
		#region Properties

		public int ID { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public decimal? OldPrice { get; set; }

		public string ShopName { get; set; }

		public string YaMoney { get; set; }

		public List<int> Images { get; set; }

		public bool HasImages { get { return Images != null && Images.Count > 0; } }

		#endregion

		#region Methods

		public static string GetImageUrl (int id, int width, int height)
		{
			return string.Format ("http://tapki.azurewebsites.net/image/{0}/{1}/{2}", id, width, height);
		}

		#endregion

	}
}


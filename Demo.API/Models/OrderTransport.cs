using System.Threading.Tasks;

namespace Demo.API.Models
{
	public sealed class OrderTransport
	{
		public int ProductID { get; set; }

		public string UserName { get; set; }

		public string UserPhone { get; set; }
	}
}


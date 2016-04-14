using System.Threading.Tasks;

namespace Demo.API.Models
{
	public enum Status
	{
		Success = 1,
		Fail = 2
	}

	public class Order
	{
		public int ID { get; set; }

		public Status Status { get; set; }

		public int PinCode { get; set; }
	}
}


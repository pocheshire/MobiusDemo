
namespace Demo.API.Models.Yandex
{
	public class YaMoneyBase
	{
		public string Status { get; set; }

		public string Error { get; set; }

		public bool HasError { get { return !string.IsNullOrEmpty (Error); } }
	}
}


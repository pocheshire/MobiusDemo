using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Demo.API.Models.Yandex
{
	public sealed class YaMoneyInstance : YaMoneyBase
	{
		public const string Key = "YaMoneyInstance";

		[JsonProperty ("instance_id")]
		public string InstanceID { get; set; }
	}
}


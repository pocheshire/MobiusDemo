using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Demo.API.Models.Yandex
{
	public sealed class YaMoneyProcess : YaMoneyBase
	{
		[JsonProperty ("acs_uri")]
		public string ASCUri { get; set; }

		[JsonProperty ("acs_params")]
		public Dictionary<string, string> YaMoneyACSParams { get; set; }
	}
}


using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;

namespace Demo.API.Models.Yandex
{
    public sealed class YaMoneyRequest : YaMoneyBase
    {
        [JsonProperty("request_id")]
        public string RequestID { get; set; }

        [JsonProperty("contract_amount")]
        public decimal ContractAmount { get; set; }

        public string Title { get; set; }
    }
}


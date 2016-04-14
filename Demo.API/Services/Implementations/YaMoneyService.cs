using System;
using System.Threading.Tasks;
using Demo.API.Models.Yandex;
using System.Net.Http;
using System.Globalization;

namespace Demo.API.Services.Implementations
{
    public class YaMoneyService : IYaMoneyService
    {
        protected virtual async Task<T> PostRequest<T>(string url, HttpContent data, string errorMessage)
            where T : YaMoneyBase
        {
            var result = await ConnectionService.Post<T> (url, data, errorMessage);

            if (result.HasError)
                throw new Exception(result.Error);
            else
                return result;
        }

        #region IYaMoneyService implementation

        public async Task<YaMoneyInstance> LoadMoneyInstance()
        {
            var url = @"https://money.yandex.ru/api/instance-id";
            var content = new StringContent("client_id=FA6EB0A21F25A7D51527AE459AC21DC13176275E4874A531BF3E890E82198C86", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            return await PostRequest<YaMoneyInstance>(url, content, "Не удалось загрузить данные");
        }

        public async Task<YaMoneyProcess> LoadMoneyProcess(string requestId, string instanceId)
        {
            var url = @"https://money.yandex.ru/api/process-external-payment";
            var content = new StringContent(
                string.Format ("request_id={0}&instance_id={1}&ext_auth_success_uri={2}&ext_auth_fail_uri={3}", requestId, instanceId, AppData.YaSuccessUri, AppData.YaFailUri),
                System.Text.Encoding.UTF8,
                "application/x-www-form-urlencoded"
            );

            return await PostRequest<YaMoneyProcess>(url, content, "Не удалось загрузить данные");
        }

        public async Task<YaMoneyRequest> LoadMoneyRequest(string instanceId, string billTo, decimal amount)
        {
            var url = @"https://money.yandex.ru/api/request-external-payment";
            var content = new StringContent(
                string.Format("pattern_id=p2p&instance_id={0}&to={1}&amount={2}", instanceId, billTo, (amount.ToString("#########.##", CultureInfo.InvariantCulture)).Trim()),
                System.Text.Encoding.UTF8,
                "application/x-www-form-urlencoded"
            );

            return await PostRequest<YaMoneyRequest>(url, content, "Не удалось загрузить данные");
        }

        #endregion
    }
}


using System;
using System.Threading.Tasks;
using Demo.API.Models.Yandex;

namespace Demo.API.Services
{
    public interface IYaMoneyService
    {
        Task<YaMoneyInstance> LoadMoneyInstance();
        Task<YaMoneyProcess> LoadMoneyProcess(string requestId, string instanceId);
        Task<YaMoneyRequest> LoadMoneyRequest(string instanceId, string billTo, decimal amount);
    }
}


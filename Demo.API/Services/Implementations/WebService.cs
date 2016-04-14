using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Demo.API.Models;
using Demo.API.Models.Beacons;
using Newtonsoft.Json;

namespace Demo.API.Services.Implementations
{
    public class WebService : IWebService
    {
        #region IWebService implementation
        public async Task<List<BeaconRegionModel>> LoadBeacons()
        {
            var url = string.Format("{0}beacon", AppData.Host);

            return await ConnectionService.Get<List<BeaconRegionModel>>(url, "Не удалось загрузить список маячков");
        }

        public async Task<Order> SendOrderWithStatus(Order order)
        {
            var url = string.Format("{0}orderstatus", AppData.Host);
            var data = JsonConvert.SerializeObject(order);

            return await ConnectionService.Post<Order>(url, new StringContent(data, Encoding.UTF8, "application/json"), "Не удалось получить статус заказа");
        }

        public async Task<Order> SendOrderTransport(OrderTransport orderTransport)
        {
            var url = string.Format("{0}order/post", AppData.Host);
            var data = JsonConvert.SerializeObject(orderTransport);

            return await ConnectionService.Post<Order>(url, new StringContent(data, Encoding.UTF8, "application/json"), "Не удалось загрузить данные о заказе");
        }

        public async Task<ProductCard> LoadProductCard(int beaconId)
        {
            var url = string.Format("{0}beacon/{1}", AppData.Host, beaconId);

            return await ConnectionService.Get<ProductCard>(url, "Не удалось загрузить данные о заказе");
        }
        #endregion
        
    }
}


using System;
using System.Threading.Tasks;
using Demo.API.Models.Beacons;
using System.Collections.Generic;
using Demo.API.Models;

namespace Demo.API.Services
{
    public interface IWebService
    {
        Task<List<BeaconModel>> LoadBeacons();

        Task<Order> SendOrderWithStatus(Order order);
        Task<Order> SendOrderTransport(OrderTransport orderTransport);
        Task<ProductCard> LoadProductCard(int beaconId);
    }
}


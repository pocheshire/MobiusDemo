using System;
using Demo.API.Models;
using Newtonsoft.Json;

namespace Demo.Core.ViewModels.Thanks
{
    public class ThanksViewModel : CommonViewModel
    {
        internal ProductCard ProductCard  { get; private set; }
        internal Order Order  { get; private set; }

        #region Properties

        private string _shopName;
        public string ShopName
        {
            get
            {
                return _shopName;
            }
            set
            {
                _shopName = value;
                RaisePropertyChanged(() => ShopName);
            }
        }

        private string _productName;
        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                _productName = value;
                RaisePropertyChanged(() => ProductName);
            }
        }

        private string _price;
        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                RaisePropertyChanged(() => Price);
            }
        }

        private string _pinCode;
        public string PinCode
        {
            get
            {
                return _pinCode;
            }
            set
            {
                _pinCode = value;
                RaisePropertyChanged(() => PinCode);
            }
        }

        #endregion

        #region Constructor

        public ThanksViewModel()
        {
            Hint.NavigationType = NavigationType.PresentModal;
        }

        #endregion

        #region Methods

        public void Init (string product, string order)
        {
            ProductCard = JsonConvert.DeserializeObject<ProductCard>(product);
            Order = JsonConvert.DeserializeObject<Order>(order);

            ShopName = ProductCard.ShopName;
            ProductName = ProductCard.Name;
            Price = string.Format ("Сумма: {0}", ProductCard.Price.ToPriceString ());
            PinCode = string.Format("Для получения товара предъявите код покупки на кассе: {0}", Order.PinCode);
        }

        #endregion
    }
}


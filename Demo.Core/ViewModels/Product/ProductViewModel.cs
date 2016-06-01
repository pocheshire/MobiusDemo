using System;
using Demo.API.Models;
using MvvmCross.Platform;
using Demo.API.Services;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;
using Demo.Core.ViewModels.Basket;

namespace Demo.Core.ViewModels.Product
{
    public class ProductViewModel : CommonViewModel
    {
        internal ProductCard Bundle { get; private set; }

        #region Command

        private ICommand _basketCommand;
        public ICommand BasketCommand
        {
            get
            { 
                return _basketCommand ?? (_basketCommand = new MvxCommand(() => ShowViewModel<BasketViewModel>(new { product = JsonConvert.SerializeObject(Bundle) })));
            }
        }

        #endregion

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

        private string _oldPrice;
        public string OldPrice
        {
            get
            {
                return _oldPrice;
            }
            set
            {
                _oldPrice = value;
                RaisePropertyChanged(() => OldPrice);
            }
        }

        #endregion

        #region Methods

        private async Task LoadContent(int id)
        {
            Loading = true;

            try
            {
                Bundle = await Mvx.Resolve<IWebService>().LoadProductCard(id);
                if (Bundle != null)
                {
                    ShopName = Bundle.ShopName;
                    ProductName = Bundle.Name;

                    Price = Bundle.Price.ToPriceString();

                    if (Bundle.OldPrice != null && Bundle.OldPrice.HasValue)
                        OldPrice = Bundle.OldPrice.Value.ToPriceString();
                }
                else
                    UserInteractions.Alert("Не удалось загрузить подробную информацию", title: "Ошибка");
            }
            catch (Exception ex)
            {
                UserInteractions.Alert(ex.Message ?? "Не удалось загрузить подробную информацию", title: "Ошибка");
            }
            finally 
            {
                Loading = false;
            }
        }

        public void Init (int id)
        {
            LoadContent(id);
        }

        #endregion
    }
}


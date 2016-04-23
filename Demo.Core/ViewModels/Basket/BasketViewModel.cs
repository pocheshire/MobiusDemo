using System;
using Demo.API.Models;
using Newtonsoft.Json;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Demo.API.Services;
using Demo.Core.ViewModels.Pay;

namespace Demo.Core.ViewModels.Basket
{
    public class BasketViewModel : CommonViewModel
    {
        internal ProductCard Bundle { get; private set; }

        #region Command

        private ICommand _payCommand;
        public ICommand PayCommand
        {
            get
            { 
                return _payCommand ?? (_payCommand = new MvxCommand(async () =>
                    {
                        if (UserName.IsNullOrEmpty())
                        {
                            UserInteractions.Alert ("Введите Ваше имя", title: "Уведомление");
                            return;
                        }

                        if (UserPhone.IsNullOrEmpty() || UserPhone.Length <= 4)
                        {
                            UserInteractions.Alert ("Введите Ваш номер телефона", title: "Уведомление");
                            return;
                        }

                        Loading = true;
                        try 
                        {
                            var order = await Mvx.Resolve<IWebService>().SendOrderTransport(new OrderTransport {
                                ProductID = Bundle.ID,
                                UserName = UserName,
                                UserPhone = UserPhone
                            });

                            if (order != null && order.ID > 0)
                                ShowViewModel<PayViewModel>(new { product = JsonConvert.SerializeObject(Bundle), order = JsonConvert.SerializeObject(order) });
                            else
                                UserInteractions.Alert ("Невозможно обработать заказ", title: "Ошибка");
                        }
                        catch (Exception ex)
                        {
                            UserInteractions.Alert (ex.Message ?? "Невозможно обработать заказ", title: "Ошибка");
                        }
                        finally
                        {
                            Loading = false;
                        }
                    }));
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

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private string _userPhone;
        public string UserPhone
        {
            get
            {
                return _userPhone;
            }
            set
            {
                _userPhone = value;
                RaisePropertyChanged(() => UserPhone);
            }
        }

        #endregion

        #region Methods

        public void Init (string product)
        {
            Bundle = JsonConvert.DeserializeObject<ProductCard>(product);

            ShopName = Bundle.ShopName;
            ProductName = Bundle.Name;
            Price = Bundle.Price.ToPriceString();
            OldPrice = Bundle.OldPrice.HasValue ? Bundle.OldPrice.Value.ToPriceString() : "";
        }

        #endregion
    }
}


using System;
using Demo.API.Models;
using Newtonsoft.Json;
using Demo.API.Models.Yandex;
using MvvmCross.Platform;
using Demo.API.Services;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.ViewModels.Thanks;

namespace Demo.Core.ViewModels.Pay
{
    public class PayViewModel : CommonViewModel
    {
        private const string pageFormat =
            @"
                <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
                <html xmlns=""http://www.w3.org/1999/xhtml"">
                <head>
                    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
                    <title>Происходит перенаправление в банк. Ожидайте.</title>
                </head>
                <body>
                    <form name=""transForm"" method=""post"" action=""{0}"">
                            Происходит перенаправление в банк. Ожидайте.
                            {1}
                    </form>
                </body>
                <script type=""text/javascript"">
                    var theForm = document.forms[""transForm""]; if (!theForm) document.orderForm; theForm.submit();
                </script>
                </html>";
        
        internal ProductCard ProductCard  { get; private set; }
        internal Order Order  { get; private set; }

        #region Properties

        private string _htmlText;
        public string HtmlText
        {
            get
            {
                return _htmlText;
            }
            set
            {
                _htmlText = value;
                RaisePropertyChanged(() => HtmlText);
            }
        }

        #endregion

        #region Methods

        private async void LoadContent()
        {
            Loading = true;
            try
            {
                var instance = new YaMoneyInstance{ InstanceID = "Raqlr8fgDFqlICSixvkEk1g4wgjUhaQCDXqSHAcM6ImWEWwzjpsGC4sp9dpR37Xq" };

                var request = await Mvx.Resolve<IYaMoneyService>().LoadMoneyRequest (instance.InstanceID, ProductCard.YaMoney, ProductCard.Price);

                var process = await Mvx.Resolve<IYaMoneyService>().LoadMoneyProcess (request.RequestID, instance.InstanceID);

                var hiddenParams = new StringBuilder ();
                foreach (var acsParam in process.YaMoneyACSParams)
                    hiddenParams.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", acsParam.Key, acsParam.Value);

                HtmlText = string.Format (pageFormat, process.ASCUri, hiddenParams);

            }
            catch (Exception ex)
            {
                UserInteractions.Alert(ex.Message ?? "Не удалось провести платеж", title: "Ошибка");
            }
            finally
            {
                Loading = false;
            }
        }

        public void Init (string product, string order)
        {
            ProductCard = JsonConvert.DeserializeObject<ProductCard>(product);
            Order = JsonConvert.DeserializeObject<Order>(order);
        }

        public override void Start()
        {
            base.Start();

            LoadContent();
        }

        public async void PaymentSucceeded()
        {
            Order.Status = Status.Success;

            try
            {
                var order = await Mvx.Resolve<IWebService>().SendOrderWithStatus(Order);
                ShowViewModel<ThanksViewModel>(new { product = JsonConvert.SerializeObject(ProductCard), order = JsonConvert.SerializeObject(order) });
            }
            catch (Exception ex)
            {
                UserInteractions.Alert(ex.Message, title: "Ошибка");
            }
        }

        public async void PaymentFailed()
        {
            Order.Status = Status.Fail;

            try
            {
                var order = await Mvx.Resolve<IWebService>().SendOrderWithStatus(Order);
                UserInteractions.Alert("Не удалось совершить платеж", title: "Ошибка");
            }
            catch (Exception ex)
            {
                UserInteractions.Alert(ex.Message, title: "Ошибка");
            }
        }

        #endregion
    }
}


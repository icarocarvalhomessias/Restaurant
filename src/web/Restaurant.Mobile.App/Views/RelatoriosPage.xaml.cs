using Restaurant.Mobile.App.Services;
using Restaurant.Mobile.App.ViewModels;

namespace Restaurant.Mobile.App.Views
{
    public partial class RelatoriosPage : ContentPage
    {
        private readonly IOrderService _orderService;

        public RelatoriosPage(IOrderService orderService)
        {
            _orderService = orderService;
            InitializeComponent();
            BindingContext = new RelatoriosViewModel(_orderService);
        }
    }
}

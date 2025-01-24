using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restaurant.Mobile.App.Models;
using Restaurant.Mobile.App.Services;

namespace Restaurant.Mobile.App.ViewModels
{
    public class RelatoriosViewModel : BaseViewModel
    {
        private readonly IOrderService _orderService;

        public RelatoriosViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            LoadOrdersCommand = new AsyncRelayCommand(LoadOrdersAsync);
            LoadOrdersAsync().ConfigureAwait(false);
        }

        public ObservableCollection<OrderModel> Orders { get; } = new ObservableCollection<OrderModel>();

        public ICommand LoadOrdersCommand { get; }

        private async Task LoadOrdersAsync()
        {
            IsBusy = true;
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                Orders.Clear();
                foreach (var order in orders)
                {
                    Orders.Add(order);
                }
                Debug.WriteLine($"Loaded {Orders.Count} orders.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load orders: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}


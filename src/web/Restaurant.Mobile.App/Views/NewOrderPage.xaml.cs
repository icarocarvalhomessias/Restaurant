using Microsoft.Maui.Controls;
using Restaurant.Mobile.App.Models;
using Restaurant.Mobile.App.Models.Inputs;
using Restaurant.Mobile.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Mobile.App.Views
{
    public partial class NewOrderPage : ContentPage
    {
        private List<ProductModel> allProducts;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public NewOrderPage(IOrderService orderService, IProductService productService)
        {
            InitializeComponent();
            _orderService = orderService;
            _productService = productService;
            LoadProducts();
        }

        private async void LoadProducts()
        {
            // Load all products from the product service
            allProducts = (await _productService.GetAllProductsAsync()).ToList();
            ProductCollectionView.ItemsSource = allProducts;
        }

        private void OnProductSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue.ToLower();
            ProductCollectionView.ItemsSource = allProducts
                .Where(p => p.Name.ToLower().Contains(searchText))
                .ToList();
        }

        private async void OnSubmitOrderClicked(object sender, EventArgs e)
        {
            var selectedProducts = ProductCollectionView.SelectedItems.Cast<ProductModel>().ToList();
            var productIds = selectedProducts.Select(p => p.Id).ToList();

            var orderInput = new OrderInput
            {
                ClientName = ClientNameEntry.Text,
                ClientAddress = ClientAddressEntry.Text,
                ClientPhone = ClientPhoneEntry.Text,
                ProductIds = productIds
            };

            // Call the API to submit the order
            var success = await _orderService.AddOrderAsync(orderInput);

            if (success)
            {
                await DisplayAlert("Order Submitted", "Your order has been submitted successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Order Failed", "There was an error submitting your order. Please try again.", "OK");
            }
        }
    }
}

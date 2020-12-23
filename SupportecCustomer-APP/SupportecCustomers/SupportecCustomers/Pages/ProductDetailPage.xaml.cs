using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SupportecCustomers.Models;
using SupportecCustomers.Services;
using Xamarin.Forms;

namespace SupportecCustomers.Pages
{
    public partial class ProductDetailPage : ContentPage
    {
        public ObservableCollection<Item> Items;


        public ProductDetailPage(int id)
        {
            InitializeComponent();
            GetProductDetail(id);
        }

            public async Task GetProductDetail(int id)
        {
            ApiService apiService = new ApiService();
            var Product = await apiService.GetProduct(id);
            var Price = await apiService.GetProductPrice(id);

            PImage.Source = Product.PhotoUrl;
            PName.Text = Product.Name;
            PPrice.Text = Price.ToString() + " €";

            Items = new ObservableCollection<Item>();

            foreach (var item in Product.Items)
            {
                Items.Add(item);
            }

            LvItems.ItemsSource = Items;
            BusyIndicator.IsRunning = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SupportecCustomers.Models;
using SupportecCustomers.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SupportecCustomers.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        public ObservableCollection<Product> Products;
        private bool First = true;

        public ProductPage()
        {
            InitializeComponent();
            Products = new ObservableCollection<Product>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (First)
            {
                ApiService apiService = new ApiService();
                var id = apiService.GetUserId();
                var products = await apiService.GetAllProductsOfUser(id);

                foreach (var product in products)
                {
                    Products.Add(product);
                }
                LvProducts.ItemsSource = Products;

                BusyIndicator.IsRunning = false;
            }
            First = false;
        }

        private void LvProducts_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedProduct = e.SelectedItem as Product;
            if(selectedProduct != null)
            {
                Navigation.PushAsync(new ProductDetailPage(selectedProduct.Id));
            }
           ((ListView)sender).SelectedItem = null;
        }
    }
}

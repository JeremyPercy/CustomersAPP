using System;
using System.Collections.Generic;
using SupportecCustomers.Models;
using SupportecCustomers.Services;
using Xamarin.Forms;

namespace SupportecCustomers.Pages
{
    public partial class AccountPage : ContentPage
    {
        public User user;

        public AccountPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                ApiService apiService = new ApiService();

                user = await apiService.GetUser();

                if (user != null)
                {
                    PName.Text = user?.Name;
                    PLastName.Text = user?.LastName;
                    PPhone.Text = user?.Phone;
                    PEmail.Text = user?.Email;
                }

                if (user.Company != null)
                {
                    CName.Text = user?.Company.Name;
                    CAddress.Text = user?.Company.Address;
                    CZipcode.Text = user?.Company.Zipcode;
                    CCity.Text = user?.Company.City;
                    CEmail.Text = user?.Company.Email;
                    CBtw.Text = user?.Company.BtwCode;
                }
                BusyIndicator.IsRunning = false;
                AccountGrid.IsVisible = true;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.ToString(), "Ok");
            }

        }

        public void BtnEditProfile(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProfile(user));
        }

        public void BtnEditCompany(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditCompany(user.Company));
        }
    }
}

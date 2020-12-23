using System;
using System.Collections.Generic;
using SupportecCustomers.Services;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SupportecCustomers.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            LoginForm.IsVisible = false;
            BusyIndicator.IsRunning = true;
            ApiService apiService = new ApiService();
            var response = await apiService.GetToken(EntEmail.Text, EntPassword.Text);
            if (string.IsNullOrEmpty(response.Token))
            {
                await DisplayAlert("Error", "Something wrong", "Alright");
                LoginForm.IsVisible = true;
            } else
            {
                Preferences.Set("token", response.Token);
                Application.Current.MainPage = new MasterPage();
            }
        }

        private void TapSignUp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
    }
}

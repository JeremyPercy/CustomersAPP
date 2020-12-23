using System;
using System.Collections.Generic;
using SupportecCustomers.Services;
using Xamarin.Forms;

namespace SupportecCustomers.Pages
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        public async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            ApiService apiService = new ApiService();

            if (EntPassword.Text == EntConfirmPassword.Text)
            {
                bool response = await apiService.RegisterUser(EntEmail.Text, EntPassword.Text);
                if (!response)
                {
                    await DisplayAlert("Oops", "Something goes wrong", "Cancel");
                }
                else
                {
                    await DisplayAlert("Hi", "Your account has been created", "Alright");
                    await Navigation.PopToRootAsync();
                }
            } else
            {
                await DisplayAlert("Oops", "Passwords do not match", "Try again");
            }
        }
    }
}

using System;

using Xamarin.Forms;

namespace SupportecCustomers.Pages
{
    public class AccountPage : ContentPage
    {
        public AccountPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}


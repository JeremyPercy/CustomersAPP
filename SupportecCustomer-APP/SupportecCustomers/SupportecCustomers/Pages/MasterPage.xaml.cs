using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SupportecCustomers.Pages
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
        }

        private void TapHome_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
        }

        private void TapProducts_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new ProductPage());
            IsPresented = false;
        }

        private void TapEditAcount_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AccountPage());
            IsPresented = false;
        }

        private void TapLogout_Tapped(object sender, EventArgs e)
        {
            Preferences.Remove("token", string.Empty);
            Application.Current.MainPage = new NavigationPage(new LoginPage());

        }
    }
}

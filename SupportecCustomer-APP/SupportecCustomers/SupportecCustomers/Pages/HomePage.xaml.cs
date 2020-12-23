using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SupportecCustomers.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public void BtnCall_Clicked(object sender, EventArgs e)
        {
            PhoneDialer.Open("+31(0)13 207 39 89");
        }

        public void BtnEmail_Clicked(object sender, EventArgs e)
        {
            var message = new EmailMessage("", "", "info@supportec.nl");
            Email.ComposeAsync(message);
        }
    }
}

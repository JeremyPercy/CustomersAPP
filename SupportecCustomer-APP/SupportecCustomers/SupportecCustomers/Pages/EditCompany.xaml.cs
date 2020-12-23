using System;
using System.Collections.Generic;
using SupportecCustomers.Models;
using SupportecCustomers.Services;
using Xamarin.Forms;

namespace SupportecCustomers.Pages
{
    public partial class EditCompany : ContentPage
    {
        public Company _company;

        public EditCompany(Company company)
        {
            InitializeComponent();
            _company = company;

            if(_company != null)
            {
                EntName.Text = _company.Name;
                EntAddress.Text = _company.Address;
                EntZipcode.Text = _company.Zipcode;
                EntCity.Text = _company.City;
                EntEmail.Text = _company.Email;
                EntBtwCode.Text = _company.BtwCode;
            }
        }

        public async void BtnEditCompany(object sender, EventArgs e)
        {
            var company = new Company()
            {
                Name = EntName.Text,
                Address = EntAddress.Text,
                Zipcode = EntZipcode.Text,
                City = EntCity.Text,
                Email = EntEmail.Text,
                BtwCode = EntBtwCode.Text
            };

            try
            {
                ApiService apiService = new ApiService();
                await apiService.UpdateCompany(company);
                await DisplayAlert("Succeed", "Company information is updated", "Oke");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Oke");
            }


        }
    }
}

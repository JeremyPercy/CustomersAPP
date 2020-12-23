using System;
using System.Collections.Generic;
using SupportecCustomers.Models;
using SupportecCustomers.Services;
using Xamarin.Forms;

namespace SupportecCustomers.Pages
{
	public partial class EditProfile : ContentPage
	{
		public User _user;

		public EditProfile(User user)
		{
			InitializeComponent();
			_user = user;

			if (_user != null)
			{
				EntEmail.Text = _user?.Email;
				EntName.Text = _user?.Name;
				EntLastName.Text = _user?.LastName;
				EntPhone.Text = _user?.Phone;
			}
		}

		public async void BtnEditProfile(object sender, EventArgs e)
		{
            var user = new User()
            {
                Email = EntEmail.Text,
                Name = EntName.Text,
                LastName = EntLastName.Text,
                Phone = EntPhone.Text
			};

			try
			{
				ApiService apiService = new ApiService();
				await apiService.UpdateUser(user);
                await DisplayAlert("Succeed", "Profile is updated", "Oke");
                await Navigation.PopAsync();
            } catch(Exception ex)
			{
				await DisplayAlert("Error", ex.ToString(), "Oke");
			}

		}
	}
}

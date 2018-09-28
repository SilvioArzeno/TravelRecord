using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        
        private void LoginButton_Clicked(object sender , EventArgs e)
        {
            bool UserEmpty = string.IsNullOrEmpty(UserEntry.Text);
            bool PassEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if (UserEmpty || PassEmpty)
            {
                ErrorLabel.Text = "Username or Password Invalid";
                ErrorLabel.IsVisible = true;
                ForgotButton.IsVisible = true;
            }
            else
            {
                ErrorLabel.IsVisible = false;
                Navigation.PushAsync( new HomePage());
            }
        }

        private void ForgotButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
    }
}

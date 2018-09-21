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
            InitializeComponent();
            BG.WidthRequest = App.PhoneWidth;
            BG.HeightRequest = App.PhoneHeight;

        }

        public void LoginButton_Clicked(object sender , EventArgs e)
        {
            bool UserEmpty = string.IsNullOrEmpty(UserEntry.Text);
            bool PassEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if (UserEmpty || PassEmpty)
            {
                ErrorLabel.Text = "Username or Password Invalid";
            }
            else
            {
                Navigation.PushAsync( new HomePage());
            }
        }
    }
}

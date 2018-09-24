using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravel : ContentPage
	{
		public NewTravel ()
		{
			InitializeComponent ();
		}

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            TravelPost post = new TravelPost()
            {
                Experience = ExperienceEntry.Text
            };

            SQLiteConnection Connection = new SQLiteConnection(App.DBLocation);
            Connection.CreateTable<TravelPost>();
            int rows = Connection.Insert(post);
            Connection.Close();
            if (rows > 0)
                DisplayAlert("Experienced saved", "Your experienced was saved succesfully", "Ok");
            else
                DisplayAlert("Oops!", "Your Experienced was not saved", "Ok");

            Navigation.PushAsync(new HomePage());
        }
    }
}
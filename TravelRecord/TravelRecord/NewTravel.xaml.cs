﻿using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Logic;
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

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var Locator = CrossGeolocator.Current;
            var Position =await Locator.GetPositionAsync();
            var Venues = VenueLogic.GetVenues(Position.Latitude,Position.Longitude);
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            TravelPost post = new TravelPost()
            {
                Experience = ExperienceEntry.Text
            };

            using (SQLiteConnection Connection = new SQLiteConnection(App.DBLocation))
            {
                Connection.CreateTable<TravelPost>();
                int rows = Connection.Insert(post);

                if (rows > 0)
                    DisplayAlert("Experienced saved", "Your experienced was saved succesfully", "Ok");
                else
                    DisplayAlert("Oops!", "Your Experienced was not saved", "Ok");
            }
            Navigation.PushAsync(new HomePage());
        }
    }
}
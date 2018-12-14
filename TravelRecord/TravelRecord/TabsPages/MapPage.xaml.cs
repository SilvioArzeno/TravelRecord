using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord.TabsPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var PermissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (PermissionStatus != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        var Result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                        if (Result.ContainsKey(Permission.Location))
                        {
                            PermissionStatus = Result[Permission.Location];
                            LocationMap.IsShowingUser = true;
                        }
                        else
                        {
                            await DisplayAlert("Location Permission Denied ", "We are unable to open the map without your location", "Ok");
                            LocationMap.IsShowingUser = false;
                        }
                    }

                    else
                    {
                        await DisplayAlert("Location Permission Denied ", "We are unable to open the map without your location", "Ok");
                        LocationMap.IsShowingUser = false;
                    }
                }
                else
                {
                    LocationMap.IsShowingUser = true;
                }

                var Locator = CrossGeolocator.Current;
                var Position = await Locator.GetPositionAsync();
                Locator.PositionChanged += Locator_PositionChanged;
                await Locator.StartListeningAsync(TimeSpan.FromSeconds(0), 100);
                LocationMap.MoveToRegion(new Xamarin.Forms.Maps.MapSpan(new Xamarin.Forms.Maps.Position(Position.Latitude, Position.Longitude), .005f, .005f));
                using (SQLiteConnection Connection = new SQLiteConnection(App.DBLocation))
                {
                    Connection.CreateTable<TravelPost>();
                    var TravelPosts = Connection.Table<TravelPost>().ToList();
                    DisplayInMap(TravelPosts);
                }
            }
            catch(Exception)
            {
                OnDisappearing();
            }
        }

        private void DisplayInMap(List<TravelPost> travelPosts)
        {
            try
            {
                foreach (TravelPost post in travelPosts)
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);

                    var pin = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };
                    if(pin.Label != null)
                    LocationMap.Pins.Add(pin);
                }
            }
            catch (Exception){ }
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            var Locator = CrossGeolocator.Current;
            Locator.PositionChanged -= Locator_PositionChanged;
            await Locator.StopListeningAsync();
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            LocationMap.MoveToRegion(new Xamarin.Forms.Maps.MapSpan(new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude), .005f, .005f));
        }
    }
}
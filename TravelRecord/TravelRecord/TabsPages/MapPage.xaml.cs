using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
            }
            catch(Exception)
            {
               await DisplayAlert("Unable to access map", "We were unable to access the map , please allow the app to access your location", "Ok");
            }
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
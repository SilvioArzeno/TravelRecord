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
            var PermissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if(PermissionStatus != PermissionStatus.Granted)
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
                }
 
            }
        }
    }
}
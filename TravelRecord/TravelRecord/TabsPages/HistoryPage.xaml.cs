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
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            SQLiteConnection Connection = new SQLiteConnection(App.DBLocation);
            Connection.CreateTable<TravelPost>();
            var TravelPosts = Connection.Table<TravelPost>().ToList();
            Connection.Close();

        }

    }
}
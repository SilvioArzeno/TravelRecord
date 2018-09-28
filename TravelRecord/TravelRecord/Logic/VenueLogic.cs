using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;

namespace TravelRecord.Logic
{
    public class VenueLogic
    {
        public async static Task<List<Venue>> GetVenues(double latitude , double longitude)
        {
            List<Venue> Venues = new List<Venue>();
            var URL = Venue.GenerateURL(latitude, longitude);

            using (HttpClient Client = new HttpClient())
            {
               var Response = await Client.GetAsync(URL);
               var Json = await Response.Content.ReadAsStringAsync();
            }
                return Venues;
        } 
    }
}

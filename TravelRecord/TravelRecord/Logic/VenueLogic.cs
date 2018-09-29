using Newtonsoft.Json;
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
            var URL = VenueRoot.GenerateURL(latitude, longitude);

            using (HttpClient Client = new HttpClient())
            {
               var Response = await Client.GetAsync(URL);
               var Json = await Response.Content.ReadAsStringAsync();
               var VenueRoot = JsonConvert.DeserializeObject<VenueRoot>(Json);
                Venues = VenueRoot.Response.venues as List<Venue>;
            }
                return Venues;
        } 
    }
}

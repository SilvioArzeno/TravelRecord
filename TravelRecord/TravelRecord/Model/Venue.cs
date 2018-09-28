using System;
using System.Collections.Generic;
using System.Text;
using TravelRecord.Helpers;

namespace TravelRecord.Model
{
    public class Venue
    {
        public static string GenerateURL(double latitude , double longitude)
        {
            return String.Format(Constants.VENUE_SEARCH, latitude, longitude, Constants.CLIENT_ID, Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}

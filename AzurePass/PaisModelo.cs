using System.Collections.Generic;

namespace AzurePass
{
    public class Item
    {
        public string countryName { get; set; }
        public string capital { get; set; }
        public string continentName { get; set; }
        public string areaInSqKm { get; set; }
        public string languages { get; set; }
        public string currencyCode { get; set; }
    }

    public class RootObject
    {
        public List<Item> geonames { get; set; }
    }

}
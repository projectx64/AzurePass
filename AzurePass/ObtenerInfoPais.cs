using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AzurePass
{
    public class ObtenerInfoPais
    {

        double latitud;
        double longitud;

        internal RootObject ObtenerInformacionGeneral(double latitud, double longitud)
        {
            this.latitud = latitud;
            this.longitud = longitud;
            string codigoPais = ObtenerCodigoPais();
            return ObtenerInformacionPais(codigoPais);
        }

        private string ObtenerCodigoPais()
        {

            var request = HttpWebRequest.Create(string.Format(@"http://api.geonames.org/countrySubdivision?lat={0}&lng={1}&username=osmar", latitud.ToString().Replace(",", "."), longitud.ToString().Replace(",", ".")));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.Out.WriteLine("Error: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        return "Respuesta vacía";
                    }
                    else
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(content);
                        XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("/geonames/countrySubdivision/countryCode");
                        return node.InnerText;
                    }

                    //Assert.NotNull(content);
                }
            }
        }

        private RootObject ObtenerInformacionPais(string codigoPais)
        {
            var request = HttpWebRequest.Create(string.Format(@"http://api.geonames.org/countryInfoJSON?formatted=true&lang=es&country={0}&username=osmar&style=full", codigoPais));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.Out.WriteLine("Error: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string content = reader.ReadToEnd();

                    if (string.IsNullOrWhiteSpace(content))
                    {
                        return new RootObject();
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<RootObject>(content);
                    }
                }
            }
        }

    }
}
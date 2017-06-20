using Android.App;
using Android.Widget;
using Android.OS;
using Plugin.Geolocator;
using System;

namespace AzurePass
{
    [Activity(Label = "AzurePass", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected async  override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var paisNombre = FindViewById<TextView>(Resource.Id.textViewResultNombre);
            var capital = FindViewById<TextView>(Resource.Id.textViewResultCapital);
            var continente = FindViewById<TextView>(Resource.Id.textViewResultContinente);
            var extension = FindViewById<TextView>(Resource.Id.textViewResultExtension);
            var idiomas = FindViewById<TextView>(Resource.Id.textViewResultIdiomas);
            var moneda = FindViewById<TextView>(Resource.Id.textViewResultMoneda);

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(10000);
            RootObject pais = new ObtenerInfoPais().ObtenerInformacionGeneral(position.Latitude, position.Longitude);

            paisNombre.Text = pais.geonames[0].countryName ;
            capital.Text = pais.geonames[0].capital;
            continente.Text = pais.geonames[0].continentName;
            extension.Text = pais.geonames[0].areaInSqKm + " km2";
            idiomas.Text = pais.geonames[0].languages;
            moneda.Text = pais.geonames[0].currencyCode;

        }
    }
}


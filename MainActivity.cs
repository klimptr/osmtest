using System;
using System.Globalization;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using OsmSharp.Android.UI;
using OsmSharp.Android.UI.Data.SQLite;
using OsmSharp.Math.Geo;
using OsmSharp.UI.Map;
using OsmSharp.UI.Map.Layers;

namespace App1
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    public class MainActivity : AppCompatActivity
    {
        MapView _mapView;
        Layer _mapLayer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Native.Initialize();
            
            _mapView = new MapView(this, new MapViewSurface(this));
            _mapView.MapTilt = 0;
            _mapView.MapCenter = new GeoCoordinate(53.770226, 20.490189);
            _mapView.MapZoom = 12;
            _mapView.Map = new Map();
            _mapView.MapAllowZoom = true;
            _mapLayer = _mapView.Map.AddLayerTile("http://a.tile.openstreetmap.de/tiles/osmde/{0}/{1}/{2}.png");

            using (var bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.pin))
            {
                var marker = new MapMarker(this, new GeoCoordinate(53.770226, 20.490189), MapMarkerAlignmentType.CenterBottom, bitmap);
                _mapView.AddMarker(marker);
            }
            var layout = FindViewById<RelativeLayout>(Resource.Id.Mapka);
            layout.AddView(_mapView);
        }

        [Java.Interop.Export("Goto")]
        public void Goto(View v)
        {
            var latitude = FindViewById<EditText>(Resource.Id.latitude);
            var longitude = FindViewById<EditText>(Resource.Id.longitude);

            double latitudeValue = double.Parse(latitude.Text, CultureInfo.InvariantCulture);
            double longitudeValue = double.Parse(longitude.Text, CultureInfo.InvariantCulture);

            _mapView.MapCenter = new GeoCoordinate(latitudeValue, longitudeValue);
            using (var bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.pin))
            {
                var marker = new MapMarker(this, new GeoCoordinate(latitudeValue, longitudeValue), MapMarkerAlignmentType.CenterBottom, bitmap);
                _mapView.AddMarker(marker);
            }
        }
    }
}
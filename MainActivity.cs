using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
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

            Native.Initialize();

            _mapView = new MapView(this, new MapViewSurface(this));
            _mapView.MapTilt = 0; // must be set otherwise NullReferenceException
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


            var layout = new RelativeLayout(this);
            layout.AddView(_mapView);


            SetContentView(layout);





        }
    }
}
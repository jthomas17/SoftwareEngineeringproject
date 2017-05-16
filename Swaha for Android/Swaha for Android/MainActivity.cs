using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Runtime;
using Android.Provider;
using System;
using Java.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace Swaha_for_Android
{
    [Activity(Label = "Swaha_for_Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ActionBar.Hide();
            SetContentView(Resource.Layout.OpeningPage);

            Handler h = new Handler();
            Action LoadScreen = () =>
             {
                 StartActivity(typeof(CameraActivity));
             };
            h.PostDelayed(LoadScreen,5000);

            // Set our view from the "main" layout resource  
            // SetContentView (Resource.Layout.Main);
        }
    }


}


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
    //Activity that handles the loading screen
    [Activity(Label = "Swaha for Android", MainLauncher = true, Icon = "@drawable/ICONLOGO")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ActionBar.Hide();
            SetContentView(Resource.Layout.OpeningPage);

            //Delays the next activity from starting for 4 seconds.
            Handler h = new Handler();
            Action LoadScreen = () =>
             {
                 StartActivity(typeof(CameraActivity));
             };
            h.PostDelayed(LoadScreen,4000);
        }
    }


}


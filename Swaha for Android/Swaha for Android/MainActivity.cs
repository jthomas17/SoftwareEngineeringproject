using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Swaha_for_Android
{
    [Activity(Label = "Swaha_for_Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            Button startPicActivityButton = FindViewById<Button>(Resource.Id.StartPicActivityButton);
            startPicActivityButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(PicSelectAvtivity));
                StartActivity(intent);
            };
        }
    }
}


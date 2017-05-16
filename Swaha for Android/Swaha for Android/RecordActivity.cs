using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Media.Projection;
using Android.Net;
using Android.Provider;
using Android.Hardware.Display;
using Java.IO;

namespace Swaha_for_Android
{
    [Activity(Label = "RecordActivity")]
    public class RecordActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.RecordScreen); 
           

            Button toPreview = FindViewById<Button>(Resource.Id.stop);
            toPreview.Click += delegate
            {
                // Add an activity that is for previewing video
                // currently loops back to main
                var PreviewIntent = new Intent(this, typeof(MainActivity));
                StartActivity(PreviewIntent);
            };
        }
    }
}
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


    [Activity(Label = "Preview")]
    public class Preview : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Preview);
            var play = FindViewById<Button>(Resource.Id.Play_Video);
            // Create your application here
            string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/DCIM/Camera/testing.mp4";
            var video = FindViewById<VideoView>(Resource.Id.surfaceView1);
            
            var uri = Android.Net.Uri.Parse(path);
            play.Click += delegate
            {
                video.SetVideoURI(uri);
                video.Start();
            };
        }
    }
}
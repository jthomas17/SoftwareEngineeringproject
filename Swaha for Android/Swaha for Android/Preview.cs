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

            ActionBar.Hide();
            SetContentView(Resource.Layout.Preview);

            var play = FindViewById<ImageButton>(Resource.Id.previewButton);
            var video = FindViewById<VideoView>(Resource.Id.PreviewVid);
            var mail = FindViewById<Button>(Resource.Id.toEmail);
            var save = FindViewById<Button>(Resource.Id.toPhone);
            
            string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/DCIM/Camera/Titletest.mp4";
            var uri = Android.Net.Uri.Parse(path);

            //plays story that was just created
            play.Click += delegate
            {
                play.Visibility = Android.Views.ViewStates.Invisible;
                video.SetVideoURI(uri);
                video.Start();
            };

            // Add in functionality to save to phone
            save.Click += delegate
            {

            };
            
            //enables user to send story via android email intent
            mail.Click += delegate
            {
                var email = new Intent(Intent.ActionSend);
                email.PutExtra(Intent.ExtraEmail, new[] { "jthomas17@my.whitworth.edu" });
                email.PutExtra(Intent.ExtraSubject, "Sample email with attachment");
                email.PutExtra(Intent.ExtraStream, uri);
                email.SetType("message/rfc822");
                this.StartActivity(Intent.CreateChooser(email, "Send..."));
            };
        }
    }
}
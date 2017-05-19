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
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;


public static class App
{
    public static File _file;
    public static File _dir;
    public static Bitmap bitmap;
}

namespace Swaha_for_Android
{
    [Activity(Label = "CameraActivity")]
    public class CameraActivity : Activity
    {

        private ImageView _imageView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ActionBar.Hide();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Confirms that the device has camera taking abilities, if so activates camera button
            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                Button button = FindViewById<Button>(Resource.Id.myButton);
                button.Click += TakeAPicture;
            }

            //Starts Activity to select pictures for recording
            Button gotoPicSelect = FindViewById<Button>(Resource.Id.GoToPictureSelect);
            gotoPicSelect.Click += delegate
            {
                var SelectPicturesIntent = new Intent(this, typeof(SelectPicturesActivity));
                StartActivity(SelectPicturesIntent);
            };

            var play1 = FindViewById<ImageButton>(Resource.Id.previewButton1);
            var video1 = FindViewById<VideoView>(Resource.Id.PreviewVid1);
            var play2 = FindViewById<ImageButton>(Resource.Id.previewButton2);
            var video2 = FindViewById<VideoView>(Resource.Id.PreviewVid2);
            var play3 = FindViewById<ImageButton>(Resource.Id.previewButton3);
            var video3 = FindViewById<VideoView>(Resource.Id.PreviewVid3);
            var play4 = FindViewById<ImageButton>(Resource.Id.previewButton4);
            var video4 = FindViewById<VideoView>(Resource.Id.PreviewVid4);
            var play5 = FindViewById<ImageButton>(Resource.Id.previewButton5);
            var video5 = FindViewById<VideoView>(Resource.Id.PreviewVid5);

            string path1 = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/DCIM/Camera/testing.mp4";
            var uri1 = Android.Net.Uri.Parse(path1);

            string path2 = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/DCIM/Camera/testing.mp4";
            var uri2 = Android.Net.Uri.Parse(path2);

            string path3 = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/DCIM/Camera/testing.mp4";
            var uri3 = Android.Net.Uri.Parse(path3);

            string path4 = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/DCIM/Camera/testing.mp4";
            var uri4 = Android.Net.Uri.Parse(path4);

            string path5 = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/DCIM/Camera/testing.mp4";
            var uri5 = Android.Net.Uri.Parse(path5);

            play1.Click += delegate
            {
                play1.Visibility = Android.Views.ViewStates.Invisible;
                video1.SetVideoURI(uri1);
                video1.Start();
            };
            play2.Click += delegate
            {
                play2.Visibility = Android.Views.ViewStates.Invisible;
                video2.SetVideoURI(uri2);
                video2.Start();
            };
            play3.Click += delegate
            {
                play3.Visibility = Android.Views.ViewStates.Invisible;
                video3.SetVideoURI(uri3);
                video3.Start();
            };
            play4.Click += delegate
            {
                play4.Visibility = Android.Views.ViewStates.Invisible;
                video4.SetVideoURI(uri4);
                video4.Start();
            };
            play5.Click += delegate
            {
                play5.Visibility = Android.Views.ViewStates.Invisible;
                video5.SetVideoURI(uri5);
                video5.Start();
            };

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. Resizes the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            if (App.bitmap != null)
            {
                _imageView.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
        }

        //Sets up android camera intent
        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }

        //creates a directory for pictures taken in the app
        private void CreateDirectoryForPictures()
        {
            App._dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "Swaha_for_Android");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        // checks to see if there is camera capabilities on the device
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

      
    }
}
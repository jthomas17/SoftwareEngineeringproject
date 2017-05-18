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
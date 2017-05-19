using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Swaha_for_Android
{
    class SimpleImageScalerTask : AsyncTask<string, Java.Lang.Void, Bitmap>
    {
        private ImageView image;
        // private readonly WeakReference<Bitmap> softRef;
        public SimpleImageScalerTask(ImageView tile)
        {
            image = tile;
        }

        protected override Bitmap RunInBackground(params string[] @params)
        {
            return scaleBitmap(@params[0]);
        }

        protected override void OnPostExecute(Bitmap result)
        {
           image.SetImageBitmap(result);
        }

        private Bitmap scaleBitmap(string address)
        {
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InSampleSize = 8;
            Bitmap bitmap = BitmapFactory.DecodeFile(address, options);

            return bitmap;
        }
    }
}
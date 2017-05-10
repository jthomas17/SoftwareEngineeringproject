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
    class ImageScalerTask : AsyncTask<string, Java.Lang.Void, Bitmap>
    {
        private readonly System.WeakReference imageButtonReference;

        public ImageScalerTask(ImageButton tile)
        {
            imageButtonReference = new WeakReference(tile);
        }

        protected override Bitmap RunInBackground(params string[] @params)
        {
            return scaleBitmap(@params[0]);
        }

        protected override void OnPostExecute(Bitmap result)
        {
            if (IsCancelled)
            {
                result = null;
            }

            if (imageButtonReference != null)
            {
                ImageButton tile = (ImageButton)imageButtonReference.Target;
                if (tile != null)
                {
                    if (result != null)
                        tile.SetImageBitmap(result);
                    else
                        tile.SetImageResource(Resource.Drawable.Icon);
                }
            }
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
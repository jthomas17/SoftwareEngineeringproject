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
        private GridViewAdapter.ViewHolder holder;
        private int pos;
        
        public ImageScalerTask(GridViewAdapter.ViewHolder tile, int p)
        {
            holder = tile;
            pos = p;
        }

        protected override Bitmap RunInBackground(params string[] @params)
        {

            if (holder.Position == pos)
            {
                return scaleBitmap(@params[0]);
            }
            else return null;
        }

        protected override void OnPostExecute(Bitmap result)
        {
            if (holder.Position == pos)
            {
                holder.Thumbnail.SetImageBitmap(result);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Swaha_for_Android
{
    class UserImage
    {
        private Android.Net.Uri imageDefinition;
        private int imageId;
        private string imagePathString;
        private Bitmap bip;
        
        public UserImage(Android.Net.Uri path, int id, string fileString)
        {
            imageDefinition = path;
            imageId = id;
            imagePathString = fileString;
        }

        public int id
        {
            get { return imageId; }
        }

        public Android.Net.Uri uri
        {
            get { return imageDefinition; }
        }
        public string filestring
        {
            get { return imagePathString; }
        }

        public void SetScaledImage(bool positionFlag)
        {
            if (positionFlag == true)
            {
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.InSampleSize = 8;
                Bitmap bitmap = BitmapFactory.DecodeFile(imagePathString, options);
                bip = bitmap;
            }
            else
            {
                bip = null;
            }
                
        }
        public Bitmap bitmap
        {
            get { return bip; }
        }
    }
}
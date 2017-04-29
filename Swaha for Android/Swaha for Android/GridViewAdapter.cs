using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Database;
using Android.Provider;
using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;

namespace Swaha_for_Android
{
    class GridViewAdapter : BaseAdapter
    {
        Activity _activity;
        List<Bitmap> imageList;

        public GridViewAdapter(Activity activity)
        {
            _activity = activity;
            fillImages();
        }

        void fillImages()
        {
            // Button galleryButton = FindViewById<Button>(Resource.Id.GalleryButton);
            var imageUri = MediaStore.Images.Media.ExternalContentUri;

            string[] projection = { MediaStore.Images.Media.InterfaceConsts.Data };

            var loader = new CursorLoader(_activity, MediaStore.Images.Media.ExternalContentUri, projection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();
            imageList = new List<Bitmap>();
            int columnIndex = cursor.GetColumnIndex(projection[0]);
            
            if (cursor.MoveToFirst())
            {
                do
                {
                    if (cursor.IsLast)
                    {
                        break;
                    }
                    var pictureUri = ContentUris.WithAppendedId(MediaStore.Images.Media.ExternalContentUri, cursor.GetLong(columnIndex));
                    // gotta reformat the bitmap or else it doesn't show
                    Bitmap bitmap = BitmapFactory.DecodeFile(cursor.GetString(columnIndex));
                    //galleryImage.SetImageBitmap(bitmap);
                    imageList.Add(bitmap);
                    
                } while (cursor.MoveToNext());
            }
        }

        public override int Count
        {
            get { return imageList.Count; }
        }

        public override Java.Lang.Object GetItem (int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return imageList[position].GenerationId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.PicSelectGridViewChildLayout, parent, false);
            var image = view.FindViewById<ImageView>(Resource.Id.gridImage);

            image.SetImageBitmap(imageList[position]);
            return view;
        }
        
    }
}
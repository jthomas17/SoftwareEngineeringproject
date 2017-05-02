using System.Collections.Generic;
using System.Linq;


using Android.Database;
using Android.Provider;
using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Swaha_for_Android
{
    class GridViewAdapter : BaseAdapter
    {
        Activity _activity;

        // probably should not use a list of bitmaps TODO
        List<UserImage> imageList;

        public GridViewAdapter(Activity activity)
        {
            _activity = activity;
            fillImages();
        }

        void fillImages()
        {
            // Button galleryButton = FindViewById<Button>(Resource.Id.GalleryButton);
            var imageUri = MediaStore.Images.Media.ExternalContentUri;
            
            string[] projection = { MediaStore.Images.Media.InterfaceConsts.Data,
                                    MediaStore.Images.Media.InterfaceConsts.Id};

            var loader = new CursorLoader(_activity, MediaStore.Images.Media.ExternalContentUri, projection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();
            int[] columnIndex = { cursor.GetColumnIndex(projection[0]), cursor.GetColumnIndex(projection[1])};

            imageList = new List<UserImage>();
            
            if (cursor.MoveToFirst())
            {
                do
                {
                    if (cursor.IsLast)
                    {
                        break;
                    }
                    Android.Net.Uri pictureUri = ContentUris.WithAppendedId(MediaStore.Images.Media.ExternalContentUri, cursor.GetLong(columnIndex[0]));
                    string pictureUriString = cursor.GetString(columnIndex[0]);
                    int pictureID = cursor.GetInt(columnIndex[1]);
                    
                    imageList.Add(new UserImage(pictureUri, pictureID, pictureUriString));
                    
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
            return imageList[position].GetId();
        }

        /*
        TODO: I think this currently is returning an entire gridview
        for every instance of an image. Need to fix
        */
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.PicSelectGridViewChildLayout, parent, false);
            //var image = view.FindViewById<ImageView>(Resource.Id.gridImage);

            // NEED THIS LATER IN A DIFFERENT FUNCTION
            //Bitmap bitmap = BitmapFactory.DecodeFile(imageList[position].GetUri().ToString());
            Bitmap bitmap = BitmapFactory.DecodeFile(imageList[position].GetFileString());

            image.SetImageBitmap(bitmap);
            return view;
        }
        
    }
}
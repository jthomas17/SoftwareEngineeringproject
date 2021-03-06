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
    class GridItemLoader
    {
        public List<UserImage> gridItems { get; private set; }
        private Activity _activity;
        public GridItemLoader(Activity activity)
        {
            _activity = activity;
            gridItems = new List<UserImage>();
            fillImages();
        }

        void fillImages()
        {
            // Button galleryButton = FindViewById<Button>(Resource.Id.GalleryButton);
            var imageUri = MediaStore.Images.Media.ExternalContentUri;

            string[] projection = { MediaStore.Images.Media.InterfaceConsts.Data,
                                    MediaStore.Images.Media.InterfaceConsts.Id,};

            var loader = new CursorLoader(_activity, imageUri, projection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();
            int[] columnIndex = { cursor.GetColumnIndex(projection[0]), cursor.GetColumnIndex(projection[1])};

            gridItems = new List<UserImage>();

            if (cursor.MoveToFirst())
            {
                do
                {
                    if (cursor.IsLast)
                    {
                        break;
                    }
                    //   not sure why this was here
                    //Android.Net.Uri pictureUri = ContentUris.WithAppendedId(MediaStore.Images.Media.ExternalContentUri, cursor.GetLong(columnIndex[0]));

                    string pictureUriString = cursor.GetString(columnIndex[0]);
                    int pictureID = cursor.GetInt(columnIndex[1]);
                    Android.Net.Uri pictureUri = Android.Net.Uri.Parse(pictureUriString);

                    gridItems.Add(new UserImage(pictureUri, pictureID, pictureUriString));

                } while (cursor.MoveToNext());
            }
        }

    }
}
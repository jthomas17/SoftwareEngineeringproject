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
using System.Threading.Tasks;
using Android.Graphics.Drawables;

namespace Swaha_for_Android
{
    class GridViewAdapter : BaseAdapter<UserImage>
    {
        Context _context;
        Activity _activity;
        GridItemLoader _gridItemLoader;

        /*
        public GridViewAdapter(Activity activity, GridItemLoader gridItemLoader)
        {
            _activity = activity;
            _gridItemLoader = gridItemLoader;
        }
        */
        public GridViewAdapter(Context context, GridItemLoader gridItemLoader)
        {
            _context = context;
            _gridItemLoader = gridItemLoader;
        }

        /*
        void fillImages()
        {
            // Button galleryButton = FindViewById<Button>(Resource.Id.GalleryButton);
            var imageUri = MediaStore.Images.Media.ExternalContentUri;
            
            string[] projection = { MediaStore.Images.Media.InterfaceConsts.Data,
                                    MediaStore.Images.Media.InterfaceConsts.Id,
                                    MediaStore.Images.Thumbnails.Data};

            var loader = new CursorLoader(_activity, MediaStore.Images.Media.ExternalContentUri, projection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();
            int[] columnIndex = { cursor.GetColumnIndex(projection[0]), cursor.GetColumnIndex(projection[1]), cursor.GetColumnIndex(projection[2])};

            imageList = new List<UserImage>();
            
            if (cursor.MoveToFirst())
            {
                do
                {
                    if (cursor.IsLast)
                    {
                        break;
                    }
                    //Android.Net.Uri pictureUri = ContentUris.WithAppendedId(MediaStore.Images.Media.ExternalContentUri, cursor.GetLong(columnIndex[0]));

                    string pictureUriString = cursor.GetString(columnIndex[0]);
                    int pictureID = cursor.GetInt(columnIndex[1]);
                    string thumbnailString = cursor.GetString(columnIndex[2]);
                    Android.Net.Uri pictureUri = Android.Net.Uri.Parse(pictureUriString);
                    Android.Net.Uri thumbUri = Android.Net.Uri.Parse(thumbnailString);

                    imageList.Add(new UserImage(pictureUri, pictureID, pictureUriString, thumbUri, thumbnailString));
                    
                } while (cursor.MoveToNext());
            }
        }
        */

        public override int Count
        {
            get { return _gridItemLoader.gridItems.Count; }
        }

        public override UserImage this[int position]
        {
            get { return _gridItemLoader.gridItems[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.PicSelectGridViewChildLayout, parent, false);
            var image = view.FindViewById<ImageView>(Resource.Id.gridImage);

            

            //image.SetScaleType(ImageView.ScaleType.CenterCrop);

            //image.SetImageURI(imageList[position].uri);

            //BitmapFactory.Options options = await GetBitmapOptionsOfImageAsync(_gridItemLoader.gridItems[position].filestring);
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InSampleSize = 8;
            Bitmap bitmap = BitmapFactory.DecodeFile(_gridItemLoader.gridItems[position].filestring, options);
            //image.SetImageURI(_gridItemLoader.gridItems[position].thumburi);
            image.SetImageBitmap(bitmap);
            //bitmap.Recycle();
            return view;
        }
        
    }
}
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
//using Android.Database;

namespace Swaha_for_Android
{
    [Activity(Label = "PicSelectAvtivity")]
    public class PicSelectAvtivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PicSelect);
            
            

            Button galleryButton = FindViewById<Button>(Resource.Id.GalleryButton);
            ImageView galleryImage = FindViewById<ImageView>(Resource.Id.myImageView);

            galleryButton.Click += delegate
            {
                var imageUri = MediaStore.Images.Media.ExternalContentUri;
                
                string[] projection = {MediaStore.Images.Media.InterfaceConsts.Data};

                // might use this later to use .LoadInBackground()
                //var loader = new CursorLoader(this, imageUri, projection, null, null, null);

                // create cursor to retrieve pictures
                using (ICursor cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, projection, null, null, null))
                {
                    int columnIndex = cursor.GetColumnIndex(projection[0]);

                    if (cursor.MoveToFirst())
                    {
                        var pictureUri = ContentUris.WithAppendedId(MediaStore.Images.Media.ExternalContentUri, cursor.GetLong(columnIndex));
                        // gotta reformat the bitmap or else it doesn't show
                        Bitmap bitmap = BitmapFactory.DecodeFile(cursor.GetString(columnIndex));
                        galleryImage.SetImageBitmap(bitmap);
                    }


                }
            };
            


        }

        /*
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                var imageView = FindViewById<ImageView>(Resource.Id.MyImageView);
                imageView.SetImageURI(data.Data);
            }
        }
        */

    }
}
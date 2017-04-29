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

namespace Swaha_for_Android
{
    [Activity(Label = "PicSelectAvtivity")]
    public class PicSelectAvtivity : Activity
    {
        private GridView theGrid;
        private GridViewAdapter gridAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the layout
            SetContentView(Resource.Layout.PicSelect);

            theGrid = FindViewById<GridView>(Resource.Id.mygridview);
            gridAdapter = new GridViewAdapter(this);
            theGrid.Adapter = gridAdapter;

            // Button galleryButton = FindViewById<Button>(Resource.Id.GalleryButton);
            var imageUri = MediaStore.Images.Media.ExternalContentUri;
            string[] projection = {MediaStore.Images.Media.InterfaceConsts.Data};

            // might use this later to use .LoadInBackground()
            //var loader = new CursorLoader(this, imageUri, projection, null, null, null);

            // create cursor to retrieve pictures
            
              

        }


    }
}
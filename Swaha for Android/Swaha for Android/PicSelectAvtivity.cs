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
        GridItemLoader gridLoader;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ActionBar.Hide();
            // Set the layout
            SetContentView(Resource.Layout.SelectPictures);

            Button toRecord = FindViewById<Button>(Resource.Id.toRecord);
            toRecord.Click += delegate
            {
                var RecordIntent = new Intent(this, typeof(RecordActivity));
                StartActivity(RecordIntent);
            };

            //gridLoader = new GridItemLoader(this);
            //theGrid = FindViewById<GridView>(Resource.Id.mygridview);
            //gridAdapter = new GridViewAdapter(this, gridLoader);
            //theGrid.Adapter = gridAdapter;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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

            // Set the layout
            SetContentView(Resource.Layout.PicSelect);
            gridLoader = new GridItemLoader(this);
            gridAdapter = new GridViewAdapter(this, gridLoader);

            theGrid = FindViewById<GridView>(Resource.Id.mygridview);
            
            theGrid.Adapter = gridAdapter;
            theGrid.ScrollStateChanged += delegate
            {
                theGrid.InvalidateViews();
            };
        }
        
        
    }
}
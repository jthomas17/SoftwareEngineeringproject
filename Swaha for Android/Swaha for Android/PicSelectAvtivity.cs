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
        private List<UserImage> StoryImagesYo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the layout
            SetContentView(Resource.Layout.PicSelect);
            gridLoader = new GridItemLoader(this);
            gridAdapter = new GridViewAdapter(this, gridLoader);

            theGrid = FindViewById<GridView>(Resource.Id.mygridview);

            StoryImagesYo = new List<UserImage>();
            theGrid.ItemClick += GridView_ItemClick;
            theGrid.Adapter = gridAdapter;
            theGrid.ChoiceMode = ChoiceMode.Multiple;
            Button toRecordActivityButton = FindViewById<Button>(Resource.Id.GoToRecordActivity);
            toRecordActivityButton.Click += delegate
            {
                var RecordIntent = new Intent(this, typeof(RecordActivity));
                RecordIntent.PutExtra("storybundle", StoryBundle());
                StartActivity(RecordIntent);
            };

            theGrid.Scroll += delegate
            {
                gridAdapter.spinning = true;
                gridAdapter.NotifyDataSetChanged();
            };
            theGrid.ScrollStateChanged += delegate
            {
                gridAdapter.spinning = false;
                gridAdapter.NotifyDataSetChanged();
            };
            
        }

        void GridView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
        {
            e.View.Selected = true;
            StoryImagesYo.Add(gridAdapter[e.Position]);
        }

        public Bundle StoryBundle()
        {
            Bundle b = new Bundle();
            List<string> list = new List<string>();
            for (int i = 0; i < StoryImagesYo.Count; i++)
            {
                list.Add(StoryImagesYo[i].filestring);
            }
            b.PutStringArrayList("list", list);
            return b;
        }
    }
}
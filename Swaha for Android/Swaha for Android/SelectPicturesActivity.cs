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
    [Activity(Label = "SelectPicturesActivity")]
    public class SelectPicturesActivity : Activity
    {
        private GridView theGrid;
        private GridViewAdapter gridAdapter;

        private List<string> list;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();

            // Set the layout
            SetContentView(Resource.Layout.SelectPictures);
            
            // Set references to views
            Button toRecord = FindViewById<Button>(Resource.Id.toRecord);
            Button toCamRoll = FindViewById<Button>(Resource.Id.camRoll);
            theGrid = FindViewById<GridView>(Resource.Id.mygridview);

            // Get grid collection ready
            list = new List<string>();
            gridAdapter = new GridViewAdapter(this, list);
            theGrid.Adapter = gridAdapter;

            // Add photo delegate
            toCamRoll.Click += delegate {
                var imageIntent = new Intent(Intent.ActionPick);
                imageIntent.SetType("image/*");
                imageIntent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(
                    Intent.CreateChooser(imageIntent, "Select photo"), 0);
            };

            // Continue to RecordActivity delegate
            toRecord.Click += delegate
            {
                var RecordIntent = new Intent(this, typeof(RecordActivity));
                StartActivity(RecordIntent);
            };
        }

        // Occurs once the user has selected a photo to use
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                AddUriToList(data.Data);
            }
        }


        public void AddUriToList(Android.Net.Uri uri)
        {
            list.Add(uri.Path);
            gridAdapter.NotifyDataSetChanged();
        }
    }
}
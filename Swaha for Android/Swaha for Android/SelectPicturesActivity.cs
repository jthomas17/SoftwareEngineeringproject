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

        private const int MAX_IMAGES = 10;

        private IList<string> list;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();

            // Set the layout
            SetContentView(Resource.Layout.SelectPictures);

            // Set references to views
            Button toRecord = FindViewById<Button>(Resource.Id.toRecord);
            Button toCamRoll = FindViewById<Button>(Resource.Id.camRoll);
            theGrid = FindViewById<GridView>(Resource.Id.SelectPicturesGrid);
            

            // Get grid collection ready
            list = new List<string>();
            gridAdapter = new GridViewAdapter(this, list);
            theGrid.Adapter = gridAdapter;

            if (list.Count == MAX_IMAGES)
                toCamRoll.Enabled = false;

            // Add photo delegate
            toCamRoll.Click += delegate
            {
                var imageIntent = new Intent(Intent.ActionPick);
                imageIntent.SetType("image/*");
                imageIntent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(
                    Intent.CreateChooser(imageIntent, "Select photo"), 0);
                toRecord.Enabled = true;
            };

            // Continue to RecordActivity delegate
            toRecord.Click += delegate
            {
                var RecordIntent = new Intent(this, typeof(RecordActivity));
                RecordIntent.PutExtras(StoryBundle());
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

        // Adds path to the collection and updates gridview adapter
        private void AddUriToList(Android.Net.Uri uri)
        {
            list.Add(GetPathToImage(uri));
            gridAdapter.NotifyDataSetChanged();
        }

        // Does the nitty gritty in retrieving string filepath from URI
        private string GetPathToImage(Android.Net.Uri uri)
        {
            string doc_id = "";
            using (var c1 = ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                string document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }

            string path = null;

            // The projection contains the columns we want to return in our query.
            string selection = MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = ContentResolver.Query(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] { doc_id }, null))
            {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;
        }
    
        // Packages image paths to next activity
        public Bundle StoryBundle()
        {
            Bundle b = new Bundle();
            //List<string> list = new List<string>();
            //for (int i = 0; i < list.Count; i++)
            //{
            //    list.Add(list[i]);
            //}
            b.PutStringArrayList("story", list);
            return b;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Graphics;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Media.Projection;
using Android.Net;
using Android.Provider;
using Android.Hardware.Display;
using Java.IO;

namespace Swaha_for_Android
{
    [Activity(Label = "RecordActivity")]
    public class RecordActivity : Activity/*, View.IOnTouchListener*/
    {
        // Holds the selected pictures by the user
        IList<string> bundledList;

        // Holds a collection of imageview references for assigning delegates
        List<ImageView> storyImageList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();

            SetContentView(Resource.Layout.RecordScreen);
            storyImageList = new List<ImageView>(10);
            //GridItemLoader gload = new GridItemLoader(this);

            LinearLayout photoTray = FindViewById<LinearLayout>(Resource.Id.phototrayholder);
            //photoTray.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.MarginLayoutParams.MatchParent, 20);
            var storyImage = FindViewById<ImageView>(Resource.Id.StoryPicture);
            storyImage.Rotation = 90;

            Button Delete = FindViewById<Button>(Resource.Id.delete);
            Delete.Click += (sender, args) =>
            {
                var confirm = new AlertDialog.Builder(this);
                confirm.SetMessage("Are you sure you want to delete this story?");
                confirm.SetPositiveButton("Yes", (s, e) => 
                {
                    
                });
                confirm.SetNegativeButton("No", (s, e) => { /* do something on Cancel click */ });
                confirm.Create().Show();
            };

            // retrieve picture file data from bundle
            bundledList = this.Intent.Extras.GetStringArrayList("story");

            // async operation to implement photos to the top view
            // also adds views to the storyImageList for delegates
            for (int i = 0; i < bundledList.Count; i++)
            {
                ImageView img = new ImageView(this);
                img.Rotation = 90;
                photoTray.AddView(img);
                storyImageList.Add(img);
                new SimpleImageScalerTask(img).Execute(bundledList[i]);
               
                //img.SetOnTouchListener(this);
            }

            // fills the rest of the storyImageList with dummy imageviews
             if ( bundledList.Count < 10)
            {
                for (int i = bundledList.Count; i < 10; i++)
                {
                    ImageView img = new ImageView(this);
                    storyImageList.Add(img);
                }
            }

            //for (int j = 0; j < storyImageList.Count; j++)
            //{
            //    storyImageList[j].Click += delegate{
            //        storyImage.SetImageBitmap(BitmapFactory.DecodeFile(bundledList[j]));
            //    };
            //}
            
            // Yes, it's horrible code, but we couldn't figure out how to 
            // assign delegates programatically. So here's a shitton of 
            // delegates, each referencing an imageview that may or may not
            // be showing.
            storyImageList[0].Click += delegate
            {
                storyImage.SetImageURI(Uri.Parse(bundledList[0]));
            };
            storyImageList[1].Click += delegate
            {
                storyImage.SetImageURI(Uri.Parse(bundledList[1]));
            };
            storyImageList[2].Click += delegate
            {
                storyImage.SetImageURI(Uri.Parse(bundledList[2]));
            };
            
            storyImageList[3].Click += delegate
            {
                storyImage.SetImageURI(Uri.Parse(bundledList[3]));
            };
            storyImageList[4].Click += delegate
            {
                storyImage.SetImageURI(Uri.Parse(bundledList[4]));
            };
                storyImageList[5].Click += delegate
                {
                    storyImage.SetImageURI(Uri.Parse(bundledList[5]));
                };
                storyImageList[6].Click += delegate
                {
                    storyImage.SetImageURI(Uri.Parse(bundledList[6]));
                };
                storyImageList[7].Click += delegate
                {
                    storyImage.SetImageURI(Uri.Parse(bundledList[7]));
                };
                storyImageList[8].Click += delegate
                {
                    storyImage.SetImageURI(Uri.Parse(bundledList[8]));
                };
                storyImageList[9].Click += delegate
                {
                    storyImage.SetImageURI(Uri.Parse(bundledList[9]));
                };

            // go to next activity, Preview video
            Button toPreview = FindViewById<Button>(Resource.Id.stop);
            toPreview.Click += delegate
            {
                // Add an activity that is for previewing video
                // currently loops back to main

                // TODO SEND TO SERVER
                //System.Net.WebClient Client = new System.Net.WebClient();
                //Client.Headers.Add("Content-Type", "binary/octet-stream");
                //byte[] result = Client.UploadFile("http://cs1.whitworth.edu/swaha/fileUpload.php", "POST", bundledList[0]);
                //string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);

                var PreviewIntent = new Intent(this, typeof(Preview));
                StartActivity(PreviewIntent);
            };

        }

        //public bool OnTouch(View v, MotionEvent e)
        //{
        //    switch (e.Action)
        //    {
        //        case
        //    }
        //    return true;
        //}
        
    }
}
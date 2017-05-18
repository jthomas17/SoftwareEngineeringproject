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
    public class RecordActivity : Activity
    {
        IList<string> bundledList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();

            SetContentView(Resource.Layout.RecordScreen);

            //GridItemLoader gload = new GridItemLoader(this);

            LinearLayout photoTray = FindViewById<LinearLayout>(Resource.Id.phototrayholder);
            //photoTray.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.MarginLayoutParams.MatchParent, 20);

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

            // this mimics the bundle data for testing purposes
            bundledList = this.Intent.Extras.GetStringArrayList("story");
            //for (int j = 0; j < 20; j++)
            //{
            //    bundledList.Add(gload.gridItems[j].filestring);
            //}
            
            // async operation to implement photos to the top view
            for (int i = 0; i < bundledList.Count; i++)
            {
                ImageView img = new ImageView(this);
                photoTray.AddView(img);
                new SimpleImageScalerTask(img).Execute(bundledList[i]);
            }
            
            Button toPreview = FindViewById<Button>(Resource.Id.stop);
            toPreview.Click += delegate
            {
                // Add an activity that is for previewing video
                // currently loops back to main
                var PreviewIntent = new Intent(this, typeof(Preview));
                StartActivity(PreviewIntent);
            };

        }
        
    }
}
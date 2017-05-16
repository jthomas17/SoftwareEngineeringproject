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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();

            SetContentView(Resource.Layout.RecordScreen);

            GridItemLoader gload = new GridItemLoader(this);

            LinearLayout photoTray = FindViewById<LinearLayout>(Resource.Id.phototrayholder);

            // this mimics the bundle data for testing purposes
            List<string> bundleData = new List<string>();
            for (int j = 0; j < 20; j++)
            {
                bundleData.Add(gload.gridItems[j].filestring);
            }
            
            // async operation to implement photos to the top view
            for (int i = 0; i < bundleData.Count; i++)
            {
                ImageView img = new ImageView(this);
                photoTray.AddView(img);
                new SimpleImageScalerTask(img).Execute(bundleData[i]);
            }

            //ArrayAdapter adapter = new ArrayAdapter(this, Resource.Layout.PicSelectGridViewChildLayout, data);
            
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
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
        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);

            List<string> list = (List<string>)bundle.GetStringArrayList("list");

            SetContentView(Resource.Layout.Record);

            ImageView img = FindViewById<ImageView>(Resource.Id.RecordImage);

            img.SetImageBitmap(BitmapFactory.DecodeFile(list[2]));
        }
    }
}
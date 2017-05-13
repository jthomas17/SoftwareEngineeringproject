using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            SetContentView(Resource.Layout.Record); 
            
            if(savedInstanceState == null)
            {
                var video = new Video();
                var ft = FragmentManager.BeginTransaction();
                ft.Add(Resource.Id.fragment_container, video);
                ft.Commit();

            }
        }
    }
}
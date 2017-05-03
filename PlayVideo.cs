
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Swaha_for_Android
{
	[Activity(Label = "PlayVideo")]
	public class PlayVideo : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			// Create your application here

			SetContentView(Resource.Layout.PlayVideo);

			//Button PlayButton 
			//Button SaveVideo
			//Button DiscardVideo


			var videoView = FindViewById<VideoView>(XmlResourceParserReader.Id.SampleVideoView);

			var uri = Android.Net.Uri.Parse (/* insert url*/)

			videoView.SetVideoURI(uri);
						videoView.Visibility = ViewStates.Visible;


			videoView.Start();
		}
	}
}

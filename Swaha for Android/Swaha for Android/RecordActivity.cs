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
        private static int CAST_PERMISSION_CODE = 22;
        public DisplayMetrics displayMetric = new DisplayMetrics();
        public MediaProjection mediaProjection;
        public VirtualDisplay virtualDisplay;
        public MediaRecorder mediaRecorder;
        public MediaProjectionManager mediaProjectionManager;

        public Button start;
        public Button stop;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Record);

            start = FindViewById<Button>(Resource.Id.start);
            stop = FindViewById<Button>(Resource.Id.stop);

            mediaRecorder = new MediaRecorder();
            mediaProjectionManager = (MediaProjectionManager)GetSystemService(Context.MediaProjectionService);
            var metrics = Resources.DisplayMetrics;


            start.Click += delegate
            {
                startRecording();
            };
            stop.Click += delegate
            {
                stopRecording();
            };

        }

        private void startRecording()
        {
            prepareRecording();

            if (mediaProjection == null)
            {
                StartActivityForResult(mediaProjectionManager.CreateScreenCaptureIntent(), CAST_PERMISSION_CODE);
                return;
            }
            var metrics = Resources.DisplayMetrics;
            int screenDensity = (int)metrics.Density;
            virtualDisplay = mediaProjection.CreateVirtualDisplay("ScreenCapture", metrics.WidthPixels, metrics.HeightPixels, screenDensity, (DisplayFlags)VirtualDisplayFlags.AutoMirror, mediaRecorder.Surface, null, null);

            mediaRecorder.Start();
        }

        //}
        private void stopRecording()
        {
            if (virtualDisplay == null)
                return;
            virtualDisplay.Release();
            virtualDisplay = null;
            mediaRecorder.Stop();
            mediaRecorder.Reset();
            mediaRecorder.Release();
            mediaRecorder.Prepare();
            //prepareRecording();
        }

        //public override void onAcitvityResult
        void prepareRecording()
        {
            string directory = "/My phone/Pictures/Swaha_for_Android";
            File folder = new File(directory, "test.mp4"); //need to fix later
            bool success = true;
            folder.Mkdir();
            //if (!folder.Exists())
            //{
            //    folder.Mkdir();
            //    success = folder.Exists();
            //    Toast.MakeText(this, "We have gotten into the folder", ToastLength.Long).Show();
            //}
            string filePath;
            if (success)
            {
                string videoName = ("Test.mp4");
                filePath = directory + File.Separator + videoName;
                Toast.MakeText(this, "filepath is " + filePath, ToastLength.Long).Show();

            }
            else
            {
                Toast.MakeText(this, "Fail to create recordings directory", ToastLength.Short);
                return;
            }
            var metric = Resources.DisplayMetrics;

            mediaRecorder.SetAudioSource(AudioSource.Mic);
            mediaRecorder.SetVideoSource(VideoSource.Surface);
            mediaRecorder.SetOutputFormat(OutputFormat.Mpeg4);
            mediaRecorder.SetVideoEncoder(VideoEncoder.H264);
            mediaRecorder.SetAudioEncoder(AudioEncoder.AmrNb);
            mediaRecorder.SetVideoFrameRate(30);
            mediaRecorder.SetVideoSize(metric.WidthPixels, metric.HeightPixels); //when we are ready this is where we will change to only record what we want
            mediaRecorder.SetOutputFile(filePath);
            mediaRecorder.SetVideoEncodingBitRate(512 * 1000);
            mediaRecorder.Prepare();
        }
    }
}
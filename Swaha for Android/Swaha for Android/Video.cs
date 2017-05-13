using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware.Display;
using Android.Media.Projection;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace Swaha_for_Android
{
    public class Video : Fragment
    {
        private const string TAG = "ScreenCaptureFragment";

        private const string STATE_RESULT_CODE = "result_code";
        private const string STATE_RESULT_DATA = "result_data";

        private const int REQUEST_MEDIA_PROJECTION = 1;

        private int screenDensity;

        private int resultCode;
        private Intent resultData;

        private Surface surface;
        private MediaProjection mediaProjection;
        private VirtualDisplay virtualDisplay;
        private MediaProjectionManager mediaProjectionManager;
        private Button buttonToggle;
        private SurfaceView surfaceView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (savedInstanceState != null)
            {
                resultCode = savedInstanceState.GetInt(STATE_RESULT_CODE);
                resultData = (Intent)savedInstanceState.GetParcelable(STATE_RESULT_DATA);
            }
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            return inflater.Inflate(Resource.Layout.videofragment, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            surfaceView = view.FindViewById<SurfaceView>(Resource.Id.surface);
            surface = surfaceView.Holder.Surface;
            buttonToggle = view.FindViewById<Button>(Resource.Id.toggle);
            buttonToggle.Click += delegate (object sender, EventArgs e)
            {
                if (virtualDisplay == null)
                    StartScreenCapture();
                else
                    StopScreenCapture();
            };
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            var metrics = new DisplayMetrics();
            Activity.WindowManager.DefaultDisplay.GetMetrics(metrics);
            screenDensity = (int)metrics.DensityDpi;
            mediaProjectionManager = (MediaProjectionManager)Activity.GetSystemService(Context.MediaProjectionService);
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            if (resultData != null)
            {
                outState.PutInt(STATE_RESULT_CODE, resultCode);
                outState.PutParcelable(STATE_RESULT_DATA, resultData);

            }
        }

        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == REQUEST_MEDIA_PROJECTION)
            {
                if (resultCode != Result.Ok)
                {
                    Toast.MakeText(Activity, "USER CANCELED", ToastLength.Short).Show();
                    return;
                }
                if (Activity == null)
                    return;
                this.resultCode = (int)resultCode;
                this.resultData = data;
                SetUpMediaProjection();
                SetUpVirtualDisplay();
            }
        }

        public override void OnPause()
        {
            base.OnPause();
            StopScreenCapture();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            TearDownMediaProjection();
        }

        public void SetUpMediaProjection()
        {
            mediaProjection = mediaProjectionManager.GetMediaProjection(resultCode, resultData);
        }

        private void TearDownMediaProjection()
        {
            if(mediaProjection != null)
            {
                mediaProjection.Stop();
                mediaProjection = null;
            }

        }

        private void StartScreenCapture()
        {
            if (surface == null || Activity == null)
                return;
            if (mediaProjection != null)
            {
                SetUpVirtualDisplay();
            }
            else if (resultCode != 0 && resultData != null)
            {
                SetUpMediaProjection();
                SetUpVirtualDisplay();
            }
            else
            {
                // This initiates a prompt for the user to confirm screen projection.
                StartActivityForResult(mediaProjectionManager.CreateScreenCaptureIntent(), REQUEST_MEDIA_PROJECTION);
            }
        }

        private void SetUpVirtualDisplay()
        {
            virtualDisplay = mediaProjection.CreateVirtualDisplay("ScreenCapture",
                surfaceView.Width, surfaceView.Height, screenDensity,
                (DisplayFlags)VirtualDisplayFlags.AutoMirror, surface, null, null);
            buttonToggle.Text = "stop";
        }
        private void StopScreenCapture()
        {
            if (virtualDisplay == null)
                return;

            virtualDisplay.Release();
            virtualDisplay = null;
            buttonToggle.Text = "start";
        }
    }
}
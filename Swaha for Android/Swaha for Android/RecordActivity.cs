using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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
        IList<string> bundledList; // Holds the selected pictures by the user
        
        List<ImageView> storyImageList; // Holds a collection of imageview references for assigning delegates
        
        IList<int> times;
        IList<int> ImageIndexes;
        Stopwatch stopwatch;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();

            SetContentView(Resource.Layout.RecordScreen);
            storyImageList = new List<ImageView>(10);
            times = new List<int>();
            ImageIndexes = new List<int>();
            stopwatch = new Stopwatch();
            bool restart = false;

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

            Button Start = FindViewById<Button>(Resource.Id.start);
            Start.Click += delegate
            {
                /* TODO:
                this function will set conditions to:
                    Change graphics to focus on the changing image
                    Change the name of this button to "Stop"
                    Start/stop the timer

                start time
                each delegate checks condition; if true, add photo/create timestamp 
                */
                if (restart == false)
                {
                    stopwatch.Start();
                    Start.Text = "Stop";
                    restart = true;
                }
                else {
                    stopwatch.Stop();
                    Start.Text = "Start";
                    restart = false;
                }
                    

            };

            // retrieve picture file data from bundle
            bundledList = this.Intent.Extras.GetStringArrayList("story");

            // async operation to implement photos to the top view
            // also adds views to the storyImageList for delegates
            for (int i = 0; i < bundledList.Count; i++)
            {
                
                ImageView img = new ImageView(this);
                
                photoTray.AddView(img);
                storyImageList.Add(img);
                new SimpleImageScalerTask(img).Execute(bundledList[i]);
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
            
            // Yes, it's horrible code, but we couldn't figure out how to 
            // assign delegates programatically. So here's a shitton of 
            // delegates, each referencing an imageview that may or may not
            // be showing.


            storyImageList[0].Click += delegate
            {
                addStoryInterval(0);
                storyImage.SetImageURI(Uri.Parse(bundledList[0]));
                //string hardpath = "/My Phone/DCIM/Neat Photos/20170224_140502.jpg";
                //storyImage.SetImageBitmap(BitmapFactory.DecodeFile(hardpath));
            };
            storyImageList[1].Click += delegate
            {
                addStoryInterval(1);
                storyImage.SetImageURI(Uri.Parse(bundledList[1]));
            };
            storyImageList[2].Click += delegate
            {
                addStoryInterval(2);
                storyImage.SetImageURI(Uri.Parse(bundledList[2]));
            };
            storyImageList[3].Click += delegate
            {
                addStoryInterval(3);
                storyImage.SetImageURI(Uri.Parse(bundledList[3]));
            };
            storyImageList[4].Click += delegate
            {
                addStoryInterval(4);
                storyImage.SetImageURI(Uri.Parse(bundledList[4]));
            };
            storyImageList[5].Click += delegate
            {
                addStoryInterval(5);
                storyImage.SetImageURI(Uri.Parse(bundledList[5]));
            };
            storyImageList[6].Click += delegate
            {
                addStoryInterval(6);
                storyImage.SetImageURI(Uri.Parse(bundledList[6]));
            };
            storyImageList[7].Click += delegate
            {
                addStoryInterval(7);
                storyImage.SetImageURI(Uri.Parse(bundledList[7]));
            };
            storyImageList[8].Click += delegate
            {
                addStoryInterval(8);
                storyImage.SetImageURI(Uri.Parse(bundledList[8]));
            };
            storyImageList[9].Click += delegate
            {
                addStoryInterval(9);
                storyImage.SetImageURI(Uri.Parse(bundledList[9]));
            };

            // go to next activity, Preview video
            Button toPreview = FindViewById<Button>(Resource.Id.stop);
            toPreview.Click += delegate
            {
                var path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                var filename = System.IO.Path.Combine(path.ToString(), "times.txt");

                var streamWriter = new System.IO.StreamWriter(filename, true);

                for(int i = 0; i < times.Count; i++)
                {
                    if (i == 0) {
                        streamWriter.WriteLine(times[i]);
                    }
                    else
                        streamWriter.WriteLine(times[i] - times[i-1]);
                }
                    var bitfilename = System.IO.Path.Combine(path.ToString(), "s1.png");
                    var stream = new System.IO.FileStream(bitfilename, System.IO.FileMode.Create);
                    Bitmap bitmap = BitmapFactory.DecodeFile(bundledList[0]);
                    bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
                    stream.Close();
                    bitmap = null;

                System.Net.WebClient Client = new System.Net.WebClient();
                Client.Headers.Add("Content-Type", "binary/octet-stream");
                //byte[] result = Client.UploadFile("http://cs1.whitworth.edu/swaha/index.php", "POST", bundledList[0]);
                try {
                    /* NEED TO:
                    Compress photos so that server will accept them < 1MB
                    */

                    

                    byte[] photoresult = Client.UploadFile("http://cs1.whitworth.edu/swaha/index.php", "POST", @bitfilename);
                    string s = System.Text.Encoding.UTF8.GetString(photoresult, 0, photoresult.Length);
                    Toast.MakeText(this, s, ToastLength.Long).Show();


                    // For the text file of times

                    byte[] textresult = Client.UploadFile("http://cs1.whitworth.edu/swaha/index.php", "POST", @filename);
                    string t = System.Text.Encoding.UTF8.GetString(textresult, 0, textresult.Length);
                    Toast.MakeText(this, t, ToastLength.Long).Show();
                    
                }
                catch (System.Exception e) { Toast.MakeText(this, e.Message, ToastLength.Long).Show(); }
                
                
                //var PreviewIntent = new Intent(this, typeof(Preview));
                //StartActivity(PreviewIntent);
            };
        }

        public void handleImageClick(int p, ImageView img)
        {
            addStoryInterval(p);
            img.SetImageURI(Uri.Parse(bundledList[p]));
        }

        public void addStoryInterval(int imagepos)
        {
            if (stopwatch.IsRunning)
            {
                times.Add((int)stopwatch.ElapsedMilliseconds);
                ImageIndexes.Add(imagepos);
            }
        }
        
    }
}
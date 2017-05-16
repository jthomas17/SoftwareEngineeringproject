using System.Collections.Generic;
using System.Linq;

using Android.Database;
using Android.Provider;
using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Android.Graphics.Drawables;

namespace Swaha_for_Android
{
    class GridViewAdapter : BaseAdapter<UserImage>
    {
        Context _context;
        Activity _activity;
        GridItemLoader _gridItemLoader;

        /*
        public GridViewAdapter(Activity activity, GridItemLoader gridItemLoader)
        {
            _activity = activity;
            _gridItemLoader = gridItemLoader;
        }
        */
        public GridViewAdapter(Context context, GridItemLoader gridItemLoader)
        {
            _context = context;
            _gridItemLoader = gridItemLoader;
        }

        public override int Count
        {
            get { return _gridItemLoader.gridItems.Count; }
        }

        public override UserImage this[int position]
        {
            get { return _gridItemLoader.gridItems[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.PicSelectGridViewChildLayout, parent, false);
            var image = view.FindViewById<ImageView>(Resource.Id.gridImage);

            //image.SetScaleType(ImageView.ScaleType.CenterCrop);
            //image.SetImageURI(imageList[position].uri);
            //BitmapFactory.Options options = await GetBitmapOptionsOfImageAsync(_gridItemLoader.gridItems[position].filestring);

            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InSampleSize = 8;
            Bitmap bitmap = BitmapFactory.DecodeFile(_gridItemLoader.gridItems[position].filestring, options);
            //image.SetImageURI(_gridItemLoader.gridItems[position].thumburi);
            image.SetImageBitmap(bitmap);
            //bitmap.Recycle();
            return view;
        }
        
    }
}
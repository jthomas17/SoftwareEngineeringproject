using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
        GridItemLoader _gridItemLoader;
        
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
            var image = view.FindViewById<ImageButton>(Resource.Id.cellImage);
            new ImageScalerTask(image).Execute(_gridItemLoader.gridItems[position].filestring);

            return image;

            /*------------------------------------------------------------------------------------------------------------------------*/

            //View itemView = convertView;
            //ViewHolder viewHolder;

            //if (itemView != null)
            //{
            //    viewHolder = (ViewHolder)itemView.Tag;
            //}
            //else   /* If convertView is null */
            //{
            //    viewHolder = new ViewHolder();
            //    itemView = LayoutInflater.From(_context).Inflate(Resource.Layout.PicSelectGridViewChildLayout, parent, false);

            //    ImageButton imgThumbnail = itemView.FindViewById<ImageButton>(Resource.Id.cellImage);
            //    imgThumbnail.SetScaleType(ImageButton.ScaleType.CenterCrop);
            //    //imgThumbnail.SetImageResource(Resource.Drawable.Icon);

            //    viewHolder.Thumbnail = imgThumbnail;
            //    itemView.Tag = viewHolder;

            //}

            /* TODO: async function that scales the images from the filepath goes here 
                if (holder.imageView != null){new ImageScalerTask(imagefilepath)}
            */

            //if (viewHolder.Thumbnail != null)
            //{
            //    new ImageScalerTask(viewHolder.Thumbnail).Execute(_gridItemLoader.gridItems[position].filestring);
            //}
            //return itemView;

        }
        
        public class ViewHolder : Java.Lang.Object
        {
            public ImageButton Thumbnail { get; set; }
        }
    }
}
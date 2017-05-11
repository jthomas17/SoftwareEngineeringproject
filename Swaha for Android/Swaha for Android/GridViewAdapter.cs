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
            /*------------------------------------------------------------------------------------------------------------------------*/

            View itemView = convertView;
            ViewHolder holder;

            if (itemView != null)
            {
                holder = (ViewHolder)itemView.Tag;
                holder.IsRecycled = true;
            }
            else   /* If convertView is null */
            {
                holder = new ViewHolder();
                holder.IsRecycled = false;
                itemView = LayoutInflater.From(_context).Inflate(Resource.Layout.PicSelectGridViewChildLayout, parent, false);

                // need to set the scale type before assinging it to the viewholder
                ImageButton imgThumbnail = itemView.FindViewById<ImageButton>(Resource.Id.cellImage);
                imgThumbnail.SetScaleType(ImageButton.ScaleType.CenterCrop);
                
                holder.Thumbnail = imgThumbnail;
                itemView.Tag = holder;
            }
            holder.Thumbnail.SetImageResource(Resource.Drawable.Icon);
            /* TODO: async function that scales the images from the filepath goes here 
                if (holder.imageView != null){new ImageScalerTask(imagefilepath)}
            */
            holder.Position = position;

            new ImageScalerTask(holder, position).Execute(_gridItemLoader.gridItems[position].filestring);
            
            return itemView;
        }

        public class ViewHolder : Java.Lang.Object
        {
            public ImageButton Thumbnail { get; set; }
            public int Position { get; set; }
            public bool IsRecycled { get; set; }
        }
    }
}
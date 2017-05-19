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

// !!!!! THIS USED TO USE A GENERIC LIST, NOW USES ILIST
namespace Swaha_for_Android
{
    class GridViewAdapter : BaseAdapter<string>
    {
        Context _context;
        IList<string> _list;

        public GridViewAdapter(Context c, IList<string> list)
        {
            _context = c;
            _list = list;
        }
        public override int Count
        {
            get { return _list.Count; }
        }
        public override string this[int position]
        {
            get {return _list[position]; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View itemView = convertView;
            ViewHolder holder;

            if (itemView != null)
            {
                holder = (ViewHolder)itemView.Tag;

            }
            else   /* If convertView is null */
            {
                holder = new ViewHolder();

                itemView = LayoutInflater.From(_context).Inflate(Resource.Layout.GridTile, parent, false);

                // need to set the scale type before assinging it to the viewholder
                ImageView imgThumbnail = itemView.FindViewById<ImageView>(Resource.Id.gridImage);
                imgThumbnail.SetScaleType(ImageView.ScaleType.CenterCrop);
                imgThumbnail.Rotation = 90;
                holder.Thumbnail = imgThumbnail;
                itemView.Tag = holder;
            }
            
            holder.Position = position;
            new ImageScalerTask(holder, position).Execute(_list[position]);
    

            return itemView;
        }

        public class ViewHolder : Java.Lang.Object
        {
            public ImageView Thumbnail { get; set; }
            public int Position { get; set; }
        }
    }
}
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
    class UserImage
    {
        private Android.Net.Uri imageDefinition;
        private int imageId;
        private string imagePathString;
      
        public UserImage(Android.Net.Uri path, int id, string fileString)
        {
            imageDefinition = path;
            imageId = id;
            imagePathString = fileString;
        }

        public int id
        {
            get { return imageId; }
        }

        public Android.Net.Uri uri
        {
            get { return imageDefinition; }
        }
        public string filestring
        {
            get { return imagePathString; }
        }
    }

}
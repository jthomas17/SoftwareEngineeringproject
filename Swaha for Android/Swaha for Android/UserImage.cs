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
        private string thumbnailPathString;
        private Android.Net.Uri thumbnailPathUri;
        public UserImage(Android.Net.Uri path, int id, string fileString, Android.Net.Uri thumbUri, string tpath)
        {
            imageDefinition = path;
            imageId = id;
            imagePathString = fileString;
            thumbnailPathString = tpath;
            thumbnailPathUri = thumbUri;
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
        public string thumbstring
        {
            get { return thumbnailPathString; }
        }
        public Android.Net.Uri thumburi
        {
            get { return thumbnailPathUri; }
        }
    }

}
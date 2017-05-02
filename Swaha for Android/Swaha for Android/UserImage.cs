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

        public int GetId()
        {
            return imageId;
        }

        public Android.Net.Uri GetUri()
        {
            return imageDefinition;
        }
        public string GetFileString()
        {
            return imagePathString;
        }
    }

}
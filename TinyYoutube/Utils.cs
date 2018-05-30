using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Net;

namespace TinyYoutube
{
    class Utils
    {
        #region Variables

        static WebClient wc = new WebClient();

        #endregion

        #region Main Functions

        public static Image readImageFromUrl(string url)
        {
            byte[] bytes = wc.DownloadData(url);
            MemoryStream ms = new MemoryStream(bytes);
            return Image.FromStream(ms);
        }

        #endregion
    }
}

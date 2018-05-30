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

        public const string VIDEO_PARAM = "?autoplay=1&cc_load_policy=1&controls=0&disablekb=1&fs=0&iv_load_policy=3&modestbranding=1&rel=0&playsinline=1";

        public const string VIDEO_URL = "https://www.youtube.com/embed/%URL%?autoplay=1&cc_load_policy=1&controls=0&disablekb=1&fs=0&iv_load_policy=3&modestbranding=1&rel=0&playsinline=1";

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

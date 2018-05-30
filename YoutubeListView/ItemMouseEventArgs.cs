using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeListView
{
    public class ItemMouseEventArgs : MouseEventArgs
    {
        int index;
        string url;

        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                this.index = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                this.url = value;
            }
        }

        public ItemMouseEventArgs(MouseButtons buttons, int click, int x, int y, int delta) : base(buttons, click, x, y, delta)
        {
            this.index = index;
        }
    }
}

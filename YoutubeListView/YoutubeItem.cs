using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeListView
{
    public partial class YoutubeItem : UserControl
    {
        #region Variables

        public new event EventHandler DoubleClick = null;
        public new event MouseEventHandler MouseUp = null;

        private int index;
        private string url;
        private bool selected = false;

        public System.Drawing.Image Image
        {
            get
            {
                return this.previewer.Image;
            }
            set
            {
                this.previewer.Image = value;
            }
        }

        public string Title
        {
            get
            {
                return titleLabel.Text;
            }
            set
            {
                titleLabel.Text = value;
            }
        }

        public string Description
        {
            get
            {
                return descLabel.Text;
            }
            set
            {
                descLabel.Text = value;
            }
        }

        public string Duration
        {
            get
            {
                return durationLabel.Text;
            }
            set
            {
                durationLabel.Text = value;
            }
        }


        public int Index
        {
            get
            {
                return this.index;
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
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                if (this.selected == true)
                {
                    
                }
                else
                {
                    
                }
            }
        }

        #endregion

        #region Item Initialization

        public YoutubeItem()
        {
            InitializeComponent();

            InitializeMyComponent();
        }

        public YoutubeItem(Boolean hasImage)
        {
            InitializeComponent();

            InitializeMyComponent();

            if (!hasImage)
            {
                this.previewer.Width = 0;
            }
        }

        private void InitializeMyComponent()
        {
            this.titleLabel.MouseUp += YoutubeItem_MouseUp;
            this.descLabel.MouseUp += YoutubeItem_MouseUp;
            this.previewer.MouseUp += YoutubeItem_MouseUp;
        }

        #endregion

        #region Handlers

        private void YoutubeItem_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClick != null)
            {
                DoubleClick(sender, e);
            }
        }

        private void YoutubeItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseUp != null)
            {
                ItemMouseEventArgs me = new ItemMouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta);
                me.Index = this.index;
                me.Url = this.url;
                MouseUp(sender, me);
            }
        }

        #endregion
    }
}

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

        public YoutubeItem()
        {
            InitializeComponent();

            InitializeMyComponent();
        }

        private void InitializeMyComponent()
        {
            base.MouseUp += YoutubeItem_MouseUp;
            base.DoubleClick += YoutubeItem_DoubleClick;
        }

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
                MouseUp(sender, e);
            }
        }
    }
}

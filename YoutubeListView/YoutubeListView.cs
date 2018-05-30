using System;
using System.Collections;
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
    public partial class YoutubeListView: UserControl
    {
        #region Variables 

        public new event EventHandler DoubleClick = null;
        public new event MouseEventHandler MouseUp = null;

        private ArrayList controlList;
        private int itemWidth = 200;
        private int itemHeight = 30;
        private Size actualListSize;
        private YoutubeItem previousSelectedItem = null;

        public YoutubeItem this[int index]
        {
            get
            {
                return (YoutubeItem) controlList[index];
            }
            set
            {
                controlList[index] = value;
            }
        }

        public YoutubeItem SelectedItem
        {
            get
            {
                return previousSelectedItem;
            }
        }

        public int HeightToShowAll
        {
            get
            {
                System.Drawing.Point p = GetItemLocation(controlList.Count);
                return p.Y + this.itemHeight;
            }
        }

        #endregion

        #region List Initialization

        public YoutubeListView()
        {
            InitializeComponent();

            controlList = new ArrayList();

            InitializeMyComponent();
        }

        #endregion

        private void InitializeMyComponent()
        {
            base.DoubleClick += YoutubeListView_DoubleClick;
            base.MouseUp += YoutubeListView_MouseUp;
            base.MouseWheel += MainPanel_MouseWheel;
            base.SizeChanged += YoutubeListView_SizeChanged;
        }

        private void YoutubeListView_SizeChanged(object sender, EventArgs e)
        {
            actualListSize = new Size(this.Width, this.Height);
            this.itemWidth = actualListSize.Width;
            this.Height = actualListSize.Height;
            this.mainPanel.Height = actualListSize.Height;
        }

        private void MainPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            int pace = 15 * e.Delta / Math.Abs(e.Delta);

            if (mainPanel.Location.Y + pace > 0 ||
                mainPanel.Location.Y + pace < this.Height - mainPanel.Height)
            {
                return;
            }
            mainPanel.Location = new Point(mainPanel.Location.X, mainPanel.Location.Y + pace);
        }

        private void YoutubeListView_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClick != null)
            {
                DoubleClick(sender, e);
            }
        }

        private void YoutubeListView_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseUp != null)
            {
                MouseUp(sender, e);
            }
        }

        public void Remove(YoutubeItem value)
        {
            controlList.Remove(value);
            this.mainPanel.Controls.Remove(value);
            ReCalculateItems();
        }

        public bool Contains(YoutubeItem value)
        {
            if (this.SelectedItem == value)
            {
                value.Selected = false;
            }
            return controlList.Contains(value);
        }

        public void Clear()
        {
            controlList.Clear();
            this.mainPanel.Controls.Clear();
            ReCalculateItems();
        }

        public int IndexOf(YoutubeItem value)
        {
            return controlList.IndexOf(value);
        }

        public int Add(YoutubeItem value)
        {
            YoutubeItem lItem = (YoutubeItem) value;
            PrepareItemToAdd(lItem);
            this.mainPanel.Controls.Add(lItem);
            int i = controlList.Add(lItem);
            ReCalculateItems();
            return i;
        }

        public int Count
        {
            get
            {
                return controlList.Count;
            }
        }

        public void ReCalculateItems()
        {
            YoutubeItem lItem;
            for (int i = 0; i < controlList.Count; i++)
            {
                lItem = (YoutubeItem) controlList[i];
                lItem.Location = GetItemLocation(i);
            }
            this.mainPanel.Height = this.HeightToShowAll;
            this.mainPanel.Location = new Point(0, 0);
            this.ResumeLayout();
        }

        private Point GetItemLocation(int index)
        {
            int ItemPerRow = (this.Width - 20) / this.itemWidth;
            if (ItemPerRow == 0)
            {
                ItemPerRow = 1;
            }
            int rowIndex = index / ItemPerRow;
            int colIndex = index - rowIndex * ItemPerRow;
            Point p = new Point(0, index * this.itemHeight);
            return p;
        }

        private void PrepareItemToAdd(YoutubeItem lItem)
        {
            lItem.MouseUp += YoutubeListView_MouseUp;
            lItem.DoubleClick += YoutubeListView_DoubleClick;
            lItem.Size = new System.Drawing.Size(this.itemWidth, this.itemHeight);
            lItem.Selected = false;
        }
    }
}

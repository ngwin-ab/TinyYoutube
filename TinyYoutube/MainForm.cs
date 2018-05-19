using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinyYoutube.Youtube;

namespace TinyYoutube
{
    public partial class MainForm : Form
    {
        #region Variables

        YoutubeSearch searcher;

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        #region Main Form

        private void MainForm_Load(object sender, EventArgs e)
        {
            var screen = Screen.FromPoint(this.Location);
            this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);

            searcher = new YoutubeSearch();
            searcher.VideoUpdated += videoListUpdated;
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        #endregion

        private async void searchText_TextChangedAsync(object sender, EventArgs e)
        {
            var searchTextStr = searchText.Text.Trim().ToLower();
            await searcher.search(searchTextStr, 20);
        }

        void videoListUpdated(List<VideoInfo> videos)
        {
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

using TinyYoutube.Youtube;
using YoutubeListView;

namespace TinyYoutube
{
    public partial class MainForm : Form
    {
        #region Variables

        YoutubeSearch searcher;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        long lastTextTime = 0;
        string videoId = "";
        BackgroundWorker worker;

        #endregion

        #region Form Initialization

        public MainForm()
        {
            SetBrowserFeatureControl();

            InitializeComponent();
        }

        /// <summary>
        /// This function is to allow embedded Youtube video to be displayed in an window app
        /// </summary>
        private void SetBrowserFeatureControl()
        {
            // http://msdn.microsoft.com/en-us/library/ee330720(v=vs.85).aspx

            // FeatureControl settings are per-process
            var fileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

            // make the control is not running inside Visual Studio Designer
            if (String.Compare(fileName, "devenv.exe", true) == 0 || String.Compare(fileName, "XDesProc.exe", true) == 0)
                return;

            SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, GetBrowserEmulationMode()); // Webpages containing standards-based !DOCTYPE directives are displayed in IE10 Standards mode.
            SetBrowserFeatureControlKey("FEATURE_AJAX_CONNECTIONEVENTS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_MANAGE_SCRIPT_CIRCULAR_REFS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_DOMSTORAGE ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_GPU_RENDERING ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_IVIEWOBJECTDRAW_DMLT9_WITH_GDI  ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_LEGACY_COMPRESSION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_LOCALMACHINE_LOCKDOWN", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_OBJECT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_SCRIPT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_NAVIGATION_SOUNDS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SCRIPTURL_MITIGATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SPELLCHECKING", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_STATUS_BAR_THROTTLING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_TABBED_BROWSING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_VALIDATE_NAVIGATE_URL", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_DOCUMENT_ZOOM", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_POPUPMANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_MOVESIZECHILD", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ADDON_MANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBSOCKET", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WINDOW_RESTRICTIONS ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_XMLHTTP", fileName, 1);
        }

        private void SetBrowserFeatureControlKey(string feature, string appName, uint value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(
                String.Concat(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\", feature),
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(appName, (UInt32)value, RegistryValueKind.DWord);
            }
        }

        private UInt32 GetBrowserEmulationMode()
        {
            int browserVersion = 7;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. Default value for Internet Explorer 11.
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. Default value for applications hosting the WebBrowser Control.
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. Default value for Internet Explorer 8
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. Default value for Internet Explorer 9.
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode. Default value for Internet Explorer 10.
                    break;
                default:
                    // use IE11 mode by default
                    break;
            }

            return mode;
        }

        #endregion

        #region Main Form Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            var screen = Screen.FromPoint(this.Location);
            this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);

            // setup the search and events when video list and thumb images 
            // are updated
            searcher = new YoutubeSearch();
            searcher.VideoUpdated += videoListUpdated;
            searcher.ImageUpdated += imageUpdated;
            searcher.CommentUpdated += commentUpdated;

            // detects when mouse is clicked over the surface of 
            // the web browser
            viewer.DocumentCompleted += Viewer_DocumentCompleted;
            
            SendMessage(this.searchText.Handle, 0x1501, 1, "keywords");

            // start the clock to wait for the searching
            runSearchThread();
        }

        private void Viewer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (viewer.Document != null)
            {
                viewer.Document.Click += Document_Click; ;
            }
        }

        private void Document_Click(object sender, HtmlElementEventArgs e)
        {
            this.y2bList.Visible = false;
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            try
            {
                this.Opacity = 0.5;
            }
            catch { }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void closeLabel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void commentLabel_Click(object sender, EventArgs e)
        {
            // load comments
            runCommentSearchThread();
        }

        #endregion

        #region Search Handler

        /// <summary>
        /// to start the clock, this clock is to check whether 
        /// user stops editing 
        /// </summary>
        private void runSearchThread()
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
        }

        private async void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var searchTextStr = searchText.Text.Trim().ToLower();
            await searcher.search(searchTextStr, 20);
        }
        
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                double secDiff = new TimeSpan(DateTime.Now.Ticks - lastTextTime).TotalSeconds;
                if (lastTextTime > 0 && secDiff >= 1)
                {
                    worker.ReportProgress(0);
                    lastTextTime = 0;
                }
                Thread.Sleep(500);
            }
        }

        async void runCommentSearchThread()
        {
            if (!this.videoId.Equals(""))
            {
                await searcher.getComments(this.videoId, 50);
            }
        }

        private void searchText_TextChangedAsync(object sender, EventArgs e)
        {
            lastTextTime = DateTime.Now.Ticks;
        }

        async void videoListUpdated(List<VideoInfo> videos)
        {
            y2bList.Clear();
            foreach (VideoInfo info in videos)
            {
                YoutubeItem item = new YoutubeItem();
                item.Title = info.Title;
                item.Description = info.Description;
                item.Duration = info.PublishedAt; // info.Duration;
                item.Url = info.Id;
                
                // load the thumb image in async mode
                await searcher.readImageFromUrl(info.Thumbnail, item);
                y2bList.Add(item);
            }
            y2bList.Visible = true;
        }

        async void imageUpdated(Image srcImage, YoutubeItem item)
        {
            item.Image = srcImage;
        }

        async void commentUpdated(List<Comment> comments)
        {
            y2bList.Clear();
            foreach (Comment cmt in comments)
            {
                YoutubeItem item = new YoutubeItem(false);
                item.Title = cmt.authorName;
                item.Description = cmt.comment;
                item.Duration = cmt.time;
                y2bList.Add(item);
            }
            y2bList.Visible = true;
        }

        private void y2bList_MouseUp(object sender, MouseEventArgs e)
        {
            y2bList.Visible = false;
            ItemMouseEventArgs me = (ItemMouseEventArgs)e;
            this.videoId = me.Url;
            string videoUrl = Utils.VIDEO_URL.Replace("%VIDEO_ID%", this.videoId);
            loadUrl(videoUrl);
        }

        private void loadUrl(string url)
        {
            // this flag is to play the licensed songs
            string additionalHeaders = "Referer : http://youtube.com";
            viewer.Navigate(new Uri(url, UriKind.Absolute), "", null, additionalHeaders);
            // viewer.Url = new Uri(videoUrl, UriKind.Absolute);
        }

        #endregion

        
    }
}

namespace TinyYoutube
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.searchText = new System.Windows.Forms.TextBox();
            this.viewer = new System.Windows.Forms.WebBrowser();
            this.closeLabel = new System.Windows.Forms.Label();
            this.y2bList = new YoutubeListView.YoutubeListView();
            this.commentLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchText
            // 
            this.searchText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.searchText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchText.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchText.Location = new System.Drawing.Point(0, 0);
            this.searchText.Margin = new System.Windows.Forms.Padding(5);
            this.searchText.Multiline = true;
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(360, 20);
            this.searchText.TabIndex = 0;
            this.searchText.TextChanged += new System.EventHandler(this.searchText_TextChangedAsync);
            // 
            // viewer
            // 
            this.viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer.Location = new System.Drawing.Point(0, 20);
            this.viewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.viewer.Name = "viewer";
            this.viewer.ScrollBarsEnabled = false;
            this.viewer.Size = new System.Drawing.Size(360, 200);
            this.viewer.TabIndex = 1;
            // 
            // closeLabel
            // 
            this.closeLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeLabel.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeLabel.Image = global::TinyYoutube.Properties.Resources.icons8_multiply_16;
            this.closeLabel.Location = new System.Drawing.Point(344, 2);
            this.closeLabel.Name = "closeLabel";
            this.closeLabel.Size = new System.Drawing.Size(13, 15);
            this.closeLabel.TabIndex = 3;
            this.closeLabel.Click += new System.EventHandler(this.closeLabel_Click);
            // 
            // y2bList
            // 
            this.y2bList.BackColor = System.Drawing.Color.White;
            this.y2bList.Dock = System.Windows.Forms.DockStyle.Left;
            this.y2bList.Location = new System.Drawing.Point(0, 20);
            this.y2bList.Name = "y2bList";
            this.y2bList.Size = new System.Drawing.Size(280, 200);
            this.y2bList.TabIndex = 2;
            this.y2bList.Visible = false;
            this.y2bList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.y2bList_MouseUp);
            // 
            // commentLabel
            // 
            this.commentLabel.Image = global::TinyYoutube.Properties.Resources.icons8_topic_16;
            this.commentLabel.Location = new System.Drawing.Point(321, 1);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(18, 18);
            this.commentLabel.TabIndex = 7;
            this.commentLabel.Click += new System.EventHandler(this.commentLabel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 220);
            this.ControlBox = false;
            this.Controls.Add(this.commentLabel);
            this.Controls.Add(this.closeLabel);
            this.Controls.Add(this.y2bList);
            this.Controls.Add(this.viewer);
            this.Controls.Add(this.searchText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.WebBrowser viewer;
        private YoutubeListView.YoutubeListView y2bList;
        private System.Windows.Forms.Label closeLabel;
        private System.Windows.Forms.Label commentLabel;
    }
}


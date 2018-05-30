namespace YoutubeListView
{
    partial class YoutubeItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.previewer = new System.Windows.Forms.PictureBox();
            this.descLabel = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.previewer)).BeginInit();
            this.SuspendLayout();
            // 
            // previewer
            // 
            this.previewer.Dock = System.Windows.Forms.DockStyle.Left;
            this.previewer.Location = new System.Drawing.Point(0, 0);
            this.previewer.Name = "previewer";
            this.previewer.Size = new System.Drawing.Size(50, 30);
            this.previewer.TabIndex = 0;
            this.previewer.TabStop = false;
            // 
            // descLabel
            // 
            this.descLabel.BackColor = System.Drawing.Color.Transparent;
            this.descLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.descLabel.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descLabel.ForeColor = System.Drawing.Color.Gray;
            this.descLabel.Location = new System.Drawing.Point(50, 14);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(250, 16);
            this.descLabel.TabIndex = 2;
            this.descLabel.Text = "The Description";
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.durationLabel.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.durationLabel.ForeColor = System.Drawing.Color.Gray;
            this.durationLabel.Location = new System.Drawing.Point(272, 0);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(28, 12);
            this.durationLabel.TabIndex = 3;
            this.durationLabel.Text = "03:25";
            this.durationLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // titleLabel
            // 
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.titleLabel.Location = new System.Drawing.Point(50, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(222, 15);
            this.titleLabel.TabIndex = 4;
            this.titleLabel.Text = "The Main Title";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // YoutubeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.descLabel);
            this.Controls.Add(this.previewer);
            this.Name = "YoutubeItem";
            this.Size = new System.Drawing.Size(300, 30);
            ((System.ComponentModel.ISupportInitialize)(this.previewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox previewer;
        private System.Windows.Forms.Label descLabel;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Label titleLabel;
    }
}

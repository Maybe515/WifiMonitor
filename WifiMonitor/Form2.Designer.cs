namespace WifiMonitor
{
    partial class Form2
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_blank = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文字サイズToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.標準ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.特大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ツールToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.電波取得一時停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 372);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(782, 31);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_blank
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel_blank";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(528, 25);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(52, 25);
            this.toolStripStatusLabel1.Text = "AP：";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(101, 25);
            this.toolStripStatusLabel2.Text = "LastRead：";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(66, 25);
            this.toolStripStatusLabel3.Text = "速度：";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem,
            this.表示ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(782, 35);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了ToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(96, 29);
            this.ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(155, 30);
            this.終了ToolStripMenuItem.Text = "終了";
            this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
            // 
            // 表示ToolStripMenuItem
            // 
            this.表示ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文字サイズToolStripMenuItem});
            this.表示ToolStripMenuItem.Name = "表示ToolStripMenuItem";
            this.表示ToolStripMenuItem.Size = new System.Drawing.Size(83, 29);
            this.表示ToolStripMenuItem.Text = "表示";
            // 
            // 文字サイズToolStripMenuItem
            // 
            this.文字サイズToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.小ToolStripMenuItem,
            this.標準ToolStripMenuItem,
            this.大ToolStripMenuItem,
            this.特大ToolStripMenuItem});
            this.文字サイズToolStripMenuItem.Name = "文字サイズToolStripMenuItem";
            this.文字サイズToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            this.文字サイズToolStripMenuItem.Text = "フォントサイズ";
            // 
            // 小ToolStripMenuItem
            // 
            this.小ToolStripMenuItem.Name = "小ToolStripMenuItem";
            this.小ToolStripMenuItem.Size = new System.Drawing.Size(134, 30);
            this.小ToolStripMenuItem.Text = "小";
            this.小ToolStripMenuItem.Click += new System.EventHandler(this.小ToolStripMenuItem_Click);
            // 
            // 標準ToolStripMenuItem
            // 
            this.標準ToolStripMenuItem.Name = "標準ToolStripMenuItem";
            this.標準ToolStripMenuItem.Size = new System.Drawing.Size(134, 30);
            this.標準ToolStripMenuItem.Text = "標準";
            this.標準ToolStripMenuItem.Click += new System.EventHandler(this.標準ToolStripMenuItem_Click);
            // 
            // 大ToolStripMenuItem
            // 
            this.大ToolStripMenuItem.Name = "大ToolStripMenuItem";
            this.大ToolStripMenuItem.Size = new System.Drawing.Size(134, 30);
            this.大ToolStripMenuItem.Text = "大";
            this.大ToolStripMenuItem.Click += new System.EventHandler(this.大ToolStripMenuItem_Click);
            // 
            // 特大ToolStripMenuItem
            // 
            this.特大ToolStripMenuItem.Name = "特大ToolStripMenuItem";
            this.特大ToolStripMenuItem.Size = new System.Drawing.Size(134, 30);
            this.特大ToolStripMenuItem.Text = "特大";
            this.特大ToolStripMenuItem.Click += new System.EventHandler(this.特大ToolStripMenuItem_Click);
            // 
            // ツールToolStripMenuItem
            // 
            this.ツールToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.電波取得一時停止ToolStripMenuItem});
            this.ツールToolStripMenuItem.Name = "ツールToolStripMenuItem";
            this.ツールToolStripMenuItem.Size = new System.Drawing.Size(66, 29);
            this.ツールToolStripMenuItem.Text = "ツール";
            // 
            // 電波取得一時停止ToolStripMenuItem
            // 
            this.電波取得一時停止ToolStripMenuItem.Name = "電波取得一時停止ToolStripMenuItem";
            this.電波取得一時停止ToolStripMenuItem.Size = new System.Drawing.Size(242, 30);
            this.電波取得一時停止ToolStripMenuItem.Text = "電波取得一時停止";
            this.電波取得一時停止ToolStripMenuItem.Click += new System.EventHandler(this.電波取得一時停止ToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView1.Font = new System.Drawing.Font("メイリオ", 11F);
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 33);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(782, 345);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "　SSID";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Channel";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Signal";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "認証";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 160;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "MACアドレス";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 170;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "暗号化";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "無線タイプ";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "インターネットの種類";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 190;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 403);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form2";
            this.Text = "WifiMonitor";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 表示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文字サイズToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 標準ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 大ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 特大ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ツールToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 電波取得一時停止ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_blank;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
    }
}


namespace DemoManager.App
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyBindsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.remoteGroupBox = new System.Windows.Forms.GroupBox();
            this.remoteRepositoryView = new DemoManager.App.RepositoryView();
            this.remoteDemoPanel = new System.Windows.Forms.Panel();
            this.downloadAndPlayButton = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            this.localGroupBox = new System.Windows.Forms.GroupBox();
            this.localRepositoryView = new DemoManager.App.RepositoryView();
            this.localDemoPanel = new System.Windows.Forms.Panel();
            this.deleteButton = new System.Windows.Forms.Button();
            this.renameButton = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.authKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.remoteGroupBox.SuspendLayout();
            this.remoteDemoPanel.SuspendLayout();
            this.localGroupBox.SuspendLayout();
            this.localDemoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1153, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.reloadToolStripMenuItem.Text = "&Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keyBindsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.optionsToolStripMenuItem.Text = "&Edit";
            // 
            // keyBindsToolStripMenuItem
            // 
            this.keyBindsToolStripMenuItem.Name = "keyBindsToolStripMenuItem";
            this.keyBindsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.keyBindsToolStripMenuItem.Text = "&Key binds...";
            this.keyBindsToolStripMenuItem.Click += new System.EventHandler(this.keyBindsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.authKeyToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 537);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1153, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";
            // 
            // progressBar
            // 
            this.progressBar.MarqueeAnimationSpeed = 1;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // remoteGroupBox
            // 
            this.remoteGroupBox.Controls.Add(this.remoteRepositoryView);
            this.remoteGroupBox.Controls.Add(this.remoteDemoPanel);
            this.remoteGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteGroupBox.Location = new System.Drawing.Point(0, 0);
            this.remoteGroupBox.Name = "remoteGroupBox";
            this.remoteGroupBox.Size = new System.Drawing.Size(574, 513);
            this.remoteGroupBox.TabIndex = 3;
            this.remoteGroupBox.TabStop = false;
            this.remoteGroupBox.Text = "Remote";
            // 
            // remoteRepositoryView
            // 
            this.remoteRepositoryView.AllowColumnReorder = true;
            this.remoteRepositoryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteRepositoryView.FullRowSelect = true;
            this.remoteRepositoryView.HideSelection = false;
            this.remoteRepositoryView.Location = new System.Drawing.Point(3, 16);
            this.remoteRepositoryView.MultiSelect = false;
            this.remoteRepositoryView.Name = "remoteRepositoryView";
            this.remoteRepositoryView.Repository = null;
            this.remoteRepositoryView.SelectedDemo = null;
            this.remoteRepositoryView.Size = new System.Drawing.Size(568, 436);
            this.remoteRepositoryView.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.remoteRepositoryView.TabIndex = 2;
            this.remoteRepositoryView.UseCompatibleStateImageBehavior = false;
            this.remoteRepositoryView.View = System.Windows.Forms.View.Details;
            this.remoteRepositoryView.SelectedIndexChanged += new System.EventHandler(this.remoteRepositoryView_SelectedIndexChanged);
            // 
            // remoteDemoPanel
            // 
            this.remoteDemoPanel.Controls.Add(this.downloadAndPlayButton);
            this.remoteDemoPanel.Controls.Add(this.downloadButton);
            this.remoteDemoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.remoteDemoPanel.Enabled = false;
            this.remoteDemoPanel.Location = new System.Drawing.Point(3, 452);
            this.remoteDemoPanel.Name = "remoteDemoPanel";
            this.remoteDemoPanel.Size = new System.Drawing.Size(568, 58);
            this.remoteDemoPanel.TabIndex = 1;
            // 
            // downloadAndPlayButton
            // 
            this.downloadAndPlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadAndPlayButton.Location = new System.Drawing.Point(465, 6);
            this.downloadAndPlayButton.Name = "downloadAndPlayButton";
            this.downloadAndPlayButton.Size = new System.Drawing.Size(100, 23);
            this.downloadAndPlayButton.TabIndex = 3;
            this.downloadAndPlayButton.Text = "D&ownload && Play";
            this.downloadAndPlayButton.UseVisualStyleBackColor = true;
            this.downloadAndPlayButton.Click += new System.EventHandler(this.downloadAndPlayButton_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadButton.Location = new System.Drawing.Point(465, 32);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(100, 23);
            this.downloadButton.TabIndex = 2;
            this.downloadButton.Text = "&Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // localGroupBox
            // 
            this.localGroupBox.Controls.Add(this.localRepositoryView);
            this.localGroupBox.Controls.Add(this.localDemoPanel);
            this.localGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localGroupBox.Location = new System.Drawing.Point(0, 0);
            this.localGroupBox.Name = "localGroupBox";
            this.localGroupBox.Size = new System.Drawing.Size(575, 513);
            this.localGroupBox.TabIndex = 4;
            this.localGroupBox.TabStop = false;
            this.localGroupBox.Text = "Local";
            // 
            // localRepositoryView
            // 
            this.localRepositoryView.AllowColumnReorder = true;
            this.localRepositoryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localRepositoryView.FullRowSelect = true;
            this.localRepositoryView.HideSelection = false;
            this.localRepositoryView.Location = new System.Drawing.Point(3, 16);
            this.localRepositoryView.MultiSelect = false;
            this.localRepositoryView.Name = "localRepositoryView";
            this.localRepositoryView.Repository = null;
            this.localRepositoryView.SelectedDemo = null;
            this.localRepositoryView.Size = new System.Drawing.Size(569, 436);
            this.localRepositoryView.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.localRepositoryView.TabIndex = 2;
            this.localRepositoryView.UseCompatibleStateImageBehavior = false;
            this.localRepositoryView.View = System.Windows.Forms.View.Details;
            this.localRepositoryView.SelectedIndexChanged += new System.EventHandler(this.localRepositoryView_SelectedIndexChanged);
            // 
            // localDemoPanel
            // 
            this.localDemoPanel.Controls.Add(this.deleteButton);
            this.localDemoPanel.Controls.Add(this.renameButton);
            this.localDemoPanel.Controls.Add(this.uploadButton);
            this.localDemoPanel.Controls.Add(this.playButton);
            this.localDemoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.localDemoPanel.Enabled = false;
            this.localDemoPanel.Location = new System.Drawing.Point(3, 452);
            this.localDemoPanel.Name = "localDemoPanel";
            this.localDemoPanel.Size = new System.Drawing.Size(569, 58);
            this.localDemoPanel.TabIndex = 0;
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.Location = new System.Drawing.Point(9, 32);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "De&lete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // renameButton
            // 
            this.renameButton.Location = new System.Drawing.Point(9, 6);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(100, 23);
            this.renameButton.TabIndex = 2;
            this.renameButton.Text = "&Rename";
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // uploadButton
            // 
            this.uploadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadButton.Location = new System.Drawing.Point(466, 32);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(100, 23);
            this.uploadButton.TabIndex = 1;
            this.uploadButton.Text = "&Upload";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // playButton
            // 
            this.playButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.playButton.Location = new System.Drawing.Point(466, 6);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(100, 23);
            this.playButton.TabIndex = 0;
            this.playButton.Text = "&Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.play_Click);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.localGroupBox);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.remoteGroupBox);
            this.mainSplitContainer.Size = new System.Drawing.Size(1153, 513);
            this.mainSplitContainer.SplitterDistance = 575;
            this.mainSplitContainer.TabIndex = 5;
            // 
            // authKeyToolStripMenuItem
            // 
            this.authKeyToolStripMenuItem.Name = "authKeyToolStripMenuItem";
            this.authKeyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.authKeyToolStripMenuItem.Text = "Auth Key";
            this.authKeyToolStripMenuItem.Click += new System.EventHandler(this.authKeyToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 559);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Demo Manager";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.remoteGroupBox.ResumeLayout(false);
            this.remoteDemoPanel.ResumeLayout(false);
            this.localGroupBox.ResumeLayout(false);
            this.localDemoPanel.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private RepositoryView remoteRepositoryView;
        private System.Windows.Forms.GroupBox remoteGroupBox;
        private System.Windows.Forms.GroupBox localGroupBox;
        private RepositoryView localRepositoryView;
        private System.Windows.Forms.Panel remoteDemoPanel;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Panel localDemoPanel;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyBindsToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.Button downloadAndPlayButton;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ToolStripMenuItem authKeyToolStripMenuItem;
    }
}


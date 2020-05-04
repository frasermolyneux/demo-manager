namespace DemoManager.App
{
    partial class FrmKeybinds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKeybinds));
            this.playbackGroupBox = new System.Windows.Forms.GroupBox();
            this.playbackPanel = new System.Windows.Forms.Panel();
            this.recordingGroupBox = new System.Windows.Forms.GroupBox();
            this.recordingPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hideUnavailableCheckBox = new System.Windows.Forms.CheckBox();
            this.playbackGroupBox.SuspendLayout();
            this.recordingGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // playbackGroupBox
            // 
            this.playbackGroupBox.Controls.Add(this.playbackPanel);
            this.playbackGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playbackGroupBox.Location = new System.Drawing.Point(2, 2);
            this.playbackGroupBox.Name = "playbackGroupBox";
            this.playbackGroupBox.Padding = new System.Windows.Forms.Padding(6, 12, 6, 6);
            this.playbackGroupBox.Size = new System.Drawing.Size(792, 205);
            this.playbackGroupBox.TabIndex = 0;
            this.playbackGroupBox.TabStop = false;
            this.playbackGroupBox.Text = "Playback";
            // 
            // playbackPanel
            // 
            this.playbackPanel.AutoScroll = true;
            this.playbackPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playbackPanel.Location = new System.Drawing.Point(6, 25);
            this.playbackPanel.Name = "playbackPanel";
            this.playbackPanel.Size = new System.Drawing.Size(780, 174);
            this.playbackPanel.TabIndex = 0;
            // 
            // recordingGroupBox
            // 
            this.recordingGroupBox.Controls.Add(this.recordingPanel);
            this.recordingGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.recordingGroupBox.Location = new System.Drawing.Point(2, 207);
            this.recordingGroupBox.Name = "recordingGroupBox";
            this.recordingGroupBox.Padding = new System.Windows.Forms.Padding(6, 12, 6, 6);
            this.recordingGroupBox.Size = new System.Drawing.Size(792, 130);
            this.recordingGroupBox.TabIndex = 1;
            this.recordingGroupBox.TabStop = false;
            this.recordingGroupBox.Text = "Recording";
            // 
            // recordingPanel
            // 
            this.recordingPanel.AutoScroll = true;
            this.recordingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recordingPanel.Location = new System.Drawing.Point(6, 25);
            this.recordingPanel.Name = "recordingPanel";
            this.recordingPanel.Size = new System.Drawing.Size(780, 99);
            this.recordingPanel.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(705, 9);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(624, 9);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hideUnavailableCheckBox);
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(2, 337);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 44);
            this.panel1.TabIndex = 4;
            // 
            // hideUnavailableCheckBox
            // 
            this.hideUnavailableCheckBox.AutoSize = true;
            this.hideUnavailableCheckBox.Location = new System.Drawing.Point(6, 13);
            this.hideUnavailableCheckBox.Name = "hideUnavailableCheckBox";
            this.hideUnavailableCheckBox.Size = new System.Drawing.Size(384, 17);
            this.hideUnavailableCheckBox.TabIndex = 4;
            this.hideUnavailableCheckBox.Text = "Hide playback keys not available in Call of Duty 2/Call of Duty World at War";
            this.hideUnavailableCheckBox.UseVisualStyleBackColor = true;
            this.hideUnavailableCheckBox.CheckedChanged += new System.EventHandler(this.hideUnavailableCheckBox_CheckedChanged);
            // 
            // frmKeybinds
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(796, 383);
            this.Controls.Add(this.playbackGroupBox);
            this.Controls.Add(this.recordingGroupBox);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmKeybinds";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Keybinds";
            this.playbackGroupBox.ResumeLayout(false);
            this.recordingGroupBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox playbackGroupBox;
        private System.Windows.Forms.GroupBox recordingGroupBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel playbackPanel;
        private System.Windows.Forms.Panel recordingPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox hideUnavailableCheckBox;
    }
}
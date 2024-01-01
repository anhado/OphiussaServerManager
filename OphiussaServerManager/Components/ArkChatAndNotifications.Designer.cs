namespace OphiussaServerManager.Components {
    partial class ArkChatAndNotifications {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.chkEnableJoinNotifications = new System.Windows.Forms.CheckBox();
            this.chkEnableLeftNotifications = new System.Windows.Forms.CheckBox();
            this.chkEnableProximityTextChat = new System.Windows.Forms.CheckBox();
            this.chkEnableGlobalVoiceChat = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkEnableJoinNotifications
            // 
            this.chkEnableJoinNotifications.AutoSize = true;
            this.chkEnableJoinNotifications.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.chkEnableJoinNotifications.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.chkEnableJoinNotifications.Location = new System.Drawing.Point(247, 28);
            this.chkEnableJoinNotifications.Name = "chkEnableJoinNotifications";
            this.chkEnableJoinNotifications.Size = new System.Drawing.Size(190, 17);
            this.chkEnableJoinNotifications.TabIndex = 8;
            this.chkEnableJoinNotifications.Tag = "EnablePlayerJoinedNotifications";
            this.chkEnableJoinNotifications.Text = "Enable \'Player Joined\' Notifications";
            this.chkEnableJoinNotifications.UseVisualStyleBackColor = false;
            // 
            // chkEnableLeftNotifications
            // 
            this.chkEnableLeftNotifications.AutoSize = true;
            this.chkEnableLeftNotifications.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.chkEnableLeftNotifications.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.chkEnableLeftNotifications.Location = new System.Drawing.Point(247, 3);
            this.chkEnableLeftNotifications.Name = "chkEnableLeftNotifications";
            this.chkEnableLeftNotifications.Size = new System.Drawing.Size(177, 17);
            this.chkEnableLeftNotifications.TabIndex = 7;
            this.chkEnableLeftNotifications.Tag = "EnablePlayerLeaveNotifications";
            this.chkEnableLeftNotifications.Text = "Enable \'Player Left\' Notifications";
            this.chkEnableLeftNotifications.UseVisualStyleBackColor = false;
            // 
            // chkEnableProximityTextChat
            // 
            this.chkEnableProximityTextChat.AutoSize = true;
            this.chkEnableProximityTextChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.chkEnableProximityTextChat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.chkEnableProximityTextChat.Location = new System.Drawing.Point(3, 28);
            this.chkEnableProximityTextChat.Name = "chkEnableProximityTextChat";
            this.chkEnableProximityTextChat.Size = new System.Drawing.Size(152, 17);
            this.chkEnableProximityTextChat.TabIndex = 6;
            this.chkEnableProximityTextChat.Tag = "EnableProximityChat";
            this.chkEnableProximityTextChat.Text = "Enable Proximity Text Chat";
            this.chkEnableProximityTextChat.UseVisualStyleBackColor = false;
            // 
            // chkEnableGlobalVoiceChat
            // 
            this.chkEnableGlobalVoiceChat.AutoSize = true;
            this.chkEnableGlobalVoiceChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.chkEnableGlobalVoiceChat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.chkEnableGlobalVoiceChat.Location = new System.Drawing.Point(3, 3);
            this.chkEnableGlobalVoiceChat.Name = "chkEnableGlobalVoiceChat";
            this.chkEnableGlobalVoiceChat.Size = new System.Drawing.Size(147, 17);
            this.chkEnableGlobalVoiceChat.TabIndex = 5;
            this.chkEnableGlobalVoiceChat.Tag = "EnableGlobalVoiceChat";
            this.chkEnableGlobalVoiceChat.Text = "Enable Global Voice Chat";
            this.chkEnableGlobalVoiceChat.UseVisualStyleBackColor = false;
            // 
            // ArkChatAndNotifications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkEnableJoinNotifications);
            this.Controls.Add(this.chkEnableLeftNotifications);
            this.Controls.Add(this.chkEnableProximityTextChat);
            this.Controls.Add(this.chkEnableGlobalVoiceChat);
            this.Name = "ArkChatAndNotifications";
            this.Size = new System.Drawing.Size(450, 51);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnableJoinNotifications;
        private System.Windows.Forms.CheckBox chkEnableLeftNotifications;
        private System.Windows.Forms.CheckBox chkEnableProximityTextChat;
        private System.Windows.Forms.CheckBox chkEnableGlobalVoiceChat;
    }
}

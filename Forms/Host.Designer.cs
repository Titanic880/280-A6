﻿
namespace A6_TCP.Forms
{
    partial class Host
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
            this.CbIP = new System.Windows.Forms.ComboBox();
            this.BtnStart = new System.Windows.Forms.Button();
            this.LblUserMessages = new System.Windows.Forms.Label();
            this.lstUMessage = new System.Windows.Forms.ListBox();
            this.LblIP = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CbIP
            // 
            this.CbIP.FormattingEnabled = true;
            this.CbIP.Location = new System.Drawing.Point(12, 32);
            this.CbIP.Name = "CbIP";
            this.CbIP.Size = new System.Drawing.Size(138, 21);
            this.CbIP.TabIndex = 15;
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(12, 263);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(138, 23);
            this.BtnStart.TabIndex = 14;
            this.BtnStart.Text = "Open Connection";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // LblUserMessages
            // 
            this.LblUserMessages.AutoSize = true;
            this.LblUserMessages.Location = new System.Drawing.Point(9, 56);
            this.LblUserMessages.Name = "LblUserMessages";
            this.LblUserMessages.Size = new System.Drawing.Size(80, 13);
            this.LblUserMessages.TabIndex = 13;
            this.LblUserMessages.Text = "User Messages";
            // 
            // lstUMessage
            // 
            this.lstUMessage.FormattingEnabled = true;
            this.lstUMessage.Location = new System.Drawing.Point(12, 71);
            this.lstUMessage.Name = "lstUMessage";
            this.lstUMessage.Size = new System.Drawing.Size(138, 186);
            this.lstUMessage.TabIndex = 12;
            // 
            // LblIP
            // 
            this.LblIP.AutoSize = true;
            this.LblIP.Location = new System.Drawing.Point(12, 16);
            this.LblIP.Name = "LblIP";
            this.LblIP.Size = new System.Drawing.Size(58, 13);
            this.LblIP.TabIndex = 11;
            this.LblIP.Text = "IP Address";
            // 
            // Host
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(169, 307);
            this.Controls.Add(this.CbIP);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.LblUserMessages);
            this.Controls.Add(this.lstUMessage);
            this.Controls.Add(this.LblIP);
            this.Name = "Host";
            this.Text = "Host";
            this.Load += new System.EventHandler(this.Host_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CbIP;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Label LblUserMessages;
        private System.Windows.Forms.ListBox lstUMessage;
        private System.Windows.Forms.Label LblIP;
    }
}
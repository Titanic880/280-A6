
namespace A6_TCP_Client
{
    partial class Client
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
            this.lstUMessage = new System.Windows.Forms.ListBox();
            this.LblUserMessages = new System.Windows.Forms.Label();
            this.BtnSend = new System.Windows.Forms.Button();
            this.TbMessage = new System.Windows.Forms.TextBox();
            this.BtnDisconnect = new System.Windows.Forms.Button();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.CbIP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnFile = new System.Windows.Forms.Button();
            this.TbDownload = new System.Windows.Forms.Button();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstUMessage
            // 
            this.lstUMessage.FormattingEnabled = true;
            this.lstUMessage.Location = new System.Drawing.Point(31, 26);
            this.lstUMessage.Name = "lstUMessage";
            this.lstUMessage.Size = new System.Drawing.Size(138, 186);
            this.lstUMessage.TabIndex = 4;
            // 
            // LblUserMessages
            // 
            this.LblUserMessages.AutoSize = true;
            this.LblUserMessages.Location = new System.Drawing.Point(28, 11);
            this.LblUserMessages.Name = "LblUserMessages";
            this.LblUserMessages.Size = new System.Drawing.Size(80, 13);
            this.LblUserMessages.TabIndex = 5;
            this.LblUserMessages.Text = "User Messages";
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(31, 242);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(138, 23);
            this.BtnSend.TabIndex = 6;
            this.BtnSend.Text = "Send Message";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // TbMessage
            // 
            this.TbMessage.Location = new System.Drawing.Point(31, 218);
            this.TbMessage.Name = "TbMessage";
            this.TbMessage.Size = new System.Drawing.Size(138, 20);
            this.TbMessage.TabIndex = 7;
            // 
            // BtnDisconnect
            // 
            this.BtnDisconnect.Location = new System.Drawing.Point(31, 329);
            this.BtnDisconnect.Name = "BtnDisconnect";
            this.BtnDisconnect.Size = new System.Drawing.Size(138, 23);
            this.BtnDisconnect.TabIndex = 8;
            this.BtnDisconnect.Text = "Disconnect";
            this.BtnDisconnect.UseVisualStyleBackColor = true;
            this.BtnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(175, 53);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(138, 23);
            this.BtnConnect.TabIndex = 18;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // CbIP
            // 
            this.CbIP.FormattingEnabled = true;
            this.CbIP.Location = new System.Drawing.Point(175, 26);
            this.CbIP.Name = "CbIP";
            this.CbIP.Size = new System.Drawing.Size(138, 21);
            this.CbIP.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(211, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "IP Address";
            // 
            // BtnFile
            // 
            this.BtnFile.Location = new System.Drawing.Point(175, 218);
            this.BtnFile.Name = "BtnFile";
            this.BtnFile.Size = new System.Drawing.Size(138, 23);
            this.BtnFile.TabIndex = 19;
            this.BtnFile.Text = "Send File";
            this.BtnFile.UseVisualStyleBackColor = true;
            this.BtnFile.Click += new System.EventHandler(this.BtnFile_Click);
            // 
            // TbDownload
            // 
            this.TbDownload.Location = new System.Drawing.Point(175, 247);
            this.TbDownload.Name = "TbDownload";
            this.TbDownload.Size = new System.Drawing.Size(138, 23);
            this.TbDownload.TabIndex = 20;
            this.TbDownload.Text = "Download File";
            this.TbDownload.UseVisualStyleBackColor = true;
            this.TbDownload.Click += new System.EventHandler(this.TbDownload_Click);
            // 
            // lstFiles
            // 
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Location = new System.Drawing.Point(175, 82);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(138, 134);
            this.lstFiles.TabIndex = 21;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 384);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.TbDownload);
            this.Controls.Add(this.BtnFile);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.CbIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnDisconnect);
            this.Controls.Add(this.TbMessage);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.LblUserMessages);
            this.Controls.Add(this.lstUMessage);
            this.Name = "Client";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lstUMessage;
        private System.Windows.Forms.Label LblUserMessages;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.TextBox TbMessage;
        private System.Windows.Forms.Button BtnDisconnect;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.ComboBox CbIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnFile;
        private System.Windows.Forms.Button TbDownload;
        private System.Windows.Forms.ListBox lstFiles;
    }
}


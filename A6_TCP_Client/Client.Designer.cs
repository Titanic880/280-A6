
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
            this.BtnConnect = new System.Windows.Forms.Button();
            this.CbIP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnFile = new System.Windows.Forms.Button();
            this.BtnDownload = new System.Windows.Forms.Button();
            this.lstCmdOut = new System.Windows.Forms.ListBox();
            this.TbUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstUMessage
            // 
            this.lstUMessage.FormattingEnabled = true;
            this.lstUMessage.Location = new System.Drawing.Point(31, 26);
            this.lstUMessage.Name = "lstUMessage";
            this.lstUMessage.Size = new System.Drawing.Size(236, 186);
            this.lstUMessage.TabIndex = 3;
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
            this.BtnSend.Location = new System.Drawing.Point(31, 248);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(236, 23);
            this.BtnSend.TabIndex = 5;
            this.BtnSend.Text = "Send Message";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // TbMessage
            // 
            this.TbMessage.Location = new System.Drawing.Point(31, 222);
            this.TbMessage.Name = "TbMessage";
            this.TbMessage.Size = new System.Drawing.Size(236, 20);
            this.TbMessage.TabIndex = 4;
            this.TbMessage.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TbMessage_PreviewKeyDown);
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(273, 278);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(188, 23);
            this.BtnConnect.TabIndex = 2;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // CbIP
            // 
            this.CbIP.FormattingEnabled = true;
            this.CbIP.Location = new System.Drawing.Point(273, 25);
            this.CbIP.Name = "CbIP";
            this.CbIP.Size = new System.Drawing.Size(188, 21);
            this.CbIP.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(337, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "IP Address";
            // 
            // BtnFile
            // 
            this.BtnFile.Location = new System.Drawing.Point(273, 249);
            this.BtnFile.Name = "BtnFile";
            this.BtnFile.Size = new System.Drawing.Size(188, 23);
            this.BtnFile.TabIndex = 7;
            this.BtnFile.Text = "Send File";
            this.BtnFile.UseVisualStyleBackColor = true;
            this.BtnFile.Click += new System.EventHandler(this.BtnFile_Click);
            // 
            // BtnDownload
            // 
            this.BtnDownload.Location = new System.Drawing.Point(31, 277);
            this.BtnDownload.Name = "BtnDownload";
            this.BtnDownload.Size = new System.Drawing.Size(236, 23);
            this.BtnDownload.TabIndex = 999;
            this.BtnDownload.Text = "Download File";
            this.BtnDownload.UseVisualStyleBackColor = true;
            this.BtnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // lstCmdOut
            // 
            this.lstCmdOut.FormattingEnabled = true;
            this.lstCmdOut.Location = new System.Drawing.Point(273, 106);
            this.lstCmdOut.Name = "lstCmdOut";
            this.lstCmdOut.Size = new System.Drawing.Size(188, 134);
            this.lstCmdOut.TabIndex = 6;
            // 
            // TbUsername
            // 
            this.TbUsername.Location = new System.Drawing.Point(273, 65);
            this.TbUsername.Name = "TbUsername";
            this.TbUsername.Size = new System.Drawing.Size(188, 20);
            this.TbUsername.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(335, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "User Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(315, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Command Outputs";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 368);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TbUsername);
            this.Controls.Add(this.lstCmdOut);
            this.Controls.Add(this.BtnDownload);
            this.Controls.Add(this.BtnFile);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.CbIP);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.ComboBox CbIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnFile;
        private System.Windows.Forms.Button BtnDownload;
        private System.Windows.Forms.ListBox lstCmdOut;
        private System.Windows.Forms.TextBox TbUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}


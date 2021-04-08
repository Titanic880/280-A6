
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
            this.lstError = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstCommands = new System.Windows.Forms.ListBox();
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
            // lstError
            // 
            this.lstError.FormattingEnabled = true;
            this.lstError.Location = new System.Drawing.Point(156, 71);
            this.lstError.Name = "lstError";
            this.lstError.Size = new System.Drawing.Size(140, 186);
            this.lstError.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Error Log";
            // 
            // lstFiles
            // 
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Location = new System.Drawing.Point(302, 71);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(141, 186);
            this.lstFiles.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "List of Files";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(446, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Commands Run";
            // 
            // lstCommands
            // 
            this.lstCommands.FormattingEnabled = true;
            this.lstCommands.Location = new System.Drawing.Point(449, 71);
            this.lstCommands.Name = "lstCommands";
            this.lstCommands.Size = new System.Drawing.Size(141, 186);
            this.lstCommands.TabIndex = 20;
            // 
            // Host
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 307);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstCommands);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstError);
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
        private System.Windows.Forms.ListBox lstError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstCommands;
    }
}
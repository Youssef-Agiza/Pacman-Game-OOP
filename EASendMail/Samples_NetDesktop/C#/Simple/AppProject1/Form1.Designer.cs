namespace AppProject1
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TextBoxFrom = new System.Windows.Forms.TextBox();
            this.TextBoxTo = new System.Windows.Forms.TextBox();
            this.TextBoxCc = new System.Windows.Forms.TextBox();
            this.TextBoxSubject = new System.Windows.Forms.TextBox();
            this.GroupBoxServer = new System.Windows.Forms.GroupBox();
            this.ComboBoxPorts = new System.Windows.Forms.ComboBox();
            this.ComboBoxProtocols = new System.Windows.Forms.ComboBox();
            this.CheckBoxAuth = new System.Windows.Forms.CheckBox();
            this.CheckBoxSsl = new System.Windows.Forms.CheckBox();
            this.TextBoxPassword = new System.Windows.Forms.TextBox();
            this.TextBoxUser = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Server = new System.Windows.Forms.Label();
            this.TextBoxServer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TextBoxAttachments = new System.Windows.Forms.TextBox();
            this.ButtonSend = new System.Windows.Forms.Button();
            this.ProgressBarSend = new System.Windows.Forms.ProgressBar();
            this.StatusBarSend = new System.Windows.Forms.StatusBar();
            this.label9 = new System.Windows.Forms.Label();
            this.ComboBoxEncoding = new System.Windows.Forms.ComboBox();
            this.ButtonAddAttachment = new System.Windows.Forms.Button();
            this.ButtonClearAttachments = new System.Windows.Forms.Button();
            this.TextBoxBody = new System.Windows.Forms.RichTextBox();
            this.openFileDialogAttachment = new System.Windows.Forms.OpenFileDialog();
            this.CheckBoxHtml = new System.Windows.Forms.CheckBox();
            this.CheckBoxSignature = new System.Windows.Forms.CheckBox();
            this.CheckBoxEncrypt = new System.Windows.Forms.CheckBox();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.GroupBoxServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Subject";
            // 
            // TextBoxFrom
            // 
            this.TextBoxFrom.Location = new System.Drawing.Point(78, 15);
            this.TextBoxFrom.Name = "TextBoxFrom";
            this.TextBoxFrom.Size = new System.Drawing.Size(350, 24);
            this.TextBoxFrom.TabIndex = 1;
            this.TextBoxFrom.TextChanged += new System.EventHandler(this.TextBoxFrom_TextChanged);
            // 
            // TextBoxTo
            // 
            this.TextBoxTo.Location = new System.Drawing.Point(78, 51);
            this.TextBoxTo.Name = "TextBoxTo";
            this.TextBoxTo.Size = new System.Drawing.Size(350, 24);
            this.TextBoxTo.TabIndex = 2;
            // 
            // TextBoxCc
            // 
            this.TextBoxCc.Location = new System.Drawing.Point(78, 85);
            this.TextBoxCc.Name = "TextBoxCc";
            this.TextBoxCc.Size = new System.Drawing.Size(350, 24);
            this.TextBoxCc.TabIndex = 3;
            // 
            // TextBoxSubject
            // 
            this.TextBoxSubject.Location = new System.Drawing.Point(78, 120);
            this.TextBoxSubject.Name = "TextBoxSubject";
            this.TextBoxSubject.Size = new System.Drawing.Size(350, 24);
            this.TextBoxSubject.TabIndex = 4;
            this.TextBoxSubject.Text = "Test subject";
            // 
            // GroupBoxServer
            // 
            this.GroupBoxServer.Controls.Add(this.ComboBoxPorts);
            this.GroupBoxServer.Controls.Add(this.ComboBoxProtocols);
            this.GroupBoxServer.Controls.Add(this.CheckBoxAuth);
            this.GroupBoxServer.Controls.Add(this.CheckBoxSsl);
            this.GroupBoxServer.Controls.Add(this.TextBoxPassword);
            this.GroupBoxServer.Controls.Add(this.TextBoxUser);
            this.GroupBoxServer.Controls.Add(this.label7);
            this.GroupBoxServer.Controls.Add(this.label6);
            this.GroupBoxServer.Controls.Add(this.Server);
            this.GroupBoxServer.Controls.Add(this.TextBoxServer);
            this.GroupBoxServer.Location = new System.Drawing.Point(451, 2);
            this.GroupBoxServer.Name = "GroupBoxServer";
            this.GroupBoxServer.Size = new System.Drawing.Size(333, 251);
            this.GroupBoxServer.TabIndex = 8;
            this.GroupBoxServer.TabStop = false;
            // 
            // ComboBoxPorts
            // 
            this.ComboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxPorts.FormattingEnabled = true;
            this.ComboBoxPorts.Location = new System.Drawing.Point(262, 17);
            this.ComboBoxPorts.Name = "ComboBoxPorts";
            this.ComboBoxPorts.Size = new System.Drawing.Size(62, 26);
            this.ComboBoxPorts.TabIndex = 16;
            // 
            // ComboBoxProtocols
            // 
            this.ComboBoxProtocols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxProtocols.FormattingEnabled = true;
            this.ComboBoxProtocols.Location = new System.Drawing.Point(14, 210);
            this.ComboBoxProtocols.Name = "ComboBoxProtocols";
            this.ComboBoxProtocols.Size = new System.Drawing.Size(310, 26);
            this.ComboBoxProtocols.TabIndex = 15;
            this.ComboBoxProtocols.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProtocols_SelectedIndexChanged);
            // 
            // CheckBoxAuth
            // 
            this.CheckBoxAuth.Location = new System.Drawing.Point(14, 56);
            this.CheckBoxAuth.Name = "CheckBoxAuth";
            this.CheckBoxAuth.Size = new System.Drawing.Size(282, 27);
            this.CheckBoxAuth.TabIndex = 11;
            this.CheckBoxAuth.Text = "My server requires user authentication";
            this.CheckBoxAuth.CheckedChanged += new System.EventHandler(this.CheckBoxAuth_CheckedChanged);
            // 
            // CheckBoxSsl
            // 
            this.CheckBoxSsl.Location = new System.Drawing.Point(14, 172);
            this.CheckBoxSsl.Name = "CheckBoxSsl";
            this.CheckBoxSsl.Size = new System.Drawing.Size(215, 33);
            this.CheckBoxSsl.TabIndex = 14;
            this.CheckBoxSsl.Text = "SSL Connection";
            // 
            // TextBoxPassword
            // 
            this.TextBoxPassword.Location = new System.Drawing.Point(91, 131);
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.PasswordChar = '*';
            this.TextBoxPassword.Size = new System.Drawing.Size(234, 24);
            this.TextBoxPassword.TabIndex = 13;
            // 
            // TextBoxUser
            // 
            this.TextBoxUser.Location = new System.Drawing.Point(91, 92);
            this.TextBoxUser.Name = "TextBoxUser";
            this.TextBoxUser.Size = new System.Drawing.Size(234, 24);
            this.TextBoxUser.TabIndex = 12;
            this.TextBoxUser.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxUser_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 18);
            this.label7.TabIndex = 2;
            this.label7.Text = "Password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "User";
            // 
            // Server
            // 
            this.Server.AutoSize = true;
            this.Server.Location = new System.Drawing.Point(14, 21);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(51, 18);
            this.Server.TabIndex = 0;
            this.Server.Text = "Server";
            // 
            // TextBoxServer
            // 
            this.TextBoxServer.Location = new System.Drawing.Point(80, 18);
            this.TextBoxServer.Name = "TextBoxServer";
            this.TextBoxServer.Size = new System.Drawing.Size(181, 24);
            this.TextBoxServer.TabIndex = 10;
            this.TextBoxServer.TextChanged += new System.EventHandler(this.TextBoxServer_TextChanged);
            this.TextBoxServer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxServer_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 262);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 18);
            this.label8.TabIndex = 9;
            this.label8.Text = "Attachments";
            // 
            // TextBoxAttachments
            // 
            this.TextBoxAttachments.BackColor = System.Drawing.SystemColors.Info;
            this.TextBoxAttachments.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.TextBoxAttachments.Location = new System.Drawing.Point(121, 261);
            this.TextBoxAttachments.Name = "TextBoxAttachments";
            this.TextBoxAttachments.ReadOnly = true;
            this.TextBoxAttachments.Size = new System.Drawing.Size(533, 24);
            this.TextBoxAttachments.TabIndex = 6;
            // 
            // ButtonSend
            // 
            this.ButtonSend.Location = new System.Drawing.Point(568, 495);
            this.ButtonSend.Name = "ButtonSend";
            this.ButtonSend.Size = new System.Drawing.Size(103, 28);
            this.ButtonSend.TabIndex = 15;
            this.ButtonSend.TabStop = false;
            this.ButtonSend.Text = "Send";
            this.ButtonSend.Click += new System.EventHandler(this.ButtonSend_Click);
            // 
            // ProgressBarSend
            // 
            this.ProgressBarSend.Location = new System.Drawing.Point(18, 505);
            this.ProgressBarSend.Name = "ProgressBarSend";
            this.ProgressBarSend.Size = new System.Drawing.Size(536, 12);
            this.ProgressBarSend.TabIndex = 13;
            // 
            // StatusBarSend
            // 
            this.StatusBarSend.Location = new System.Drawing.Point(0, 546);
            this.StatusBarSend.Name = "StatusBarSend";
            this.StatusBarSend.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StatusBarSend.Size = new System.Drawing.Size(801, 27);
            this.StatusBarSend.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 172);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "Encoding";
            // 
            // ComboBoxEncoding
            // 
            this.ComboBoxEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxEncoding.Location = new System.Drawing.Point(92, 170);
            this.ComboBoxEncoding.Name = "ComboBoxEncoding";
            this.ComboBoxEncoding.Size = new System.Drawing.Size(232, 26);
            this.ComboBoxEncoding.TabIndex = 5;
            // 
            // ButtonAddAttachment
            // 
            this.ButtonAddAttachment.Location = new System.Drawing.Point(666, 260);
            this.ButtonAddAttachment.Name = "ButtonAddAttachment";
            this.ButtonAddAttachment.Size = new System.Drawing.Size(47, 28);
            this.ButtonAddAttachment.TabIndex = 7;
            this.ButtonAddAttachment.Text = "Add";
            this.ButtonAddAttachment.Click += new System.EventHandler(this.ButtonAddAttachment_Click);
            // 
            // ButtonClearAttachments
            // 
            this.ButtonClearAttachments.Location = new System.Drawing.Point(720, 260);
            this.ButtonClearAttachments.Name = "ButtonClearAttachments";
            this.ButtonClearAttachments.Size = new System.Drawing.Size(57, 28);
            this.ButtonClearAttachments.TabIndex = 8;
            this.ButtonClearAttachments.Text = "Clear";
            this.ButtonClearAttachments.Click += new System.EventHandler(this.ButtonClearAttachments_Click);
            // 
            // TextBoxBody
            // 
            this.TextBoxBody.Location = new System.Drawing.Point(19, 305);
            this.TextBoxBody.Name = "TextBoxBody";
            this.TextBoxBody.Size = new System.Drawing.Size(765, 180);
            this.TextBoxBody.TabIndex = 14;
            this.TextBoxBody.Text = "";
            // 
            // CheckBoxHtml
            // 
            this.CheckBoxHtml.AutoSize = true;
            this.CheckBoxHtml.Location = new System.Drawing.Point(19, 219);
            this.CheckBoxHtml.Name = "CheckBoxHtml";
            this.CheckBoxHtml.Size = new System.Drawing.Size(113, 22);
            this.CheckBoxHtml.TabIndex = 16;
            this.CheckBoxHtml.Text = "Html Format";
            // 
            // CheckBoxSignature
            // 
            this.CheckBoxSignature.AutoSize = true;
            this.CheckBoxSignature.Location = new System.Drawing.Point(149, 219);
            this.CheckBoxSignature.Name = "CheckBoxSignature";
            this.CheckBoxSignature.Size = new System.Drawing.Size(139, 22);
            this.CheckBoxSignature.TabIndex = 17;
            this.CheckBoxSignature.Text = "Digitial Signature";
            // 
            // CheckBoxEncrypt
            // 
            this.CheckBoxEncrypt.AutoSize = true;
            this.CheckBoxEncrypt.Location = new System.Drawing.Point(309, 219);
            this.CheckBoxEncrypt.Name = "CheckBoxEncrypt";
            this.CheckBoxEncrypt.Size = new System.Drawing.Size(80, 22);
            this.CheckBoxEncrypt.TabIndex = 18;
            this.CheckBoxEncrypt.Text = "Encrypt";
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Enabled = false;
            this.ButtonCancel.Location = new System.Drawing.Point(680, 495);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(103, 28);
            this.ButtonCancel.TabIndex = 19;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 573);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.CheckBoxEncrypt);
            this.Controls.Add(this.CheckBoxSignature);
            this.Controls.Add(this.CheckBoxHtml);
            this.Controls.Add(this.TextBoxBody);
            this.Controls.Add(this.ButtonClearAttachments);
            this.Controls.Add(this.ButtonAddAttachment);
            this.Controls.Add(this.ComboBoxEncoding);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.TextBoxAttachments);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TextBoxSubject);
            this.Controls.Add(this.TextBoxCc);
            this.Controls.Add(this.TextBoxTo);
            this.Controls.Add(this.TextBoxFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StatusBarSend);
            this.Controls.Add(this.ProgressBarSend);
            this.Controls.Add(this.ButtonSend);
            this.Controls.Add(this.GroupBoxServer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.GroupBoxServer.ResumeLayout(false);
            this.GroupBoxServer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox GroupBoxServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox CheckBoxSsl;
        private System.Windows.Forms.TextBox TextBoxFrom;
        private System.Windows.Forms.TextBox TextBoxTo;
        private System.Windows.Forms.TextBox TextBoxCc;
        private System.Windows.Forms.TextBox TextBoxSubject;
        private System.Windows.Forms.TextBox TextBoxPassword;
        private System.Windows.Forms.TextBox TextBoxUser;
        private System.Windows.Forms.Label Server;
        private System.Windows.Forms.TextBox TextBoxServer;
        private System.Windows.Forms.TextBox TextBoxAttachments;
        private System.Windows.Forms.Button ButtonSend;
        private System.Windows.Forms.ProgressBar ProgressBarSend;
        private System.Windows.Forms.StatusBar StatusBarSend;
        private System.Windows.Forms.CheckBox CheckBoxAuth;
        private System.Windows.Forms.Button ButtonAddAttachment;
        private System.Windows.Forms.Button ButtonClearAttachments;
        private System.Windows.Forms.ComboBox ComboBoxEncoding;
        private System.Windows.Forms.RichTextBox TextBoxBody;
        private System.Windows.Forms.OpenFileDialog openFileDialogAttachment;
        private System.Windows.Forms.CheckBox CheckBoxHtml;
        private System.Windows.Forms.CheckBox CheckBoxSignature;
        private System.Windows.Forms.CheckBox CheckBoxEncrypt;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.ComboBox ComboBoxProtocols;
        private System.Windows.Forms.ComboBox ComboBoxPorts;

    }
}


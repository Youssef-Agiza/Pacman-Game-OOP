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
            this.OpenFileDialogAttachment = new System.Windows.Forms.OpenFileDialog();
            this.CheckBoxSignature = new System.Windows.Forms.CheckBox();
            this.CheckBoxEncrypt = new System.Windows.Forms.CheckBox();
            this.ColorDialogForeColor = new System.Windows.Forms.ColorDialog();
            this.ComboBoxFont = new System.Windows.Forms.ComboBox();
            this.ComboBoxSize = new System.Windows.Forms.ComboBox();
            this.ButtonBold = new System.Windows.Forms.Button();
            this.ButtonItalic = new System.Windows.Forms.Button();
            this.ButtonUnderline = new System.Windows.Forms.Button();
            this.ButtonColor = new System.Windows.Forms.Button();
            this.ButtonInsertImage = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.HtmlEditor = new System.Windows.Forms.WebBrowser();
            this.GroupBoxServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Subject";
            // 
            // TextBoxFrom
            // 
            this.TextBoxFrom.Location = new System.Drawing.Point(84, 23);
            this.TextBoxFrom.Name = "TextBoxFrom";
            this.TextBoxFrom.Size = new System.Drawing.Size(344, 24);
            this.TextBoxFrom.TabIndex = 1;
            this.TextBoxFrom.TextChanged += new System.EventHandler(this.TextBoxFrom_TextChanged);
            // 
            // TextBoxTo
            // 
            this.TextBoxTo.Location = new System.Drawing.Point(84, 55);
            this.TextBoxTo.Name = "TextBoxTo";
            this.TextBoxTo.Size = new System.Drawing.Size(344, 24);
            this.TextBoxTo.TabIndex = 2;
            // 
            // TextBoxCc
            // 
            this.TextBoxCc.Location = new System.Drawing.Point(84, 87);
            this.TextBoxCc.Name = "TextBoxCc";
            this.TextBoxCc.Size = new System.Drawing.Size(344, 24);
            this.TextBoxCc.TabIndex = 3;
            // 
            // TextBoxSubject
            // 
            this.TextBoxSubject.Location = new System.Drawing.Point(84, 121);
            this.TextBoxSubject.Name = "TextBoxSubject";
            this.TextBoxSubject.Size = new System.Drawing.Size(344, 24);
            this.TextBoxSubject.TabIndex = 4;
            this.TextBoxSubject.Text = "Test email from HtmlMail sample";
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
            this.GroupBoxServer.Location = new System.Drawing.Point(457, 10);
            this.GroupBoxServer.Name = "GroupBoxServer";
            this.GroupBoxServer.Size = new System.Drawing.Size(326, 223);
            this.GroupBoxServer.TabIndex = 8;
            this.GroupBoxServer.TabStop = false;
            // 
            // ComboBoxPorts
            // 
            this.ComboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxPorts.FormattingEnabled = true;
            this.ComboBoxPorts.Location = new System.Drawing.Point(254, 19);
            this.ComboBoxPorts.Name = "ComboBoxPorts";
            this.ComboBoxPorts.Size = new System.Drawing.Size(55, 26);
            this.ComboBoxPorts.TabIndex = 16;
            // 
            // ComboBoxProtocols
            // 
            this.ComboBoxProtocols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxProtocols.FormattingEnabled = true;
            this.ComboBoxProtocols.Location = new System.Drawing.Point(13, 184);
            this.ComboBoxProtocols.Name = "ComboBoxProtocols";
            this.ComboBoxProtocols.Size = new System.Drawing.Size(297, 26);
            this.ComboBoxProtocols.TabIndex = 15;
            this.ComboBoxProtocols.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProtocols_SelectedIndexChanged);
            // 
            // CheckBoxAuth
            // 
            this.CheckBoxAuth.AutoSize = true;
            this.CheckBoxAuth.Location = new System.Drawing.Point(13, 52);
            this.CheckBoxAuth.Name = "CheckBoxAuth";
            this.CheckBoxAuth.Size = new System.Drawing.Size(247, 22);
            this.CheckBoxAuth.TabIndex = 11;
            this.CheckBoxAuth.Text = "My server requires authentication";
            this.CheckBoxAuth.CheckedChanged += new System.EventHandler(this.CheckBoxAuth_CheckedChanged);
            // 
            // CheckBoxSsl
            // 
            this.CheckBoxSsl.AutoSize = true;
            this.CheckBoxSsl.Location = new System.Drawing.Point(13, 152);
            this.CheckBoxSsl.Name = "CheckBoxSsl";
            this.CheckBoxSsl.Size = new System.Drawing.Size(138, 22);
            this.CheckBoxSsl.TabIndex = 14;
            this.CheckBoxSsl.Text = "SSL Connection";
            // 
            // TextBoxPassword
            // 
            this.TextBoxPassword.Location = new System.Drawing.Point(87, 117);
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.PasswordChar = '*';
            this.TextBoxPassword.Size = new System.Drawing.Size(223, 24);
            this.TextBoxPassword.TabIndex = 13;
            // 
            // TextBoxUser
            // 
            this.TextBoxUser.Location = new System.Drawing.Point(87, 85);
            this.TextBoxUser.Name = "TextBoxUser";
            this.TextBoxUser.Size = new System.Drawing.Size(223, 24);
            this.TextBoxUser.TabIndex = 12;
            this.TextBoxUser.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxUser_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 18);
            this.label7.TabIndex = 2;
            this.label7.Text = "Password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "User";
            // 
            // Server
            // 
            this.Server.AutoSize = true;
            this.Server.Location = new System.Drawing.Point(13, 23);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(51, 18);
            this.Server.TabIndex = 0;
            this.Server.Text = "Server";
            // 
            // TextBoxServer
            // 
            this.TextBoxServer.Location = new System.Drawing.Point(87, 20);
            this.TextBoxServer.Name = "TextBoxServer";
            this.TextBoxServer.Size = new System.Drawing.Size(163, 24);
            this.TextBoxServer.TabIndex = 10;
            this.TextBoxServer.TextChanged += new System.EventHandler(this.TextBoxServer_TextChanged);
            this.TextBoxServer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxServer_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 246);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 18);
            this.label8.TabIndex = 9;
            this.label8.Text = "Attachments";
            // 
            // TextBoxAttachments
            // 
            this.TextBoxAttachments.BackColor = System.Drawing.SystemColors.Info;
            this.TextBoxAttachments.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.TextBoxAttachments.Location = new System.Drawing.Point(114, 243);
            this.TextBoxAttachments.Name = "TextBoxAttachments";
            this.TextBoxAttachments.ReadOnly = true;
            this.TextBoxAttachments.Size = new System.Drawing.Size(546, 24);
            this.TextBoxAttachments.TabIndex = 6;
            // 
            // ButtonSend
            // 
            this.ButtonSend.Location = new System.Drawing.Point(587, 509);
            this.ButtonSend.Name = "ButtonSend";
            this.ButtonSend.Size = new System.Drawing.Size(93, 28);
            this.ButtonSend.TabIndex = 15;
            this.ButtonSend.TabStop = false;
            this.ButtonSend.Text = "Send";
            this.ButtonSend.Click += new System.EventHandler(this.ButtonSend_Click);
            // 
            // ProgressBarSend
            // 
            this.ProgressBarSend.Location = new System.Drawing.Point(12, 518);
            this.ProgressBarSend.Name = "ProgressBarSend";
            this.ProgressBarSend.Size = new System.Drawing.Size(544, 9);
            this.ProgressBarSend.TabIndex = 13;
            // 
            // StatusBarSend
            // 
            this.StatusBarSend.Location = new System.Drawing.Point(0, 555);
            this.StatusBarSend.Name = "StatusBarSend";
            this.StatusBarSend.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StatusBarSend.Size = new System.Drawing.Size(800, 27);
            this.StatusBarSend.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "Encoding";
            // 
            // ComboBoxEncoding
            // 
            this.ComboBoxEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxEncoding.Location = new System.Drawing.Point(97, 158);
            this.ComboBoxEncoding.Name = "ComboBoxEncoding";
            this.ComboBoxEncoding.Size = new System.Drawing.Size(229, 26);
            this.ComboBoxEncoding.TabIndex = 5;
            // 
            // ButtonAddAttachment
            // 
            this.ButtonAddAttachment.Location = new System.Drawing.Point(675, 241);
            this.ButtonAddAttachment.Name = "ButtonAddAttachment";
            this.ButtonAddAttachment.Size = new System.Drawing.Size(46, 28);
            this.ButtonAddAttachment.TabIndex = 7;
            this.ButtonAddAttachment.Text = "Add";
            this.ButtonAddAttachment.Click += new System.EventHandler(this.ButtonAddAttachment_Click);
            // 
            // ButtonClearAttachments
            // 
            this.ButtonClearAttachments.Location = new System.Drawing.Point(728, 241);
            this.ButtonClearAttachments.Name = "ButtonClearAttachments";
            this.ButtonClearAttachments.Size = new System.Drawing.Size(54, 28);
            this.ButtonClearAttachments.TabIndex = 8;
            this.ButtonClearAttachments.Text = "Clear";
            this.ButtonClearAttachments.Click += new System.EventHandler(this.ButtonClearAttachments_Click);
            // 
            // CheckBoxSignature
            // 
            this.CheckBoxSignature.AutoSize = true;
            this.CheckBoxSignature.Location = new System.Drawing.Point(21, 200);
            this.CheckBoxSignature.Name = "CheckBoxSignature";
            this.CheckBoxSignature.Size = new System.Drawing.Size(139, 22);
            this.CheckBoxSignature.TabIndex = 17;
            this.CheckBoxSignature.Text = "Digitial Signature";
            // 
            // CheckBoxEncrypt
            // 
            this.CheckBoxEncrypt.AutoSize = true;
            this.CheckBoxEncrypt.Location = new System.Drawing.Point(184, 200);
            this.CheckBoxEncrypt.Name = "CheckBoxEncrypt";
            this.CheckBoxEncrypt.Size = new System.Drawing.Size(80, 22);
            this.CheckBoxEncrypt.TabIndex = 18;
            this.CheckBoxEncrypt.Text = "Encrypt";
            // 
            // ComboBoxFont
            // 
            this.ComboBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFont.Location = new System.Drawing.Point(17, 273);
            this.ComboBoxFont.Name = "ComboBoxFont";
            this.ComboBoxFont.Size = new System.Drawing.Size(159, 26);
            this.ComboBoxFont.TabIndex = 20;
            this.ComboBoxFont.SelectedIndexChanged += new System.EventHandler(this.ComboBoxFont_SelectedIndexChanged);
            // 
            // ComboBoxSize
            // 
            this.ComboBoxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxSize.Location = new System.Drawing.Point(185, 273);
            this.ComboBoxSize.Name = "ComboBoxSize";
            this.ComboBoxSize.Size = new System.Drawing.Size(94, 26);
            this.ComboBoxSize.TabIndex = 21;
            this.ComboBoxSize.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSize_SelectedIndexChanged);
            // 
            // ButtonBold
            // 
            this.ButtonBold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonBold.Location = new System.Drawing.Point(288, 272);
            this.ButtonBold.Name = "ButtonBold";
            this.ButtonBold.Size = new System.Drawing.Size(28, 28);
            this.ButtonBold.TabIndex = 22;
            this.ButtonBold.Text = "B";
            this.ButtonBold.Click += new System.EventHandler(this.ButtonBold_Click);
            // 
            // ButtonItalic
            // 
            this.ButtonItalic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonItalic.Location = new System.Drawing.Point(316, 272);
            this.ButtonItalic.Name = "ButtonItalic";
            this.ButtonItalic.Size = new System.Drawing.Size(28, 28);
            this.ButtonItalic.TabIndex = 23;
            this.ButtonItalic.Text = "I";
            this.ButtonItalic.Click += new System.EventHandler(this.ButtonItalic_Click);
            // 
            // ButtonUnderline
            // 
            this.ButtonUnderline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonUnderline.Location = new System.Drawing.Point(344, 272);
            this.ButtonUnderline.Name = "ButtonUnderline";
            this.ButtonUnderline.Size = new System.Drawing.Size(28, 28);
            this.ButtonUnderline.TabIndex = 24;
            this.ButtonUnderline.Text = "U";
            this.ButtonUnderline.Click += new System.EventHandler(this.ButtonUnderline_Click);
            // 
            // ButtonColor
            // 
            this.ButtonColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonColor.ForeColor = System.Drawing.Color.Red;
            this.ButtonColor.Location = new System.Drawing.Point(372, 272);
            this.ButtonColor.Name = "ButtonColor";
            this.ButtonColor.Size = new System.Drawing.Size(28, 28);
            this.ButtonColor.TabIndex = 25;
            this.ButtonColor.Text = "C";
            this.ButtonColor.Click += new System.EventHandler(this.ButtonColor_Click);
            // 
            // ButtonInsertImage
            // 
            this.ButtonInsertImage.Location = new System.Drawing.Point(419, 272);
            this.ButtonInsertImage.Name = "ButtonInsertImage";
            this.ButtonInsertImage.Size = new System.Drawing.Size(102, 28);
            this.ButtonInsertImage.TabIndex = 26;
            this.ButtonInsertImage.Text = "Insert Picture";
            this.ButtonInsertImage.Click += new System.EventHandler(this.ButtonInsertImage_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Enabled = false;
            this.ButtonCancel.Location = new System.Drawing.Point(691, 509);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(93, 28);
            this.ButtonCancel.TabIndex = 27;
            this.ButtonCancel.TabStop = false;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // HtmlEditor
            // 
            this.HtmlEditor.Location = new System.Drawing.Point(15, 304);
            this.HtmlEditor.MinimumSize = new System.Drawing.Size(20, 20);
            this.HtmlEditor.Name = "HtmlEditor";
            this.HtmlEditor.Size = new System.Drawing.Size(769, 188);
            this.HtmlEditor.TabIndex = 28;
            this.HtmlEditor.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.HtmlEditor_Navigated);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 582);
            this.Controls.Add(this.HtmlEditor);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonInsertImage);
            this.Controls.Add(this.ButtonColor);
            this.Controls.Add(this.ButtonUnderline);
            this.Controls.Add(this.ButtonItalic);
            this.Controls.Add(this.ButtonBold);
            this.Controls.Add(this.ComboBoxSize);
            this.Controls.Add(this.ComboBoxFont);
            this.Controls.Add(this.CheckBoxEncrypt);
            this.Controls.Add(this.CheckBoxSignature);
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
        private System.Windows.Forms.OpenFileDialog OpenFileDialogAttachment;
        private System.Windows.Forms.CheckBox CheckBoxSignature;
        private System.Windows.Forms.CheckBox CheckBoxEncrypt;
        private System.Windows.Forms.ColorDialog ColorDialogForeColor;
        private System.Windows.Forms.ComboBox ComboBoxFont;
        private System.Windows.Forms.ComboBox ComboBoxSize;
        private System.Windows.Forms.Button ButtonBold;
        private System.Windows.Forms.Button ButtonItalic;
        private System.Windows.Forms.Button ButtonUnderline;
        private System.Windows.Forms.Button ButtonColor;
        private System.Windows.Forms.Button ButtonInsertImage;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.WebBrowser HtmlEditor;
        private System.Windows.Forms.ComboBox ComboBoxProtocols;
        private System.Windows.Forms.ComboBox ComboBoxPorts;
    }
}


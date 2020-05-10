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
            this.label4 = new System.Windows.Forms.Label();
            this.TextBoxFrom = new System.Windows.Forms.TextBox();
            this.TextBoxSubject = new System.Windows.Forms.TextBox();
            this.GroupBoxServer = new System.Windows.Forms.GroupBox();
            this.NumericUpDownConnections = new System.Windows.Forms.NumericUpDown();
            this.CheckBoxTestRecipient = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.label9 = new System.Windows.Forms.Label();
            this.ComboBoxEncoding = new System.Windows.Forms.ComboBox();
            this.ButtonAddAttachment = new System.Windows.Forms.Button();
            this.ButtonClearAttachments = new System.Windows.Forms.Button();
            this.TextBoxBody = new System.Windows.Forms.RichTextBox();
            this.attachmentDlg = new System.Windows.Forms.OpenFileDialog();
            this.ListViewTo = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ButtonAddRecipient = new System.Windows.Forms.Button();
            this.ButtonClearRecipients = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonSimpleSend = new System.Windows.Forms.Button();
            this.StatusBarSend = new System.Windows.Forms.StatusBar();
            this.GroupBoxServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownConnections)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Subject";
            // 
            // TextBoxFrom
            // 
            this.TextBoxFrom.Location = new System.Drawing.Point(75, 10);
            this.TextBoxFrom.Name = "TextBoxFrom";
            this.TextBoxFrom.Size = new System.Drawing.Size(382, 24);
            this.TextBoxFrom.TabIndex = 1;
            this.TextBoxFrom.TextChanged += new System.EventHandler(this.TextBoxFrom_TextChanged);
            // 
            // TextBoxSubject
            // 
            this.TextBoxSubject.Location = new System.Drawing.Point(75, 279);
            this.TextBoxSubject.Name = "TextBoxSubject";
            this.TextBoxSubject.Size = new System.Drawing.Size(382, 24);
            this.TextBoxSubject.TabIndex = 4;
            this.TextBoxSubject.Text = "Test sample";
            // 
            // GroupBoxServer
            // 
            this.GroupBoxServer.Controls.Add(this.NumericUpDownConnections);
            this.GroupBoxServer.Controls.Add(this.CheckBoxTestRecipient);
            this.GroupBoxServer.Controls.Add(this.label3);
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
            this.GroupBoxServer.Location = new System.Drawing.Point(478, 1);
            this.GroupBoxServer.Name = "GroupBoxServer";
            this.GroupBoxServer.Size = new System.Drawing.Size(306, 338);
            this.GroupBoxServer.TabIndex = 8;
            this.GroupBoxServer.TabStop = false;
            // 
            // NumericUpDownConnections
            // 
            this.NumericUpDownConnections.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.NumericUpDownConnections.Location = new System.Drawing.Point(16, 248);
            this.NumericUpDownConnections.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.NumericUpDownConnections.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.NumericUpDownConnections.Name = "NumericUpDownConnections";
            this.NumericUpDownConnections.Size = new System.Drawing.Size(68, 24);
            this.NumericUpDownConnections.TabIndex = 22;
            this.NumericUpDownConnections.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // CheckBoxTestRecipient
            // 
            this.CheckBoxTestRecipient.AutoSize = true;
            this.CheckBoxTestRecipient.Location = new System.Drawing.Point(16, 288);
            this.CheckBoxTestRecipient.Name = "CheckBoxTestRecipient";
            this.CheckBoxTestRecipient.Size = new System.Drawing.Size(158, 22);
            this.CheckBoxTestRecipient.TabIndex = 21;
            this.CheckBoxTestRecipient.Text = "Test Email Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "Maximum Connections";
            // 
            // ComboBoxPorts
            // 
            this.ComboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxPorts.FormattingEnabled = true;
            this.ComboBoxPorts.Location = new System.Drawing.Point(240, 18);
            this.ComboBoxPorts.Name = "ComboBoxPorts";
            this.ComboBoxPorts.Size = new System.Drawing.Size(53, 26);
            this.ComboBoxPorts.TabIndex = 16;
            // 
            // ComboBoxProtocols
            // 
            this.ComboBoxProtocols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxProtocols.FormattingEnabled = true;
            this.ComboBoxProtocols.Location = new System.Drawing.Point(15, 205);
            this.ComboBoxProtocols.Name = "ComboBoxProtocols";
            this.ComboBoxProtocols.Size = new System.Drawing.Size(273, 26);
            this.ComboBoxProtocols.TabIndex = 15;
            this.ComboBoxProtocols.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProtocols_SelectedIndexChanged);
            // 
            // CheckBoxAuth
            // 
            this.CheckBoxAuth.AutoSize = true;
            this.CheckBoxAuth.Location = new System.Drawing.Point(13, 58);
            this.CheckBoxAuth.Name = "CheckBoxAuth";
            this.CheckBoxAuth.Size = new System.Drawing.Size(247, 22);
            this.CheckBoxAuth.TabIndex = 11;
            this.CheckBoxAuth.Text = "My server requires authentication";
            this.CheckBoxAuth.CheckedChanged += new System.EventHandler(this.CheckBoxAuth_CheckedChanged);
            // 
            // CheckBoxSsl
            // 
            this.CheckBoxSsl.AutoSize = true;
            this.CheckBoxSsl.Location = new System.Drawing.Point(13, 166);
            this.CheckBoxSsl.Name = "CheckBoxSsl";
            this.CheckBoxSsl.Size = new System.Drawing.Size(138, 22);
            this.CheckBoxSsl.TabIndex = 14;
            this.CheckBoxSsl.Text = "SSL Connection";
            // 
            // TextBoxPassword
            // 
            this.TextBoxPassword.Location = new System.Drawing.Point(85, 126);
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.PasswordChar = '*';
            this.TextBoxPassword.Size = new System.Drawing.Size(204, 24);
            this.TextBoxPassword.TabIndex = 13;
            // 
            // TextBoxUser
            // 
            this.TextBoxUser.Location = new System.Drawing.Point(85, 92);
            this.TextBoxUser.Name = "TextBoxUser";
            this.TextBoxUser.Size = new System.Drawing.Size(204, 24);
            this.TextBoxUser.TabIndex = 12;
            this.TextBoxUser.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxUser_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 18);
            this.label7.TabIndex = 2;
            this.label7.Text = "Password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "User";
            // 
            // Server
            // 
            this.Server.AutoSize = true;
            this.Server.Location = new System.Drawing.Point(9, 21);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(51, 18);
            this.Server.TabIndex = 0;
            this.Server.Text = "Server";
            // 
            // TextBoxServer
            // 
            this.TextBoxServer.Location = new System.Drawing.Point(85, 19);
            this.TextBoxServer.Name = "TextBoxServer";
            this.TextBoxServer.Size = new System.Drawing.Size(153, 24);
            this.TextBoxServer.TabIndex = 10;
            this.TextBoxServer.TextChanged += new System.EventHandler(this.TextBoxServer_TextChanged);
            this.TextBoxServer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxServer_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 359);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 18);
            this.label8.TabIndex = 9;
            this.label8.Text = "Attachments";
            // 
            // TextBoxAttachments
            // 
            this.TextBoxAttachments.BackColor = System.Drawing.SystemColors.Info;
            this.TextBoxAttachments.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.TextBoxAttachments.Location = new System.Drawing.Point(110, 356);
            this.TextBoxAttachments.Name = "TextBoxAttachments";
            this.TextBoxAttachments.ReadOnly = true;
            this.TextBoxAttachments.Size = new System.Drawing.Size(537, 24);
            this.TextBoxAttachments.TabIndex = 6;
            // 
            // ButtonSend
            // 
            this.ButtonSend.Location = new System.Drawing.Point(411, 525);
            this.ButtonSend.Name = "ButtonSend";
            this.ButtonSend.Size = new System.Drawing.Size(121, 28);
            this.ButtonSend.TabIndex = 15;
            this.ButtonSend.TabStop = false;
            this.ButtonSend.Text = "Send";
            this.ButtonSend.Click += new System.EventHandler(this.ButtonSend_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 321);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "Encoding";
            // 
            // ComboBoxEncoding
            // 
            this.ComboBoxEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxEncoding.Location = new System.Drawing.Point(110, 317);
            this.ComboBoxEncoding.Name = "ComboBoxEncoding";
            this.ComboBoxEncoding.Size = new System.Drawing.Size(239, 26);
            this.ComboBoxEncoding.TabIndex = 5;
            // 
            // ButtonAddAttachment
            // 
            this.ButtonAddAttachment.Location = new System.Drawing.Point(661, 354);
            this.ButtonAddAttachment.Name = "ButtonAddAttachment";
            this.ButtonAddAttachment.Size = new System.Drawing.Size(47, 28);
            this.ButtonAddAttachment.TabIndex = 7;
            this.ButtonAddAttachment.Text = "Add";
            this.ButtonAddAttachment.Click += new System.EventHandler(this.ButtonAddAttachment_Click);
            // 
            // ButtonClearAttachments
            // 
            this.ButtonClearAttachments.Location = new System.Drawing.Point(717, 354);
            this.ButtonClearAttachments.Name = "ButtonClearAttachments";
            this.ButtonClearAttachments.Size = new System.Drawing.Size(61, 28);
            this.ButtonClearAttachments.TabIndex = 8;
            this.ButtonClearAttachments.Text = "Clear";
            this.ButtonClearAttachments.Click += new System.EventHandler(this.ButtonClearAttachments_Click);
            // 
            // TextBoxBody
            // 
            this.TextBoxBody.Location = new System.Drawing.Point(13, 398);
            this.TextBoxBody.Name = "TextBoxBody";
            this.TextBoxBody.Size = new System.Drawing.Size(771, 114);
            this.TextBoxBody.TabIndex = 14;
            this.TextBoxBody.Text = "";
            // 
            // ListViewTo
            // 
            this.ListViewTo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colAddress,
            this.colStatus});
            this.ListViewTo.FullRowSelect = true;
            this.ListViewTo.GridLines = true;
            this.ListViewTo.Location = new System.Drawing.Point(76, 41);
            this.ListViewTo.Name = "ListViewTo";
            this.ListViewTo.Size = new System.Drawing.Size(381, 227);
            this.ListViewTo.TabIndex = 19;
            this.ListViewTo.UseCompatibleStateImageBehavior = false;
            this.ListViewTo.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 50;
            // 
            // colAddress
            // 
            this.colAddress.Text = "Address";
            this.colAddress.Width = 150;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 300;
            // 
            // ButtonAddRecipient
            // 
            this.ButtonAddRecipient.Location = new System.Drawing.Point(14, 68);
            this.ButtonAddRecipient.Name = "ButtonAddRecipient";
            this.ButtonAddRecipient.Size = new System.Drawing.Size(56, 28);
            this.ButtonAddRecipient.TabIndex = 21;
            this.ButtonAddRecipient.Text = "Add";
            this.ButtonAddRecipient.Click += new System.EventHandler(this.ButtonAddRecipient_Click);
            // 
            // ButtonClearRecipients
            // 
            this.ButtonClearRecipients.Location = new System.Drawing.Point(14, 107);
            this.ButtonClearRecipients.Name = "ButtonClearRecipients";
            this.ButtonClearRecipients.Size = new System.Drawing.Size(56, 28);
            this.ButtonClearRecipients.TabIndex = 22;
            this.ButtonClearRecipients.Text = "Clear";
            this.ButtonClearRecipients.Click += new System.EventHandler(this.ButtonClearRecipients_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Enabled = false;
            this.ButtonCancel.Location = new System.Drawing.Point(672, 525);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(112, 28);
            this.ButtonCancel.TabIndex = 23;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonSimpleSend
            // 
            this.ButtonSimpleSend.Location = new System.Drawing.Point(541, 525);
            this.ButtonSimpleSend.Name = "ButtonSimpleSend";
            this.ButtonSimpleSend.Size = new System.Drawing.Size(122, 28);
            this.ButtonSimpleSend.TabIndex = 24;
            this.ButtonSimpleSend.TabStop = false;
            this.ButtonSimpleSend.Text = "Simple Send";
            this.ButtonSimpleSend.Click += new System.EventHandler(this.ButtonSimpleSend_Click);
            // 
            // StatusBarSend
            // 
            this.StatusBarSend.Location = new System.Drawing.Point(0, 568);
            this.StatusBarSend.Name = "StatusBarSend";
            this.StatusBarSend.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StatusBarSend.Size = new System.Drawing.Size(799, 32);
            this.StatusBarSend.TabIndex = 14;
            this.StatusBarSend.Text = "Ready";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 600);
            this.Controls.Add(this.StatusBarSend);
            this.Controls.Add(this.ButtonSimpleSend);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonClearRecipients);
            this.Controls.Add(this.ButtonAddRecipient);
            this.Controls.Add(this.ListViewTo);
            this.Controls.Add(this.TextBoxBody);
            this.Controls.Add(this.ButtonClearAttachments);
            this.Controls.Add(this.ButtonAddAttachment);
            this.Controls.Add(this.ComboBoxEncoding);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.TextBoxAttachments);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TextBoxSubject);
            this.Controls.Add(this.TextBoxFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonSend);
            this.Controls.Add(this.GroupBoxServer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.GroupBoxServer.ResumeLayout(false);
            this.GroupBoxServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownConnections)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox GroupBoxServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox CheckBoxSsl;
        private System.Windows.Forms.TextBox TextBoxFrom;
        private System.Windows.Forms.TextBox TextBoxSubject;
        private System.Windows.Forms.TextBox TextBoxPassword;
        private System.Windows.Forms.TextBox TextBoxUser;
        private System.Windows.Forms.Label Server;
        private System.Windows.Forms.TextBox TextBoxServer;
        private System.Windows.Forms.TextBox TextBoxAttachments;
        private System.Windows.Forms.Button ButtonSend;
        private System.Windows.Forms.CheckBox CheckBoxAuth;
        private System.Windows.Forms.Button ButtonAddAttachment;
        private System.Windows.Forms.Button ButtonClearAttachments;
        private System.Windows.Forms.ComboBox ComboBoxEncoding;
        
        private System.Windows.Forms.RichTextBox TextBoxBody;
        private System.Windows.Forms.OpenFileDialog attachmentDlg;
        private System.Windows.Forms.ListView ListViewTo;
        private System.Windows.Forms.Button ButtonAddRecipient;
        private System.Windows.Forms.Button ButtonClearRecipients;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colAddress;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonSimpleSend;
        private System.Windows.Forms.ComboBox ComboBoxProtocols;
        private System.Windows.Forms.ComboBox ComboBoxPorts;
        private System.Windows.Forms.CheckBox CheckBoxTestRecipient;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NumericUpDownConnections;
        private System.Windows.Forms.StatusBar StatusBarSend;
    }
}


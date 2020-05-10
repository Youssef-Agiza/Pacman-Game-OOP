namespace AppProject1
{
    partial class FormOauth
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
            this.OauthBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // OauthBrowser
            // 
            this.OauthBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OauthBrowser.Location = new System.Drawing.Point(0, 0);
            this.OauthBrowser.MinimumSize = new System.Drawing.Size(20, 18);
            this.OauthBrowser.Name = "OauthBrowser";
            this.OauthBrowser.ScriptErrorsSuppressed = true;
            this.OauthBrowser.Size = new System.Drawing.Size(584, 661);
            this.OauthBrowser.TabIndex = 0;
            this.OauthBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.OauthBrowser_DocumentCompleted);
            this.OauthBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.OauthBrowser_Navigated);
            // 
            // FormOauth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 661);
            this.Controls.Add(this.OauthBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOauth";
            this.Text = "Web Login (OAUTH)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOauth_FormClosing);
            this.Load += new System.EventHandler(this.FormOauth_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser OauthBrowser;
    }
}
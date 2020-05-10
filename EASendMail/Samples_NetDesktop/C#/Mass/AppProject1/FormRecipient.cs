using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppProject1
{
    public partial class FormRecipient : Form
    {
        public FormRecipient()
        {
            InitializeComponent();
        }

        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {
            return;
        }

        private void ButtonOK_Click(object sender, System.EventArgs e)
        {
            TextBoxName.Text = TextBoxName.Text.Trim();
            TextBoxAddress.Text = TextBoxAddress.Text.Trim();
            if (TextBoxAddress.Text.Length == 0)
            {
                MessageBox.Show(this, "Please input a valid email address!");
                TextBoxAddress.Focus();
                return;
            }

            ButtonOK.DialogResult = DialogResult.OK;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

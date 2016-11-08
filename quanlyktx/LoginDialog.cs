using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlyktx
{
    public partial class LoginDialog : Form
    {
        public LoginDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doLogin();
        }

        private void doLogin()
        {
            if (tbUsername.Text == "admin" && tbPassword.Text == "admin")
            {
                Close();
            }
            else
            {
                string message = "Wrong user name or password!";
                string title = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, title, buttons);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void tbPassword_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btDangnhap.PerformClick();
            }
        }
    }
}

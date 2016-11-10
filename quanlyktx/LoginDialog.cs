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


            if (tbUsername.Text == "a" && tbPassword.Text == "a")
            {
                Program.LOGGED_IN = true;
                Program.form1.afterLoggedIn();
                Close();
            }
            else
            {
                string message = "Wrong user name or password!";
                string title = "Error";
                Utils.showOkDialog(title, message);
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

﻿using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btDangNhap_Click(object sender, EventArgs e)
        {
            //LoginDialog loginDialog = new LoginDialog();
            //loginDialog.ShowDialog();
            afterLoggedIn();
        }

        public void afterLoggedIn()
        {
            pnMain.Visible = true;
            btDangNhap.Visible = false;
        }
    }
}
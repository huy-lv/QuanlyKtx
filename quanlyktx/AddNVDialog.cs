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
    public partial class AddNVDialog : Form
    {
        public bool isEditing { get; set; }
        public AddNVDialog()
        {
            InitializeComponent();
        }

        private void add_btThemNV_Click(object sender, EventArgs e)
        {
            if (add_tbMatKhau.Text != "" &&
                add_tbMaNV.Text != "" &&
                add_tbGhiChu.Text != "" &&
                add_tbEmail.Text != "" &&
                add_tbChucVu.Text != "" &&
                add_tbHoTenNV.Text != ""
                )
            {
                if (isEditing)
                {
                    Program.form1.editNV(add_tbMaNV.Text, add_tbHoTenNV.Text, add_tbMatKhau.Text, add_tbChucVu.Text, add_tbEmail.Text, add_tbGhiChu.Text);
                }
                else
                {
                    Program.form1.addNV(add_tbMaNV.Text, add_tbHoTenNV.Text, add_tbMatKhau.Text, add_tbChucVu.Text, add_tbEmail.Text, add_tbGhiChu.Text);
                }
                this.Close();
            }else
            {
                Utils.showOkDialog(Utils.TITLE_ERROR, "Vui lòng nhập đầy đủ thông tin!");
            }
        }
    }
}

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
    public partial class AddDialog : Form
    {
        public bool isEditing { get; set; }

        public AddDialog()
        {
            InitializeComponent();
        }


        private void add_btThem_Click(object sender, EventArgs e)
        {
            if (add_tbHoTen.Text != "" &&
                add_tbMaPhong.Text != "" &&
                add_tbMaSinhVien.Text != "" &&
                //add_tbNgayDK.Text != "" &&
                //add_tbNgaySinh.Text != "" &&
                add_tbSdt.Text != "" &&
                add_tbTenLop.Text != "" &&
                add_tbThoiGianHoc.Text != "" &&
                add_tbTrangThai.Text != "" &&
                    add_tbHoKhau.Text != "" &&
                    add_tbGioiTinh.Text != "")
            {
                if (isEditing)
                {
                    Program.form1.editSv(add_tbMaSinhVien.Text,
                     add_tbHoTen.Text,
                     add_tbMaPhong.Text,
                     add_tbHoKhau.Text,
                     add_tbGioiTinh.Text,
                     add_dtpNgaySinh.Value.Date,
                     add_dtpNgayDK.Value.Date,
                     add_tbTrangThai.Text,
                     Convert.ToInt32(add_tbSdt.Text),
                     add_tbThoiGianHoc.Text,
                     add_tbTenLop.Text);
                }
                else
                {
                    Program.form1.addSv(add_tbMaSinhVien.Text,
                     add_tbHoTen.Text,
                     add_tbMaPhong.Text,
                     add_tbHoKhau.Text,
                     add_tbGioiTinh.Text,
                     add_dtpNgaySinh.Value.Date,
                     add_dtpNgayDK.Value.Date,
                     add_tbTrangThai.Text,
                     Convert.ToInt32(add_tbSdt.Text),
                     add_tbThoiGianHoc.Text,
                     add_tbTenLop.Text
                     );
                }
                this.Close();

            }
            else
            {
                Utils.showOkDialog("Lỗi", "Vui lòng nhập đầy đủ thông tin!");
            }
        }


    }
}

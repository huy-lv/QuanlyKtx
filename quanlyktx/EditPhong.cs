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
    public partial class EditPhong : Form
    {
        public EditPhong()
        {
            InitializeComponent();
        }

        private void btSuaPhong_Click(object sender, EventArgs e)
        {
            if (add_tbMaPhong.Text != "" &&
                add_tbTenPhong.Text != "" &&
                add_tbMaTang.Text != "" &&
                add_tbMaLoaiPhong.Text != "" &&
                add_tbSLMax.Text != "" &&
                add_tbSLDangO.Text != "" &&
                add_tbSoDienThangTruoc.Text != "" &&
                add_tbSoDienThangSau.Text != ""&&
                add_tbSoNuocThangTruoc.Text != "" &&
                add_tbSoNuocThangSau.Text != ""&&
                add_tbSLTaiSan.Text != "" &&
                add_tbTinhTrang.Text != "" &&
                add_tbGioiTinh.Text != ""
                )
            {
                Program.form1.editPhong(add_tbMaPhong.Text,
                    add_tbTenPhong.Text,
                    add_tbMaTang.Text,
                    add_tbMaLoaiPhong.Text,
                    Convert.ToInt32(add_tbSLMax.Text),
                    Convert.ToInt32(add_tbSLDangO.Text),
                    Convert.ToInt32(add_tbSoDienThangTruoc.Text),
                    Convert.ToInt32(add_tbSoDienThangSau.Text),
                    Convert.ToInt32(add_tbSoNuocThangTruoc.Text),
                    Convert.ToInt32(add_tbSoNuocThangSau.Text),
                    Convert.ToInt32(add_tbSLTaiSan.Text),
                    add_tbTinhTrang.Text,
                    add_tbGioiTinh.Text
                );
            }
            else
            {
                Utils.showOkDialog(Utils.TITLE_ERROR, "Vui lòng nhập đầy đủ thông tin!");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace quanlyktx
{
    public partial class Form1 : Form
    {

        SqlConnection sqlConn = new SqlConnection(@"Data Source=DESKTOP-6LL873M\SQLEXPRESS;Initial Catalog=kytucxa;Integrated Security=True");


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

            loadDbSinhVien();
        }

        private void loadDbSinhVien()
        {
            var adapterSv = new SqlDataAdapter("select * from sinhvien", sqlConn);
            var tableSv = new DataTable();
            adapterSv.Fill(tableSv);
            dgvSinhVien.DataSource = tableSv;

            tbMaSV.DataBindings.Clear();
            tbMaSV.DataBindings.Add("text", dgvSinhVien.DataSource, "masv");
            tbHoTen.DataBindings.Clear();
            tbHoTen.DataBindings.Add("text", dgvSinhVien.DataSource, "hoten");

            tbNgaySinh.DataBindings.Clear();
            tbNgaySinh.DataBindings.Add("text", dgvSinhVien.DataSource, "ngaysinh");

            tbNgayDK.DataBindings.Clear();
            tbNgayDK.DataBindings.Add("text", dgvSinhVien.DataSource, "ngaydangky");
        }

        internal void addSv(string s1, string s2, string s3, string s4, string s5, DateTime s6, DateTime s7, string s8, string s9, string s10, string s11)
        {
            try {
                sqlConn.Open();
                string c = "insert into sinhvien values('" +
                    s1 + "',N'" + s2 + "','" + s3 + "',N'" + s4 + "',N'" +
                    s5 + "','" + s6 + "','" + s7 + "',N'" + s8 + "','" +
                    s9 + "',N'" + s10 + "',N'" + s11 + "')";
                Console.WriteLine(c);
                //string c = "insert into sinhvien values('101','nguyen van b','0312','hokhau','gnh','" + s6 + "','" + s7 + "','tt',111101,'thoigianhoc','lop')";
                var cmd = new SqlCommand(c, sqlConn);
                cmd.ExecuteNonQuery();
                sqlConn.Close();
            }catch(SqlException e)
            {
                Console.WriteLine(e.Message);
                Utils.showOkDialog("Error", e.Message);
            }
            finally
            {
                sqlConn.Close();
                loadDbSinhVien();
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            AddDialog addDialog = new AddDialog();
            addDialog.ShowDialog();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            AddDialog editDialog = new AddDialog();
            editDialog.add_tbMaSinhVien.Text = tbMaSV.Text;
            editDialog.add_tbHoTen.Text = tbHoTen.Text;
            editDialog.add_tbMaPhong.Text = tbMaPhong.Text;
            editDialog.add_tbHoKhau.Text = tbHoKhau.Text;
            editDialog.add_dtpNgaySinh.Value = DateTime.Parse(tbNgaySinh.Text);
            editDialog.add_dtpNgayDK.Value = DateTime.Parse(tbNgayDK.Text);
            editDialog.add_tbTrangThai.Text = tbMaSV.Text;
            editDialog.add_tbSdt.Text = tbSdt.Text;
            editDialog.add_tbThoiGianHoc.Text = tbThoiGianHoc.Text;
            editDialog.add_tbTenLop.Text = tbTenLop.Text;
            editDialog.ShowDialog();
        }

        private void s_Click(object sender, EventArgs e)
        {
            MessageBoxButtons bt = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("Bạn chắc chắn xóa chứ?", "Thông báo",bt);
            if(result == DialogResult.Yes)
            {
                try
                {
                    sqlConn.Open();
                    string c = "delete from sinhvien where masv = '" + tbMaSV.Text + "'";
                    var cmd = new SqlCommand(c, sqlConn);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ee)
                {
                    Utils.showOkDialog("Error", ee.Message);
                }
                finally
                {
                    sqlConn.Close();
                    loadDbSinhVien();
                }
                
            }
            
        }
    }
}

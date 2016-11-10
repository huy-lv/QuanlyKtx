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

        DataClasses1DataContext db = new DataClasses1DataContext();
        

        public Form1()
        {
            InitializeComponent();
        }
        private async void btDangNhap_Click_1(object sender, EventArgs e)
        {
            //var r = db.nhanvien_selectbyemail(tbUsername.Text);
            //if (!r.Any())
            //{
            //    Console.WriteLine("cxz");
            //}
            if (tbUsername.Text == "admin")
            {
                await LoadingAsync();
            }
            else
            {
                Utils.showOkDialog("Thông báo", "Sai tài khoản hoặc mật khẩu!");
            }

        }

        public async Task LoadingAsync()
        {
            pbLoading.Visible = true;
            Task<int> longRunningTask = LongRunningOperationAsync();
            int result = await longRunningTask;
            afterLoggedIn();
        }

        public async Task<int> LongRunningOperationAsync() // assume we return an int from this long running operation 
        {
            await Task.Run(() => loadDB());
            return 1;
        }

        public void afterLoggedIn()
        {
            pnMain.Visible = true;
            pbLoading.Visible = false;
        }

        void loadDB()
        {
            loadDbSinhVien();
            loadDBNV();
        }

        private void loadDbSinhVien()
        {
            dgvSinhVien.DataSource = db.sinhvien_selectall();

            tbMaSV.DataBindings.Clear();
            tbMaSV.DataBindings.Add("text", dgvSinhVien.DataSource, "masv");
            tbHoTen.DataBindings.Clear();
            tbHoTen.DataBindings.Add("text", dgvSinhVien.DataSource, "hoten");
            tbMaPhong.DataBindings.Clear();
            tbMaPhong.DataBindings.Add("text", dgvSinhVien.DataSource, "maphong");
            tbHoKhau.DataBindings.Clear();
            tbHoKhau.DataBindings.Add("text", dgvSinhVien.DataSource, "hokhau");
            tbGioiTinh.DataBindings.Clear();
            tbGioiTinh.DataBindings.Add("text", dgvSinhVien.DataSource, "gioitinh");
            tbNgaySinh.DataBindings.Clear();
            tbNgaySinh.DataBindings.Add("text", dgvSinhVien.DataSource, "ngaysinh");
            tbNgayDK.DataBindings.Clear();
            tbNgayDK.DataBindings.Add("text", dgvSinhVien.DataSource, "ngaydangky");
            tbTrangThai.DataBindings.Clear();
            tbTrangThai.DataBindings.Add("text", dgvSinhVien.DataSource, "trangthai");
            tbSdt.DataBindings.Clear();
            tbSdt.DataBindings.Add("text", dgvSinhVien.DataSource, "sdt");
            tbThoiGianHoc.DataBindings.Clear();
            tbThoiGianHoc.DataBindings.Add("text", dgvSinhVien.DataSource, "thoigianhoc");
            tbLop.DataBindings.Clear();
            tbLop.DataBindings.Add("text", dgvSinhVien.DataSource, "lop");

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
            editDialog.add_tbGioiTinh.Text = tbGioiTinh.Text;
            editDialog.add_tbSdt.Text = tbSdt.Text;
            editDialog.add_tbThoiGianHoc.Text = tbThoiGianHoc.Text;
            editDialog.add_tbTenLop.Text = tbLop.Text;

            editDialog.add_tbMaSinhVien.ReadOnly = true;
            editDialog.add_btThem.Text = "Xong";
            editDialog.isEditing = true;
            editDialog.ShowDialog();
        }

        internal void addSv(string s1, string s2, string s3, string s4, string s5, DateTime s6, DateTime s7, string s8, int s9, string s10, string s11)
        {
            try
            {
                db.sinhvien_insert(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11);
                Utils.showOkDialog("Thông báo", "Thêm thành công!");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Utils.showOkDialog("Error", e.Message);
            }
            finally
            {
                //sqlConn.Close();
                loadDbSinhVien();
            }
        }
        internal void editSv(string s1, string s2, string s3, string s4, string s5, DateTime s6, DateTime s7, string s8, int s9, string s10, string s11)
        {
            try
            {
                db.sinhvien_update(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11);
                Utils.showOkDialog("Thông báo", "Sửa thành công!");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Utils.showOkDialog("Error", e.Message);
            }
            finally
            {
                //sqlConn.Close();
                loadDbSinhVien();
            }
        }
        private void btXoa_Click(object sender, EventArgs e)
        {
            MessageBoxButtons bt = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("Bạn chắc chắn xóa chứ?", "Thông báo",bt);
            if(result == DialogResult.Yes)
            {
                try
                {
                    db.sinhvien_delete(tbMaSV.Text);
                    Utils.showOkDialog("Thông báo", "Xóa thành công!");
                }
                catch (SqlException ee)
                {
                    Utils.showOkDialog("Error", ee.Message);
                }
                finally
                {
                    loadDbSinhVien();
                }
                
            }
            
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            pnMain.Visible = false;
        }



        /// //////////////////////////////////////////////////////////////////////////////////
        /// /// //////////////////////////////////////////////////////////////////////////////////
        /// /// //////////////////////////////////////////////////////////////////////////////////
        /// /// //////////////////////////////////////////////////////////////////////////////////
        /// /// //////////////////////////////////////////////////////////////////////////////////
        /// /// //////////////////////////////////////////////////////////////////////////////////

        void loadDBNV()
        {
            dgvNhanVien.DataSource = db.nhanvien_selectall();

            tbMaNV.DataBindings.Clear();
            tbMaNV.DataBindings.Add("text", dgvSinhVien.DataSource, "manv");
            tbTenDangNhap.DataBindings.Clear();
            tbTenDangNhap.DataBindings.Add("text", dgvSinhVien.DataSource, "tendangnhap");
            tbMatKhau.DataBindings.Clear();
            tbMatKhau.DataBindings.Add("text", dgvSinhVien.DataSource, "matkhau");
            tbChucVu.DataBindings.Clear();
            tbChucVu.DataBindings.Add("text", dgvSinhVien.DataSource, "chucvu");
            tbEmail.DataBindings.Clear();
            tbEmail.DataBindings.Add("text", dgvSinhVien.DataSource, "email");
            tbGhiChu.DataBindings.Clear();
            tbGhiChu.DataBindings.Add("text", dgvSinhVien.DataSource, "ngaysinh");
        }

        private void btThemNV_Click(object sender, EventArgs e)
        {

        }

        
    }
}

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
using System.Collections;

namespace quanlyktx
{
    public partial class Form1 : Form
    {

        DataClasses1DataContext db = new DataClasses1DataContext();
        int TIENDIEN = 0;
        int TIENNUOC = 0;

        public Form1()
        {
            InitializeComponent();
        }
        private void btDangNhap_Click_1(object sender, EventArgs e)
        {
            bool fl=false;
            if (tbUsername.Text == "" || tbPassword.Text == "")
            {
                Utils.showOkDialog(Utils.TITLE_ERROR, "Vui lòng nhập đầy đủ thông tin!");
            }
            else
            {
                foreach (var r in db.nhanvien_selectbyemail(tbUsername.Text))
                {
                    if (r.matkhau == tbPassword.Text)
                    {
                        //login success
                        loadDB();
                        afterLoggedIn();
                        fl = true;
                    }
                }
                if (!fl)
                {
                    //login fail
                    Utils.showOkDialog(Utils.TITLE_INFO, "Sai tài khoản hoặc mật khẩu!");
                }
            }
        }

        //public async Task LoadingAsync()
        //{
        //    pbLoading.Visible = true;
        //    Task<int> longRunningTask = LongRunningOperationAsync();
        //    int result = await longRunningTask;
        //    afterLoggedIn();
        //}

        //public async Task<int> LongRunningOperationAsync() // assume we return an int from this long running operation 
        //{
        //    await Task.Run(() => loadDB());
        //    return 1;
        //}

        public void afterLoggedIn()
        {
            pnMain.Visible = true;
            pbLoading.Visible = false;
        }

        void loadDB()
        {
            //loadDbSinhVien();
            //loadDBNV();
            dgvSinhVien.BeginInvoke(new MethodInvoker(loadDbSinhVien));
            dgvNhanVien.BeginInvoke(new MethodInvoker(loadDBNV));
            dgvNhanVien.BeginInvoke(new MethodInvoker(loadDBPhong));
            loadTienDienNuoc();
        }

        void loadTienDienNuoc()
        {
            foreach(var r in db.quydinh_selectall())
            {
                TIENDIEN = r.tiendien;
                TIENNUOC = r.tiennuoc;
            }
        }
        private void loadDbSinhVien()
        {
            dgvSinhVien.DataSource = db.sinhvien_selectall();
            addBindingSV();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////

        private void addBindingSV()
        {
            tbMaSV.DataBindings.Clear();
            tbMaSV.DataBindings.Add("text", dgvSinhVien.DataSource, "masv");
            tbHoTenSV.DataBindings.Clear();
            tbHoTenSV.DataBindings.Add("text", dgvSinhVien.DataSource, "hoten");
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
            AddSVDialog addDialog = new AddSVDialog();
            addDialog.ShowDialog();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            AddSVDialog editDialog = new AddSVDialog();
            editDialog.add_tbMaSinhVien.Text = tbMaSV.Text;
            editDialog.add_tbHoTen.Text = tbHoTenSV.Text;
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
                Utils.showOkDialog(Utils.TITLE_INFO, "Thêm thành công!");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Utils.showOkDialog(Utils.TITLE_ERROR, e.Message);
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
                Utils.showOkDialog(Utils.TITLE_INFO, "Sửa thành công!");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Utils.showOkDialog(Utils.TITLE_ERROR, e.Message);
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
            DialogResult result = MessageBox.Show("Bạn chắc chắn xóa chứ?", Utils.TITLE_INFO, bt);
            if (result == DialogResult.Yes)
            {
                try
                {
                    db.sinhvien_delete(tbMaSV.Text);
                    Utils.showOkDialog(Utils.TITLE_INFO, "Xóa thành công!");
                }
                catch (SqlException ee)
                {
                    Utils.showOkDialog(Utils.TITLE_ERROR, ee.Message);
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
            tbMaNV.DataBindings.Add("text", dgvNhanVien.DataSource, "manv");
            tbHoTenNV.DataBindings.Clear();
            tbHoTenNV.DataBindings.Add("text", dgvNhanVien.DataSource, "hoten");
            tbMatKhau.DataBindings.Clear();
            tbMatKhau.DataBindings.Add("text", dgvNhanVien.DataSource, "matkhau");
            tbChucVu.DataBindings.Clear();
            tbChucVu.DataBindings.Add("text", dgvNhanVien.DataSource, "chucvu");
            tbEmail.DataBindings.Clear();
            tbEmail.DataBindings.Add("text", dgvNhanVien.DataSource, "email");
            tbGhiChu.DataBindings.Clear();
            tbGhiChu.DataBindings.Add("text", dgvNhanVien.DataSource, "ghichu");
        }

        private void btThemNV_Click(object sender, EventArgs e)
        {
            AddNVDialog addnvdialog = new AddNVDialog();
            addnvdialog.ShowDialog();
        }

        public void addNV(string s1,string s2,string s3,string s4,string s5,string s6)
        {
            try
            {
                db.nhanvien_insert(s1, s2, s3, s4, s5, s6);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Utils.showOkDialog(Utils.TITLE_ERROR, e.Message);
            }
            finally
            {
                loadDBNV();
            }
        }

        public void editNV(string s1, string s2, string s3, string s4, string s5, string s6)
        {
            try
            {
                db.nhanvien_update(s1, s2, s3, s4, s5, s6);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Utils.showOkDialog(Utils.TITLE_ERROR, e.Message);
            }
            finally
            {
                loadDBNV();
            }
        }
        public void deleteNV(string s1)
        {
            try
            {
                db.nhanvien_delete(s1);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Utils.showOkDialog(Utils.TITLE_ERROR, e.Message);
            }
            finally
            {
                loadDBNV();
            }
        }

        private void btSuaNV_Click(object sender, EventArgs e)
        {
            AddNVDialog editDialog = new AddNVDialog();
            editDialog.add_tbMaNV.Text = tbMaNV.Text;
            editDialog.add_tbHoTenNV.Text = tbHoTenNV.Text;
            editDialog.add_tbMatKhau.Text = tbMatKhau.Text;
            editDialog.add_tbChucVu.Text = tbChucVu.Text;
            editDialog.add_tbEmail.Text = tbEmail.Text;
            editDialog.add_tbGhiChu.Text = tbGhiChu.Text;

            editDialog.add_tbMaNV.ReadOnly = true;
            editDialog.add_btThemNV.Text = "Xong";
            editDialog.isEditing = true;
            editDialog.ShowDialog();
        }

        private void btXoaNV_Click(object sender, EventArgs e)
        {
            MessageBoxButtons bt = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("Bạn chắc chắn xóa chứ?", Utils.TITLE_INFO, bt);
            if (result == DialogResult.Yes)
            {
                try
                {
                    db.nhanvien_delete(tbMaNV.Text);
                    Utils.showOkDialog(Utils.TITLE_INFO, "Xóa thành công!");
                }
                catch (SqlException ee)
                {
                    Utils.showOkDialog(Utils.TITLE_ERROR, ee.Message);
                }
                finally
                {
                    loadDBNV();
                }

            }
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            IList r;
            if (rbSearchSVById.Checked)
            {
                r = db.sinhvien_selectid(tbSearch.Text).ToList();
            }
            else if (rbSearchSVByName.Checked)
            {
                r = db.sinhvien_searchbyname(tbSearch.Text).ToList();
            }else 
            {
                r = db.sinhvien_selectbymaphong(tbSearch.Text).ToList();
            }
            if (r.Count>0)
            {
                dgvSinhVien.DataSource = r;
                addBindingSV();
            }
            else
            {
                Utils.showOkDialog(Utils.TITLE_INFO, "Không tìm thấy!");
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            loadDbSinhVien();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////

        void loadDBPhong()
        {
            dgvPhong.DataSource = db.phong_selectall();
            addBindingPhong();
        }

        private void addBindingPhong()
        {
            phong_tbMaPhong.DataBindings.Clear();
            phong_tbMaPhong.DataBindings.Add("text", dgvPhong.DataSource, "maphong");
            phong_tbTenPhong.DataBindings.Clear();
            phong_tbTenPhong.DataBindings.Add("text", dgvPhong.DataSource, "tenphong");
            phong_tbMaTang.DataBindings.Clear();
            phong_tbMaTang.DataBindings.Add("text", dgvPhong.DataSource, "matang");
            phong_tbMaLoaiPhong.DataBindings.Clear();
            phong_tbMaLoaiPhong.DataBindings.Add("text", dgvPhong.DataSource, "maloaiphong");
            phong_tbSLMax.DataBindings.Clear();
            phong_tbSLMax.DataBindings.Add("text", dgvPhong.DataSource, "slmax");
            phong_tbSLDangO.DataBindings.Clear();
            phong_tbSLDangO.DataBindings.Add("text", dgvPhong.DataSource, "sldango");
            //phong_tbSoDien
            int dien = Convert.ToInt32(dgvPhong.Rows[0].Cells[7].Value.ToString()) - Convert.ToInt32(dgvPhong.Rows[0].Cells[6].Value.ToString());
            phong_tbSoDien.Text = dien.ToString();
            int nuoc = Convert.ToInt32(dgvPhong.Rows[0].Cells[9].Value.ToString()) - Convert.ToInt32(dgvPhong.Rows[0].Cells[8].Value.ToString());
            phong_tbSoNuoc.Text = nuoc.ToString();
            phong_tbTienDien.Text = (dien * TIENDIEN).ToString();
            phong_tbTienNuoc.Text = (nuoc * TIENNUOC).ToString();

            phong_tbTinhTrang.DataBindings.Clear();
            phong_tbTinhTrang.DataBindings.Add("text", dgvPhong.DataSource, "tinhtrang");
            phong_tbGioiTinh.DataBindings.Clear();
            phong_tbGioiTinh.DataBindings.Add("text", dgvPhong.DataSource, "gioitinh");
            //tbThoiGianHoc.DataBindings.Clear();
        }
        public void addPhong(string s1,string s2,string s3, string s4,int i1,int i2,int i3,int i4,int i5,string s5,string s6)
        {
            db.phong_insert(s1, s2, s3, s4, i1, i2, i3, i4, i5, s5, s6);
        }
        public void editPhong(string s1,string s2,string s3,string s4,int i1,int i2, int i3, int i4, int i5, int i6, int i7,string s5, string s6)
        {
            db.phong_update(s1, s2, s3, s4, i1, i2, i3, i4, i5, i6, i7, s5, s6);
        }
        public void deletePhong(string s1)
        {
            db.phong_delete(s1);
        }

        private void btThemPhong_Click(object sender, EventArgs e)
        {
            AddPhongDialog addDialog = new AddPhongDialog();
            addDialog.ShowDialog();
        }

        private void btSuaPhong_Click(object sender, EventArgs e)
        {
            EditPhong editDialog = new EditPhong();
            editDialog.add_tbMaPhong.Text = phong_tbMaPhong.Text;
            editDialog.add_tbTenPhong.Text = phong_tbTenPhong.Text;
            editDialog.add_tbMaTang.Text = phong_tbMaTang.Text;
            editDialog.add_tbMaLoaiPhong.Text = phong_tbMaLoaiPhong.Text;
            editDialog.add_tbSLMax.Text = phong_tbSLMax.Text;
            editDialog.add_tbSLDangO.Text = phong_tbSLDangO.Text;

            foreach (var r in db.phong_selectbymaphong(phong_tbMaPhong.Text)) {
                editDialog.add_tbSoDienThangTruoc.Text = r.sodienthangtruoc.ToString();
                editDialog.add_tbSoDienThangSau.Text = r.sodienthangsau.ToString();
                editDialog.add_tbSoNuocThangTruoc.Text = r.sonuocthangtruoc.ToString();
                editDialog.add_tbSoNuocThangSau.Text = r.sonuocthangsau.ToString();
                editDialog.add_tbSLTaiSan.Text = r.sltaisan.ToString();
            }
            editDialog.add_tbTinhTrang.Text = phong_tbTinhTrang.Text;
            editDialog.add_tbGioiTinh.Text = phong_tbGioiTinh.Text;

            editDialog.ShowDialog();
        }

        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvPhong.CurrentCell.RowIndex;
            int dien = Convert.ToInt32(dgvPhong.Rows[i].Cells[7].Value.ToString()) - Convert.ToInt32(dgvPhong.Rows[i].Cells[6].Value.ToString());
            int nuoc = Convert.ToInt32(dgvPhong.Rows[i].Cells[9].Value.ToString()) - Convert.ToInt32(dgvPhong.Rows[i].Cells[8].Value.ToString());
            phong_tbSoDien.Text = dien.ToString();
            phong_tbSoNuoc.Text = nuoc.ToString();

            phong_tbTienDien.Text = (dien * TIENDIEN).ToString();
            phong_tbTienNuoc.Text = (nuoc * TIENNUOC).ToString();
        }
    }
}

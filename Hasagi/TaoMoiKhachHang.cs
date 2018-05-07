using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Hasagi
{
    public partial class frmAdd : Form
    {
        public static string UsertName = "";
        public string kq, kq2,nhankq,nhankq2;
        public static string ma, gt, loai;
        public  int phidv=10000, tongtien=50000;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dangnhap1"].ConnectionString);
        public frmAdd()
        {
            InitializeComponent();
    
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {
            lbHello.Text = UsertName;
            con.Open();
            string phong = "select * from LoaiPhong";

            SqlDataAdapter ad = new SqlDataAdapter(phong, con);
            DataSet tb = new DataSet();
            ad.Fill(tb);
            cbLoai.DataSource = tb.Tables[0];
            cbMa.DataSource = tb.Tables[0];

            cbLoai.DisplayMember = "LoaiPhong";
            cbLoai.ValueMember = "IdLoai";
            cbMa.DisplayMember = "MaPhong";
            cbMa.ValueMember = "MaPhong";
            con.Close();
            
          

        }
        private void Radio(){
            if (rbtnNam.Checked == true)

                gt = "Nam";
            if (rbtnNu.Checked == true)
                gt = "Nữ";

            if (rbtnDon.Checked == true)

                loai = "Đơn";
            if (rbtnNhom.Checked == true)

                loai = "Nhóm";
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {

            Radio();
            if (tbHo.Text == "" || tbTen.Text == "" || tbSDT.Text == "" || tbEmail.Text == "" || tbDiaChi.Text == "" || tbCMND.Text == "")
            {
                CheckNull(errorProvider1, tbHo);
                CheckNull(errorProvider1, tbTen);
                CheckNull(errorProvider1, tbSDT);
                CheckNull(errorProvider1, tbCMND);
                CheckNull(errorProvider1, tbEmail);
                CheckNull(errorProvider1, tbDiaChi);
                CheckEmail();
                btnXacNhan.Enabled = false;
            }
            else
            {
                btnXacNhan.Enabled = true;

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dangnhap1"].ConnectionString);
                    con.Open();
                    string Add = "INSERT INTO KhachHang(Hoten,CMND,DiaChi,SoDienThoai,MaThuePhong,GioiTinh,NgayThue,DonNhom,PhiDichVu,NgayTra,TongThanhToan,Email,GhiChu) VALUES(N'" + tbHo.Text+ " " + tbTen.Text + "','" + tbCMND.Text + "',N'" + tbDiaChi.Text + "','" + tbSDT.Text + "','" + cbMa.SelectedValue.ToString() + "',N'" + gt + "','" + dateTimeThue.Value.ToString("MM/dd/yyyy") + "',N'" + loai + "','" + phidv + "','" + dateTimeTra.Value.ToString("MM/dd/yyyy") + "','" + tongtien + "','" + tbEmail.Text + "','" + txtGhiChu.Text + "') ";

                    SqlCommand cmd = new SqlCommand(Add, con);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm Thành Công");
                    frmKH frmkh = new frmKH();
                    this.Hide();
                    frmkh.Show();
                }
                catch
                {
                    MessageBox.Show("lỗi");
                }

            }
        }

   

        private void btnXacNhan_MouseHover(object sender, EventArgs e)
        {
            btnXacNhan.BackColor = System.Drawing.Color.DarkTurquoise;
        }

        private void btnXacNhan_MouseLeave(object sender, EventArgs e)
        {
            btnXacNhan.BackColor = System.Drawing.Color.White;
        }

    
 

   
    
       
        private void CheckNull(ErrorProvider ep, TextBox tb)
        {
            if (tb.Text == "")
            {
                ep.SetError(tb, "lỗi");
                btnXacNhan.Enabled = false;
            }
            else
            {
                btnXacNhan.Enabled = true;
           
                
                
            }
        }
        private void CheckEmail()
        {
            string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            Regex reg = new Regex(match);
            if (reg.IsMatch(this.tbEmail.Text))
            {

                btnXacNhan.Enabled = true;
                errorProvider1.Clear();
               

            }
            else
            {
                errorProvider1.SetError(tbEmail, "Sai định dạng Email");
                btnXacNhan.Enabled = false;
            }
        }
        private void CheckSDT(TextBox tb1)
        {
            int n = 0;
            if (int.TryParse(tb1.Text, out n) && tb1.Text.Length <= 11|| tbSDT.Text=="")
            {
                btnXacNhan.Enabled = true;
              
                errorProvider1.Clear();
                CheckNull(errorProvider1, tbHo);
                CheckNull(errorProvider1, tbTen);
                CheckNull(errorProvider1, tbCMND);
            }
            else
            {
                errorProvider1.SetError(tbSDT, "Sai định dạng SĐT");
                btnXacNhan.Enabled = false;
            }
        }
        private void CheckCMND(TextBox tb2)
        {
            int n = 0;
            if (int.TryParse(tb2.Text, out n) && tb2.Text.Length == 9 || tb2.Text.Length == 11 || tbCMND.Text=="")
            {
                btnXacNhan.Enabled = true;
                tbCMND.BackColor = System.Drawing.Color.White;
                errorProvider1.Clear();
                CheckNull(errorProvider1, tbHo);
                CheckNull(errorProvider1, tbTen);
            }
            else
            {
                errorProvider1.SetError(tbCMND, "Số CMND phải 9 hoặc 11 số");
                btnXacNhan.Enabled = false;
            }
        }

        private void frmAdd_MouseLeave(object sender, EventArgs e)
        {
         
            if (tbHo.Text == "" || tbTen.Text == "" || tbSDT.Text== "" || tbEmail.Text == "" || tbDiaChi.Text == "" || tbCMND.Text == "")
            {
      
                btnXacNhan.Enabled = false;
            }
            else
            {
                btnXacNhan.Enabled = true;
            }
            
        }


        private void tbCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
          
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            
        }

        private void tbSDT_KeyPress(object sender, KeyPressEventArgs e)
        {

            CheckSDT(tbSDT);
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
           
        }


        private void tbSDT_Click(object sender, EventArgs e)
        {
            
                CheckNull(errorProvider1, tbHo);
                CheckNull(errorProvider1,tbTen);

                CheckNull(errorProvider1, tbCMND);
             

        }

        private void tbSDT_KeyUp(object sender, KeyEventArgs e)
        {
            CheckSDT(tbSDT);
        
        }

      

        private void tbCMND_KeyUp(object sender, KeyEventArgs e)
        {
            CheckCMND(tbCMND);
        }

        private void tbEmail_Click(object sender, EventArgs e)
        {
                CheckNull(errorProvider1, tbHo);
                CheckNull(errorProvider1,tbTen);
                CheckNull(errorProvider1,tbSDT);
                CheckNull(errorProvider1,tbCMND);
                CheckNull(errorProvider1,tbDiaChi);
                
           
        }

        private void tbCMND_Click(object sender, EventArgs e)
        {
           
            if (tbHo.Text == "" || tbTen.Text == "" ||tbCMND.Text == "")
            {
                CheckNull(errorProvider1, tbHo);
                CheckNull(errorProvider1,tbTen);
          
                btnXacNhan.Enabled = false;

            }
            else
            {
                btnXacNhan.Enabled = true;
              
            }
        }

        private void tbHo_Click(object sender, EventArgs e)
        {
            tbHo.BackColor = System.Drawing.Color.White;
        }

     
        private void tbHo_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbHo.Text == "")
            {
                CheckNull(errorProvider1, tbHo);
            }
            else
            {
                errorProvider1.Clear();
                CheckNull(errorProvider1, tbTen);
            }
           
        }

        private void tbDiaChi_Click(object sender, EventArgs e)
        {

            if (tbHo.Text == "" || tbTen.Text == "" || tbSDT.Text == "" || tbCMND.Text == "")
            {
                CheckNull(errorProvider1,tbHo);
               CheckNull(errorProvider1,tbTen);
                CheckNull(errorProvider1,tbSDT);
                CheckNull(errorProvider1,tbCMND);
                btnXacNhan.Enabled = false;
                tbEmail.BackColor = System.Drawing.Color.White;
            }
            else
            {
                btnXacNhan.Enabled = true;
            }
        }

  

        private void tbEmail_KeyUp(object sender, KeyEventArgs e)
        {
            CheckEmail();
        }

        private void tbTen_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbTen.Text == "")
            {
                CheckNull(errorProvider1, tbTen);
            }
            else
            {
                errorProvider1.Clear();
                CheckNull(errorProvider1, tbHo);
            }
        }

        private void tbDiaChi_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbDiaChi.Text == "")
            {
                CheckNull(errorProvider1, tbDiaChi);
            }
            else
            {
                errorProvider1.Clear();
                CheckNull(errorProvider1, tbHo);
                CheckNull(errorProvider1, tbTen);
                CheckNull(errorProvider1, tbSDT);
                CheckNull(errorProvider1, tbCMND);
            }
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            frmKH frmKH = new frmKH();
            this.Hide();
            frmKH.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmDn frmdn = new frmDn();
            this.Hide();
            frmdn.Show();
        }

      




    }
}

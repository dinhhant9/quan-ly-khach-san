using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Hasagi
{
    public partial class frmDn : Form
    {
        public frmDn()
        {
            InitializeComponent();
        }
        public static string UsertName = "";
        
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dangnhap1"].ConnectionString);
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
           

            try
            {
                con.Open();
                string sql = "Select * from dangnhap where UserName = '" + txtUser.Text + "' and Pass='" + txtPass.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();


                if (dr.Read() == true)
                {
                    this.Hide();
                    
                    frmMHmain frmMHmain = new frmMHmain();
                    frmMHmain.UsertName = txtUser.Text;
                    frmKH.UsertName = txtUser.Text;
                    frmAdd.UsertName = txtUser.Text;
                    
                    frmMHmain.Show();
                }
                else
                {
                    MessageBox.Show("cuc m, biến.. cút ttt");
                }


            }

            catch
            {
                MessageBox.Show("Lỗi, ko thê kết nối");
            }
            finally
            {
                con.Close();
            }

        }

        private void frmDn_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn thoát?", "Màn Hình Đăng Nhập", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (kq == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void frmDn_Load(object sender, EventArgs e)
        {
            btnDangNhap.Enabled = false;
            btnDangNhap.BackColor = System.Drawing.Color.DarkSlateGray;

        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPass.Text != "")
            {
                btnDangNhap.Enabled = true;
                btnDangNhap.BackColor = System.Drawing.Color.DarkTurquoise;
            }
            else
            {
                btnDangNhap.Enabled = false;
                btnDangNhap.BackColor = System.Drawing.Color.DarkSlateGray;
            }
        }

    }
}

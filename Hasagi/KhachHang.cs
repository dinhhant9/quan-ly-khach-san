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

namespace Hasagi
{
    public partial class frmKH : Form
    {
        public frmKH()
        {
            InitializeComponent();
        }
        public static string UsertName = "";
        private void btnBack_Click(object sender, EventArgs e)
        {
            frmMHmain frmMHmain = new frmMHmain();
            this.Hide();
            frmMHmain.Show();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dangnhap1"].ConnectionString);
        public void ketnoi()
         {
             con.Open();
             String hienthi = "SELECT * FROM KhachHang order by Ma DESC";
             SqlCommand cmd = new SqlCommand(hienthi, con);
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             DataTable tb = new DataTable();
             da.Fill(tb);
             con.Close();
             dgvDS.DataSource = tb;
             dgvDS.Refresh();
         }

        private void frmKH_Load(object sender, EventArgs e)
        {
            ketnoi();
            lbHello2.Text = UsertName;
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAdd frmAdd = new frmAdd();
            frmAdd.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDn frmdn = new frmDn();
            frmdn.Show();
        }
        
    }
}

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
    public partial class frmMHmain : Form
    {
        public frmMHmain()
        {
            InitializeComponent();
        }
        public static string UsertName = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dangnhap1"].ConnectionString);
        private void btnKH_Click(object sender, EventArgs e)
        {
            frmKH frmKH = new frmKH();
            this.Hide();
            frmKH.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmDn frmDN = new frmDn();
            this.Hide();
            frmDN.Show();
        }

        private void frmMHmain_Load(object sender, EventArgs e)
        {
            lbHello.Text = UsertName;
         
        }

     
     

    }
}
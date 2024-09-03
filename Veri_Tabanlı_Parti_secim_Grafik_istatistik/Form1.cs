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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Veri_Tabanlı_Parti_secim_Grafik_istatistik
{
    //Emirhan KARAKURT
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=FARUK\\SQLEXPRESS;Initial Catalog=DBSecimProje;Integrated Security=True;");

        private void btn_oygiris_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into TBLILCE (ILCEAD,APARTI,BPARTI,CPARTI,DPARTI,EPARTI) values (@P1,@P2,@P3,@P4,@P5,@P6) ", conn);
            cmd.Parameters.AddWithValue("@P1", txt_ilce.Text);
            cmd.Parameters.AddWithValue("@P2", txt_a.Text);
            cmd.Parameters.AddWithValue("@P3", txt_b.Text);
            cmd.Parameters.AddWithValue("@P4", txt_c.Text);
            cmd.Parameters.AddWithValue("@P5", txt_d.Text);
            cmd.Parameters.AddWithValue("@P6", txt_e.Text);

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Kayıtlar Güncellendi !");
        }

        private void btn_grafik_Click(object sender, EventArgs e)
        {
            FrmGrafikler frmGrafikler = new FrmGrafikler();
            frmGrafikler.Show();
        }

        private void btn_cıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("delete from TBLILCE where ILCEAD=@P1 ", conn);
            cmd.Parameters.AddWithValue("@P1", txt_ilce.Text);

            int returnValues = cmd.ExecuteNonQuery();
            if (returnValues == 1)
            {
                MessageBox.Show("İlçe Oy Bilgileri Silindi");
            }
            else 
            {
                MessageBox.Show("İşlem Başarısız");
            }
            conn.Close();
        }
    }
}

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

namespace Veri_Tabanlı_Parti_secim_Grafik_istatistik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=FARUK\\SQLEXPRESS;Initial Catalog=DBSecimProje;Integrated Security=True;");

        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            // ilçe adlarını comboBox'a çekme
            conn.Open();

            SqlCommand cmd = new SqlCommand("select ILCEAD from TBLILCE", conn);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                comboBox1.Items.Add(dr[0]);
            }
            conn.Close();

            // Grafiğe toplam sonuçları getirme

            conn.Open();
            SqlCommand cmd2 = new SqlCommand("select SUM(APARTI),SUM(BPARTI),SUM(CPARTI),SUM(DPARTI),SUM(EPARTI) from TBLILCE", conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A PARTİ", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("B PARTİ", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("C PARTİ", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("D PARTİ", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("E PARTİ", dr2[4]);
            }

            conn.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Series["Partiler"].Points.Clear(); // her seferinde tablyu temizleyip yine yine tekrardan getirimek istedim

            conn.Open();

            SqlCommand cmd = new SqlCommand("Select * from TBLILCE where ILCEAD=@P1", conn);
            cmd.Parameters.AddWithValue("@P1", comboBox1.Text);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                progressBar1.Value = int.Parse(dr[2].ToString());
                progressBar2.Value = int.Parse(dr[3].ToString());
                progressBar3.Value = int.Parse(dr[4].ToString());
                progressBar4.Value = int.Parse(dr[5].ToString());
                progressBar5.Value = int.Parse(dr[6].ToString());

                lbl_a.Text = dr[2].ToString();
                lbl_b.Text = dr[3].ToString();
                lbl_c.Text = dr[4].ToString();
                lbl_d.Text = dr[5].ToString();
                lbl_e.Text = dr[6].ToString();

                chart1.Series["Partiler"].Points.AddXY("A PARTİ", dr[2]);
                chart1.Series["Partiler"].Points.AddXY("B PARTİ", dr[3]);
                chart1.Series["Partiler"].Points.AddXY("C PARTİ", dr[4]);
                chart1.Series["Partiler"].Points.AddXY("D PARTİ", dr[5]);
                chart1.Series["Partiler"].Points.AddXY("E PARTİ", dr[6]);
            }
            conn.Close();
        }
    }
}

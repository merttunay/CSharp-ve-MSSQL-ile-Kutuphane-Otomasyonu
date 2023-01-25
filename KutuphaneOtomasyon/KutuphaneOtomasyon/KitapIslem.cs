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


using System.Collections;

namespace KutuphaneOtomasyon
{
    public partial class KitapIslem : Form
    {
        public KitapIslem()
        {
            InitializeComponent();
        }

        private void kitapgetir() 
        {
                 SqlConnection baglan = new SqlConnection(
              ConfigurationManager.ConnectionStrings["BaglantiMetni"].ToString());

            baglan.Open();


            string query = "SELECT * FROM Kitaplar";


            SqlCommand yonet = new SqlCommand(query, baglan);
            SqlDataAdapter adapter = new SqlDataAdapter(yonet);



            DataTable KitaplarTablosu = new DataTable();
            adapter.Fill(KitaplarTablosu);

            dataGridView1.DataSource = KitaplarTablosu;



        }
        private void kitapKaydet_Click(object sender, EventArgs e)
        {
          
            string query = "INSERT INTO Kitaplar (kitapSeriNo,kitapAd, kitapYazar, kitapBasimYili, kitapSayfaSayisi, kitapYayınevi,kitapDurum) VALUES (@kitapSeriNo,@kitapAd, @kitapYazar, @kitapBasimYili, @kitapSayfaSayisi, @kitapYayınevi,@kitapDurum)";

            SqlConnection baglan = new SqlConnection(
                ConfigurationManager.ConnectionStrings["BaglantiMetni"].ToString());

            SqlCommand yonet = new SqlCommand(query, baglan);

            string durum = "Mevcut";

            yonet.Parameters.AddWithValue("@kitapSeriNo", txtSeriNo.Text);
            yonet.Parameters.AddWithValue("@kitapAd", txtAd.Text);
            yonet.Parameters.AddWithValue("@kitapYazar", txtYazar.Text);
            yonet.Parameters.AddWithValue("@kitapBasimYili", txtBasimYili.Text);
            yonet.Parameters.AddWithValue("@kitapSayfaSayisi", txtSayfaSayi.Text);
            yonet.Parameters.AddWithValue("@kitapYayınevi", txtYayınevi.Text);
            yonet.Parameters.AddWithValue("@kitapDurum", durum);
            baglan.Open();
            yonet.ExecuteNonQuery();

            MessageBox.Show("Kitap kaydı yapıldı");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            /* EKLEME İŞLEMİ BİTTİKTEN SONRA GÜNCEL TABLOYU GETİRME İŞLEMİ */
            kitapgetir();

            yonet.Dispose();
            baglan.Close();
            baglan.Dispose();
           

        }

        private void KitapIslem_Load(object sender, EventArgs e)
        {
            kitapgetir();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                txtSeriNo.Text = selectedRow.Cells["kitapSeriNo"].Value.ToString();
                txtAd.Text = selectedRow.Cells["kitapAd"].Value.ToString();
                txtYazar.Text = selectedRow.Cells["kitapYazar"].Value.ToString();
                txtBasimYili.Text = selectedRow.Cells["kitapBasimYili"].Value.ToString();
                txtSayfaSayi.Text = selectedRow.Cells["kitapSayfaSayisi"].Value.ToString();
                txtYayınevi.Text = selectedRow.Cells["kitapYayınevi"].Value.ToString();

                txtSeriNo.Enabled = false;
                // ...
            }
        }

        private void kitapGuncelle_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Kitaplar SET kitapAd=@kitapAd, kitapYazar=@kitapYazar, kitapBasimYili=@kitapBasimYili, kitapSayfaSayisi=@kitapSayfaSayisi, kitapYayınevi=@kitapYayınevi  WHERE kitapSeriNo=@kitapSeriNo";
            SqlConnection baglan = new SqlConnection(
                ConfigurationManager.ConnectionStrings["BaglantiMetni"].ToString());
            SqlCommand yonet = new SqlCommand(query, baglan);
            yonet.Parameters.AddWithValue("@kitapSeriNo", txtSeriNo.Text);
            yonet.Parameters.AddWithValue("@kitapAd", txtAd.Text);
            yonet.Parameters.AddWithValue("@kitapYazar", txtYazar.Text);
            yonet.Parameters.AddWithValue("@kitapBasimYili", txtBasimYili.Text);
            yonet.Parameters.AddWithValue("@kitapSayfaSayisi", txtSayfaSayi.Text);
            yonet.Parameters.AddWithValue("@kitapYayınevi", txtYayınevi.Text);
            baglan.Open();
            yonet.ExecuteNonQuery();
         
            MessageBox.Show("Güncelleme İşlemi Başarılı");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }


            /* GÜNCELLEME İŞLEMİ BİTTİKTEN SONRA GÜNCEL TABLOYU GETİRME İŞLEMİ */
            kitapgetir();

            yonet.Dispose();
            baglan.Close();
            baglan.Dispose();

        }

        private void kitapSil_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

            SqlConnection baglan = new SqlConnection(
               ConfigurationManager.ConnectionStrings["BaglantiMetni"].ToString());

           

            baglan.Open();

            string query = "DELETE FROM Kitaplar WHERE kitapID = @kitapID";

          

            SqlCommand yonet = new SqlCommand(query, baglan);


            yonet.Parameters.AddWithValue("@kitapID", selectedRow.Cells["kitapID"].Value);
            yonet.ExecuteNonQuery();

            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            dataGridView1.Rows.RemoveAt(selectedRowIndex);

            MessageBox.Show("Kitap Silindi");

            kitapgetir();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }


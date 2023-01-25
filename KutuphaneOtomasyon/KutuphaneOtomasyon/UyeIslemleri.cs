using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyon
{
    public partial class UyeIslemleri : Form
    {
        public UyeIslemleri()
        {
            InitializeComponent();
        }

        /* VERİTABANINDAKİ UYELERTABLOSUNU FORMDAKİ DATAGRİEDVİEW'E AKTARAN FONKSİYON */
        private void uyelertablo()
        {

            SqlConnection baglan = new SqlConnection(
              ConfigurationManager.ConnectionStrings["BaglantiMetni"].ToString());

            string queryUyeler = "SELECT * FROM Uyeler";

            SqlCommand yonetUyeler = new SqlCommand(queryUyeler, baglan);
            SqlDataAdapter adapter = new SqlDataAdapter(yonetUyeler);


            DataTable UyelerTablosu = new DataTable();
            adapter.Fill(UyelerTablosu);

            dataGridView1.DataSource = UyelerTablosu;



        }
        private void uyeKaydet_Click(object sender, EventArgs e)
        {

            string query = "INSERT INTO Uyeler (uyeTC,uyeAdSoyad,uyeDTarih, uyeTelefon, uyeEmail, uyeAdres) VALUES (@uyeTC,@uyeAdSoyad, @uyeDTarih, @uyeTelefon, @uyeEmail, @uyeAdres)";

            SqlConnection baglan = new SqlConnection(
                ConfigurationManager.ConnectionStrings["BaglantiMetni"].ToString());

            SqlCommand yonet = new SqlCommand(query, baglan);

            yonet.Parameters.AddWithValue("@uyeTC", txtTC.Text);
            yonet.Parameters.AddWithValue("@uyeAdSoyad", txtAdSoyad.Text);
            yonet.Parameters.AddWithValue("@uyeDTarih", txtDogumTarih.Text);
            yonet.Parameters.AddWithValue("@uyeTelefon", txtTelefon.Text);
            yonet.Parameters.AddWithValue("@uyeEmail", txtEmail.Text);
            yonet.Parameters.AddWithValue("@uyeAdres", txtAdres.Text);
            baglan.Open();
            yonet.ExecuteNonQuery();

            MessageBox.Show("Üye kaydı yapıldı");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            // EKLEME İŞLEMİ BİTTİKTEN SONRA GÜNCEL TABLOYU GETİRME İŞLEMİ
            uyelertablo();


            yonet.Dispose();
            baglan.Close();
            baglan.Dispose();
     
        }

        private void uyeGuncelle_Click(object sender, EventArgs e)
        {
            /* PARAMETRE KULLANARAK VERİ TABANINDAKİ*/
            string query = "UPDATE Uyeler SET uyeTC=@uyeTC, uyeAdSoyad=@uyeAdSoyad, uyeDTarih=@uyeDTarih, uyeTelefon=@uyeTelefon, uyeEmail=@uyeEmail,uyeAdres=@uyeAdres  WHERE uyeTC=@uyeTC";
            SqlConnection baglan = new SqlConnection(
                ConfigurationManager.ConnectionStrings["BaglantiMetni"].ToString());
            SqlCommand yonet = new SqlCommand(query, baglan);

            yonet.Parameters.AddWithValue("@uyeTC", txtTC.Text);
            yonet.Parameters.AddWithValue("@uyeAdSoyad", txtAdSoyad.Text);
            yonet.Parameters.AddWithValue("@uyeDTarih", txtDogumTarih.Text);
            yonet.Parameters.AddWithValue("@uyeTelefon", txtTelefon.Text);
            yonet.Parameters.AddWithValue("@uyeEmail", txtEmail.Text);
            yonet.Parameters.AddWithValue("@uyeAdres", txtAdres.Text);
            baglan.Open();
            yonet.ExecuteNonQuery();

            MessageBox.Show("Güncelleme İşlemi Başarılı");
            
            // FOREACH İLE BULUNDUĞUMUZ FORMDAKİ TOLLARI KONTROL EDİYOR EĞER BULDUĞU TOOL TEXTBOX İSE İÇİNİ TEMİZLİYOR
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }


            /* GÜNCELLEME İŞLEMİ BİTTİKTEN SONRA GÜNCEL TABLOYU GETİRME İŞLEMİ */
            uyelertablo();

            yonet.Dispose();
            baglan.Close();
            baglan.Dispose();
            /**********************************************************************/
        }

        private void uyeSil_Click(object sender, EventArgs e)
        {
            
            int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

            SqlConnection baglan = new SqlConnection(
               ConfigurationManager.ConnectionStrings["BaglantiMetni"].ToString());



            baglan.Open();

            // SİLME İŞLEMİ UYEID SİNNE GÖRE YAPILIR
            string query = "DELETE FROM Uyeler WHERE UyeID = @UyeID";



            SqlCommand yonet = new SqlCommand(query, baglan);

            // DATAGRİDVİEWDEN SEÇİLEN SATIRIN UYEID SÜTUNUNA GÖRE VERİTABANINDAN İLGİLİ SATIRI SİLER
            yonet.Parameters.AddWithValue("@UyeID", selectedRow.Cells["UyeID"].Value);
            yonet.ExecuteNonQuery();

            // FOREACH İLE BULUNDUĞUMUZ FORMDAKİ TOLLARI KONTROL EDİYOR EĞER BULDUĞU TOOL TEXTBOX İSE İÇİNİ TEMİZLİYOR
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            dataGridView1.Rows.RemoveAt(selectedRowIndex);

            MessageBox.Show("Uye Silindi");
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            // FORMU KAPATIR
            this.Close();
        }


        private void UyeIslemleri_Load(object sender, EventArgs e)
        {
            //FORM AÇILDIĞINDA OTOMATİK OLARAK uyelertablo FONKSİYONU ÇALIŞIR
            uyelertablo();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            /* DATAGRİDVİEW DE SEÇTİĞİMİZ SATIRIN BİLGİLİLERİNİ İLGİLİ TEXTBOXLARA DOLDURUYOR*/ 
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];


                txtTC.Text = selectedRow.Cells["uyeTC"].Value.ToString();
                txtAdSoyad.Text = selectedRow.Cells["uyeAdSoyad"].Value.ToString();
                txtDogumTarih.Text = selectedRow.Cells["uyeDTarih"].Value.ToString();
                txtTelefon.Text = selectedRow.Cells["uyeTelefon"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["uyeEmail"].Value.ToString();
                txtAdres.Text = selectedRow.Cells["uyeAdres"].Value.ToString();

                //üyenin tc si değiştirilmesin diye false yapıyoruz
                txtTC.Enabled = false;

            }
        }
    }
}

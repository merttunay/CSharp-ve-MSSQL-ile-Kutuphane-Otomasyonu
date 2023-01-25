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
    public partial class Emanet : Form
    {
        public Emanet()
        {
            InitializeComponent();
        }

        private void emntKaydet_Click(object sender, EventArgs e)
        {
            // SQL BAĞLANTISI YAPIYOR
            SqlConnection baglan = new SqlConnection(
   ConfigurationManager.ConnectionStrings["BaglantiMetni"].ToString());
            baglan.Open();


            string durum = "Emanet Verildi";

            //FORMA EKLEDİĞİMİZ TOOLLARDAN YAPILAN SEÇİMLERİ VERİLERE AKTARIYOR
            string UyeAd = comboBox1.SelectedItem.ToString();
            string KitapAd = comboBox2.SelectedItem.ToString();
            DateTime EmanetVerisTarih = dateTimePicker1.Value;
            DateTime EmanetAlisTarih = dateTimePicker2.Value;

            //PARAMETRELİ SQL KOMUTU OLUŞTURUYORUZ
            SqlCommand commandMevcut = new SqlCommand("update Kitaplar set kitapDurum=@kitapDurum WHERE kitapAd=@KitapAd ", baglan);
            SqlCommand command = new SqlCommand("INSERT INTO Emanet (UyeAd, KitapAd, EmanetVerisTarih,EmanetAlisTarih) VALUES (@UyeAd, @KitapAd, @EmanetVerisTarih,@EmanetAlisTarih)", baglan);
            
            //PARAMETRE KULLANARAK VERİ AKTARIMI YAPIYORUZ
            command.Parameters.AddWithValue("@UyeAd", UyeAd);
            command.Parameters.AddWithValue("@KitapAd", KitapAd);
            command.Parameters.AddWithValue("@EmanetVerisTarih", EmanetVerisTarih);
            command.Parameters.AddWithValue("@EmanetAlisTarih", EmanetAlisTarih);
            commandMevcut.Parameters.AddWithValue("@kitapDurum", durum);
            commandMevcut.Parameters.AddWithValue("@KitapAd", KitapAd);

           
            command.ExecuteNonQuery();
            commandMevcut.ExecuteNonQuery();


            /* DATAGRİEDVİEW'E SQLDEKİ EMANET TABLOSUNU AKTARIR*/
            string queryEmanet = "SELECT * FROM Emanet";


            SqlCommand yonetEmanet = new SqlCommand(queryEmanet, baglan);
            SqlDataAdapter adapter = new SqlDataAdapter(yonetEmanet);



            DataTable EmanetTablosu = new DataTable();
            adapter.Fill(EmanetTablosu);

            dataGridView1.DataSource = EmanetTablosu;
            /***************************************************/

            yonetEmanet.Dispose();
            baglan.Close();
            baglan.Dispose();
        }

        private void Emanet_Load(object sender, EventArgs e)
        {
            // SQL BAĞLANTISI YAPIYOR
            SqlConnection baglan = new SqlConnection(
    ConfigurationManager.ConnectionStrings["BaglantiMetni"].ToString());

            baglan.Open();

            //SQL SORGUSU YAZIP STİRNG DEĞERE ATIYORUZ
            string sqlUye = "SELECT uyeAdSoyad FROM Uyeler";

            // SQL SORGUSUNU VE BAĞLANTISI KULLANIP SQL SQK KOMUTU OLUŞTURUYORUZ
            SqlCommand yonetUye = new SqlCommand(sqlUye, baglan);
           
            //SQL KOMUTUNU ÇALIŞTIRIP VERİTABANINDAN VERİ ÇEKİYORUZ
            SqlDataReader readerUye = yonetUye.ExecuteReader();

            //WHİLE DÖNÜGÜSÜ İLE TABLO İÇİNDEKİ VERİ SATIRLARINI OKUYORUZ
            while (readerUye.Read())
            {
                //VERİ TABANINDAN ÇEKİLEN ÜYEADSOYAD SÜTUNUNUN VERİLERİNİ COMBOBOXA EKLİYORUZ
                comboBox1.Items.Add(readerUye["uyeAdSoyad"].ToString());

            }
            readerUye.Close();

            //SQL SORGUSU YAZIP STİRNG DEĞERE ATIYORUZ
            string sqlKitap = "SELECT kitapAd FROM Kitaplar";

            // SQL SORGUSUNU VE BAĞLANTISI KULLANIP SQL SQK KOMUTU OLUŞTURUYORUZ
            SqlCommand yonetKitap = new SqlCommand(sqlKitap, baglan);

            //SQL KOMUTUNU ÇALIŞTIRIP VERİTABANINDAN VERİ ÇEKİYORUZ
            SqlDataReader readerKitap = yonetKitap.ExecuteReader();

            //WHİLE DÖNÜGÜSÜ İLE TABLO İÇİNDEKİ VERİ SATIRLARINI OKUYORUZ
            while (readerKitap.Read())
            {
                //VERİ TABANINDAN ÇEKİLEN KİTAPAD SÜTUNUNUN VERİLERİNİ COMBOBOXA EKLİYORUZ
                comboBox2.Items.Add(readerKitap["kitapAd"].ToString());
            }
            readerKitap.Close();

            //SQL SORGUSU YAZIP STİRNG DEĞERE ATIYORUZ
            string queryEmanet = "SELECT * FROM Emanet";

            // SQL SORGUSUNU VE BAĞLANTISI KULLANIP SQL SQK KOMUTU OLUŞTURUYORUZ
            SqlCommand yonetEmanet = new SqlCommand(queryEmanet, baglan);

            //SQL KOMUTUNU ÇALIŞTIRIR
            SqlDataAdapter adapter = new SqlDataAdapter(yonetEmanet);

            //DATATABLE OLUŞTURUYORUZ
            DataTable EmanetTablosu = new DataTable();

            // SQLADAPTER KULLANARAK ÇALIŞTIRDIĞIMIZ SORGU SONUCUNDA DÖNEN VERİLERLE DATATABLE'IMIZI DOLDURUYORUZ
            adapter.Fill(EmanetTablosu);

            //OLUŞTURDUĞUMUZ DATATABLE'I DATAGRİEDVİEW'E EKLİYORUZ
            dataGridView1.DataSource = EmanetTablosu;

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyon
{
    public partial class EmanetVerilenKitaplar : Form
    {
        public EmanetVerilenKitaplar()
        {
            InitializeComponent();
        }

        private void EmanetVerilenKitaplar_Load(object sender, EventArgs e)
        {

            SqlConnection baglan = new SqlConnection(
                           ConfigurationManager.ConnectionStrings["BaglantiMetni"].ToString());

            baglan.Open();

            string durum = "Emanet Verildi";
            string query = "SELECT * FROM Kitaplar WHERE kitapDurum = 'Emanet Verildi' ";


            SqlCommand yonet = new SqlCommand(query, baglan);
            SqlDataAdapter adapter = new SqlDataAdapter(yonet);



            DataTable KitaplarTablosu = new DataTable();
            adapter.Fill(KitaplarTablosu);

            dataGridView1.DataSource = KitaplarTablosu;
        }
    }
}

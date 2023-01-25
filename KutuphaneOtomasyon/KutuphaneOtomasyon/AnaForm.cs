using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyon
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ÜYE İŞLEMLERİ FORMUNU AÇAR
            UyeIslemleri uyeis = new UyeIslemleri();
            uyeis.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //KİTAP EMANET İŞLEMLERİ FORMUNU AÇAR
            Emanet emanet = new Emanet();
            emanet.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //KİTAP İŞLEMLERİ FORMUNU AÇAR
            KitapIslem kitis = new KitapIslem();
            kitis.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //EMANET VERİLEN KİTAPLARIN GÖSTERİLDİĞİ GORMU AÇAR
            EmanetVerilenKitaplar emntkitap = new EmanetVerilenKitaplar();
            emntkitap.ShowDialog();
        }
    }
}

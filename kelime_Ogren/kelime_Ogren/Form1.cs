using System;
using System.Windows.Forms;
using System.Data.OleDb;

namespace kelime_Ogren
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\mert-\Masaüstü\dbSozluk.accdb");
        Random rast = new Random();
        int sure = 300;
        int kelime = 0;

        void getir()
        {
            int sayi;
            sayi = rast.Next(1, 2490);


            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * from sozluk where id=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", sayi);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtİngilizce.Text = dr[1].ToString();
                lblCevap.Text = dr[2].ToString();
                lblCevap.Text = lblCevap.Text.ToLower();
            }
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            getir();
            txtTürkce.Focus();
            timer1.Start();
        }

        private void txtTürkce_TextChanged(object sender, EventArgs e)
        {
            if(txtTürkce.Text==lblCevap.Text)
            {
                kelime++;
                lblKelime.Text = kelime.ToString();
                getir();
                txtTürkce.Clear();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure--;
            lblSure.Text = sure.ToString();
            if(sure==0)
            {
                txtTürkce.Enabled = false;
                txtİngilizce.Enabled = false;
                timer1.Stop();
            }
        }
    }
}

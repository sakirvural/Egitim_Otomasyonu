using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Egitim_Otomasyonu
{
    
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataReader okuma;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string kullanici = textBox1.Text;
            //string sifre = textBox2.Text;
            baglanti = new SqlConnection("server=; Initial Catalog=;Integrated Security=SSPI");
            komut = new SqlCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "SELECT * FROM KULLANICI where kullaniciadi='" + textBox1.Text + "' AND kullanicisifre='" + textBox2.Text+ "'";
            okuma = komut.ExecuteReader();
            if (okuma.Read())
            {
                Form2 frm2 = new Form2();
                frm2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
            }
            baglanti.Close();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Başarılı bir şekilde çıkış yaptınız.");
            Application.Exit();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

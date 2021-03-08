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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        static string conString = "Server=dogan;Database=dogansakir;Uid=sa;Password=;";

        SqlConnection baglanti = new SqlConnection(conString);

        private void kayit()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into CALISAN (CALISAN_AD_SOYAD,CALISAN_TC,CALISAN_TEL,CALISAN_ADRES,CALISAN_STATU,CALISAN_BRANS) values (@CALISAN_AD_SOYAD,@CALISAN_TC,@CALISAN_TEL,@CALISAN_ADRES,@CALISAN_STATU,@CALISAN_BRANS)";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@CALISAN_AD_SOYAD", textBox2.Text);
                komut.Parameters.AddWithValue("@CALISAN_TC", textBox4.Text);
                komut.Parameters.AddWithValue("@CALISAN_TEL", textBox5.Text);
                komut.Parameters.AddWithValue("@CALISAN_ADRES", richTextBox1.Text);
                komut.Parameters.AddWithValue("@CALISAN_STATU", comboBox1.Text);
                komut.Parameters.AddWithValue("@CALISAN_BRANS", comboBox2.Text);


                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                baglanti.Close();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                richTextBox1.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }
        private void güncelleme()
        {

            baglanti.Open();
            string guncelleme = "update CALISAN set CALISAN_AD_SOYAD=@CALISAN_AD_SOYAD,CALISAN_TC=@CALISAN_TC,CALISAN_TEL=@CALISAN_TEL,CALISAN_ADRES=@CALISAN_ADRES,CALISAN_STATU=@CALISAN_STATU,CALISAN_BRANS=@CALISAN_BRANS where CALISAN_ID=@CALISAN_ID";
            // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
            SqlCommand komut = new SqlCommand(guncelleme, baglanti);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            komut.Parameters.AddWithValue("@CALISAN_AD_SOYAD", textBox2.Text);
            komut.Parameters.AddWithValue("@CALISAN_TC", textBox4.Text);
            komut.Parameters.AddWithValue("@CALISAN_TEL", textBox5.Text);
            komut.Parameters.AddWithValue("@CALISAN_ADRES", richTextBox1.Text);
            komut.Parameters.AddWithValue("@CALISAN_STATU", comboBox1.Text);
            komut.Parameters.AddWithValue("@CALISAN_BRANS", comboBox2.Text);
            komut.Parameters.AddWithValue("@CALISAN_ID", textBox1.Text);

            //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
            komut.ExecuteNonQuery();
            //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
            baglanti.Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi.");
        }
        private void sil()
        {


            SqlCommand sorgu = new SqlCommand("Delete from CALISAN where CALISAN_ID='" + textBox1.Text + "'", baglanti);
            baglanti.Open();
            sorgu.ExecuteNonQuery();
            baglanti.Close();
        }
        private void yeni()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            richTextBox1.Text = "";


        }
        private void kayitGetir()
        {
            baglanti.Open();
            string doldur = "SELECT * FROM CALISAN";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            SqlCommand komut = new SqlCommand(doldur, baglanti);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            SqlDataAdapter adt = new SqlDataAdapter(komut);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            DataTable dt = new DataTable();
            adt.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView1.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            baglanti.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            kayitGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                MessageBox.Show("Lütfen yeni kayıt yapınız!");
            }
            else
            {
                kayit();
                MessageBox.Show("Kayıt Yapıldı");
                kayitGetir();
                yeni();
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Silinecek kaydı seçiniz!");
            }
            else
            {
                sil();
                MessageBox.Show("kayıt silindi");
                kayitGetir();
                yeni();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Güncellenecek kaydı seçiniz!");
            }
            else
            {
                güncelleme();
                MessageBox.Show("Kayıt güncellendi");
                kayitGetir();
                yeni();
            }

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            yeni();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            DataTable tbl = new DataTable();
            string arama, kod;
            arama = textBox5.Text;
            kod = "Select * from CALISAN  where CALISAN_AD_SOYAD like '%" + textBox6.Text + "%'";
            SqlDataAdapter adptr = new SqlDataAdapter(kod, baglanti);
            adptr.Fill(tbl);
            baglanti.Close();
            dataGridView1.DataSource = tbl;
        }
    }
}

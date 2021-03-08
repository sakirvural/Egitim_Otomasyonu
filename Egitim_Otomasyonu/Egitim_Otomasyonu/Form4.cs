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
    public partial class Form4 : Form
    {
        static string conString = "Server=dogan;Database=dogansakir;Uid=sa;Password=;";

        SqlConnection baglanti = new SqlConnection(conString);

        public Form4()
        {
            InitializeComponent();
        }
        private void kayit()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into MUSTERI (MUSTERI_AD_SOYAD,MUSTERI_VERGI_TC,MUSTERI_VERGIADI,MUSTERI_TEL,MUSTERI_ADRES,MUSTERI_TUR) values (@MUSTERI_AD_SOYAD,@MUSTERI_VERGI_TC,@MUSTERI_VERGIADI,@MUSTERI_TEL,@MUSTERI_ADRES,@MUSTERI_TUR)";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@MUSTERI_AD_SOYAD", textBox2.Text);
                komut.Parameters.AddWithValue("@MUSTERI_VERGI_TC", textBox4.Text);
                komut.Parameters.AddWithValue("@MUSTERI_VERGIADI", textBox3.Text);
                komut.Parameters.AddWithValue("@MUSTERI_TEL", textBox5.Text);
                komut.Parameters.AddWithValue("@MUSTERI_ADRES", richTextBox1.Text);
                komut.Parameters.AddWithValue("@MUSTERI_TUR", comboBox1.Text);
                

                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                baglanti.Close();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                comboBox1.Text = "";
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
            string guncelleme = "update MUSTERI set MUSTERI_AD_SOYAD=@MUSTERI_AD_SOYAD,MUSTERI_VERGI_TC=@MUSTERI_VERGI_TC,MUSTERI_VERGIADI=@MUSTERI_VERGIADI,MUSTERI_TEL=@MUSTERI_TEL,MUSTERI_ADRES=@MUSTERI_ADRES,MUSTERI_TUR=@MUSTERI_TUR where MUSTERI_ID=@MUSTERI_ID";
            // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
            SqlCommand komut = new SqlCommand(guncelleme, baglanti);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            komut.Parameters.AddWithValue("@MUSTERI_AD_SOYAD", textBox2.Text);
            komut.Parameters.AddWithValue("@MUSTERI_VERGI_TC", textBox4.Text);
            komut.Parameters.AddWithValue("@MUSTERI_VERGIADI", textBox3.Text);
            komut.Parameters.AddWithValue("@MUSTERI_TEL", textBox5.Text);
            komut.Parameters.AddWithValue("@MUSTERI_ADRES", richTextBox1.Text);
            komut.Parameters.AddWithValue("@MUSTERI_TUR", comboBox1.Text);
            komut.Parameters.AddWithValue("@MUSTERI_ID", textBox1.Text);

            //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
            komut.ExecuteNonQuery();
            //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
            baglanti.Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi.");
        }
        private void sil()
        {


            SqlCommand sorgu = new SqlCommand("Delete from MUSTERI where MUSTERI_ID='" + textBox1.Text + "'", baglanti);
            baglanti.Open();
            sorgu.ExecuteNonQuery();
            baglanti.Close();
        }
        private void yeni()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            richTextBox1.Text = "";


        }
        private void kayitGetir()
        {
            baglanti.Open();
            string doldur = "SELECT * FROM MUSTERI";
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
        private void Form4_Load(object sender, EventArgs e)
        {
            kayitGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Yeni Kayıt Açın!");

            }
            else
            {
                kayit();
                MessageBox.Show("Kayıt Başarılı!");
                yeni();
                kayitGetir();
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Lütfen kayıt seçiniz!");
            }
            else
            {
                güncelleme();
                MessageBox.Show("Kayıt Başarı ile Güncellendi!");
                yeni();
                kayitGetir();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("lütfen kayıt seçiniz!");
            }
            else
            {
                sil();
                MessageBox.Show("Kayıt Başarı İle Silindi!");
                yeni();
                kayitGetir();
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                richTextBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            DataTable tbl = new DataTable();
            string arama, kod;
            arama = textBox5.Text;
            kod = "Select * from MUSTERI where MUSTERI_AD_SOYAD like '%" + textBox6.Text + "%'";
            SqlDataAdapter adptr = new SqlDataAdapter(kod, baglanti);
            adptr.Fill(tbl);
            baglanti.Close();
            dataGridView1.DataSource = tbl;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            yeni();
        }
    }
}

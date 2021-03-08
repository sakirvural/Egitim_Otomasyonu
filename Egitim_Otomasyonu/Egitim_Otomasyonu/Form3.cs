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
    public partial class Form3 : Form
    {
        static string conString = "Server=dogan;Database=dogansakir;Uid=sa;Password=;";

        SqlConnection baglanti = new SqlConnection(conString);

        public Form3()
        {
            InitializeComponent();
        }
        private void kayitGetir()
        {
            baglanti.Open();
            string doldur = "SELECT * FROM KULLANICI";
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
        private void kayit()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into KULLANICI (kullaniciadi,kullanicisifre,kullanici_ad_soyad) values (@kullaniciadi,@kullanicisifre,@kullanici_ad_soyad)";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@kullaniciadi", textBox3.Text);
                komut.Parameters.AddWithValue("@kullanicisifre", textBox4.Text);
                komut.Parameters.AddWithValue("@kullanici_ad_soyad", textBox2.Text);

                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                baglanti.Close();
           
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }
        private void sil()
        {
            

            SqlCommand sorgu = new SqlCommand("Delete from KULLANICI where id='" + textBox1.Text + "'",baglanti);
            baglanti.Open();
            sorgu.ExecuteNonQuery();
            baglanti.Close();
        }
        private void güncelleme()
        {

            baglanti.Open();
            string guncelleme = "update KULLANICI set kullaniciadi=@kullaniciadi,kullanicisifre=@kullanicisifre,kullanici_ad_soyad=@kullanici_ad_soyad where id=@id";
            // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
            SqlCommand komut = new SqlCommand(guncelleme, baglanti);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            komut.Parameters.AddWithValue("@kullaniciadi", textBox3.Text);
            komut.Parameters.AddWithValue("@kullanicisifre", textBox4.Text);
            komut.Parameters.AddWithValue("@kullanici_ad_soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@id", textBox1.Text);

            //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
            komut.ExecuteNonQuery();
            //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
            baglanti.Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
            kayitGetir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                MessageBox.Show("Yeni Kayıt Açınız!");
            }
            else
            {
                kayit();
                MessageBox.Show("Kayıt Yapıldı!");
                kayitGetir();
            }
        }
            

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            DataTable tbl = new DataTable();
            string arama, kod;
            arama = textBox5.Text;
            kod = "Select * from KULLANICI where kullanici_ad_soyad like '%" + textBox5.Text+ "%'";
            SqlDataAdapter adptr = new SqlDataAdapter(kod, baglanti);
            adptr.Fill(tbl);
            baglanti.Close();
            dataGridView1.DataSource = tbl;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Lütfen Silinecek Kullanıcı Seçiniz!");

            }
            else
            {
                sil();
                MessageBox.Show("Kullanıcı Silindi");
                kayitGetir();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Lütfen Güncellenecek Kullanıcıyı Seçiniz!");
            }
            else
            {
                güncelleme();
                kayitGetir();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
           
        }
    }
}

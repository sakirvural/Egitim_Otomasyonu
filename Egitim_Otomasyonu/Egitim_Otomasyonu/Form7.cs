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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        static string conString = "Server=dogan;Database=dogansakir;Uid=sa;Password;";

        SqlConnection baglanti = new SqlConnection(conString);
        private void calisan()
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT *FROM CALISAN WHERE CALISAN_STATU='EĞİTMEN' ";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["CALISAN_AD_SOYAD"]);
            }
            baglanti.Close();

        }
        private void müsteri()
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT *FROM MUSTERI ";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["MUSTERI_AD_SOYAD"]);
            }
            baglanti.Close();

        }
        private void egitim()
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT *FROM EGITIM ";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr["EGITIM_ADI"]);
            }
            baglanti.Close();

        }
        private void kayit()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into KAYIT (KAYIT_TARIH,EGITMEN_ADI,MUSTERI_ADI,EGITIM_ADI) values (@KAYIT_TARIH,@EGITMEN_ADI,@MUSTERI_ADI,@EGITIM_ADI)";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@KAYIT_TARIH",Convert.ToDateTime( dateTimePicker1.Text));
                komut.Parameters.AddWithValue("@EGITMEN_ADI", comboBox1.Text);
                komut.Parameters.AddWithValue("@MUSTERI_ADI", comboBox2.Text);
                komut.Parameters.AddWithValue("@EGITIM_ADI", comboBox3.Text);
               


                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                baglanti.Close();

            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }
        private void güncelleme()
        {

            baglanti.Open();
            string guncelleme = "update KAYIT set KAYIT_TARIH=@KAYIT_TARIH,EGITMEN_ADI=@EGITMEN_ADI,MUSTERI_ADI=@MUSTERI_ADI,EGITIM_ADI=@EGITIM_ADI where KAYIT_ID=@KAYIT_ID";
            // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
            SqlCommand komut = new SqlCommand(guncelleme, baglanti);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            komut.Parameters.AddWithValue("@KAYIT_TARIH",Convert.ToDateTime( dateTimePicker1.Text));
            komut.Parameters.AddWithValue("@EGITMEN_ADI", comboBox1.Text);
            komut.Parameters.AddWithValue("@MUSTERI_ADI", comboBox2.Text);
            komut.Parameters.AddWithValue("@EGITIM_ADI", comboBox3.Text);
            komut.Parameters.AddWithValue("@KAYIT_ID", textBox1.Text);


            //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
            komut.ExecuteNonQuery();
            //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
            baglanti.Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi.");
        }
        private void sil()
        {


            SqlCommand sorgu = new SqlCommand("Delete from KAYIT where KAYIT_ID='" + textBox1.Text + "'", baglanti);
            baglanti.Open();
            sorgu.ExecuteNonQuery();
            baglanti.Close();
        }
        private void yeni()
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";

        }
        private void kayitGetir()
        {
            baglanti.Open();
            string doldur = "SELECT * FROM KAYIT";
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
        private void Form7_Load(object sender, EventArgs e)
        {
            kayitGetir();
            calisan();
            egitim();
            müsteri();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
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

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
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

        private void button4_Click(object sender, EventArgs e)
        {
            yeni();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            DataTable tbl = new DataTable();
            string arama, kod;
            arama = textBox2.Text;
            kod = "Select * from KAYIT  where EGITIM_ADI like '%" + textBox2.Text + "%'";
            SqlDataAdapter adptr = new SqlDataAdapter(kod, baglanti);
            adptr.Fill(tbl);
            baglanti.Close();
            dataGridView1.DataSource = tbl;
        }
    }
}

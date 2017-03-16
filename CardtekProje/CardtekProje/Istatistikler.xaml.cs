using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CardtekProje
{
    /// <summary>
    /// Interaction logic for Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        public int chart_kontrol = 0;
        public int tutarlar = 0;
        public int tarihler = 0;
        public double toplam_kar = 0;
        public bool tutar,tarih;
        public bool[] kontrol = new bool[20];
        
        public Window5()
        {
            InitializeComponent();
            
            olcut_combobox.Items.Add("MCC");
            olcut_combobox.Items.Add("Kisiler");
            
            mcctablo.Visibility = Visibility.Hidden;
            kisiler_combobox.Visibility = Visibility.Hidden;
            musteri_ekle();

            Tutar_Aralik.Visibility = Visibility.Hidden;
            TutarMin.Visibility = Visibility.Hidden;
            TutarMax.Visibility = Visibility.Hidden;
            odenenpara.Visibility = Visibility.Hidden;
            Kar.Visibility = Visibility.Hidden;
            Bilgi.Visibility = Visibility.Hidden;
            Tarih_Aralik.Visibility = Visibility.Hidden;
            TarihBaslangic.Visibility = Visibility.Hidden;
            TarihBitis.Visibility = Visibility.Hidden;

            Olcut_kontrol.Visibility = Visibility.Hidden;
            mcc_kontrol.Visibility = Visibility.Hidden;
            kisi_kontrol.Visibility = Visibility.Hidden;
            tarih_wrong.Visibility = Visibility.Hidden;
            Tarih_kontrol.Visibility = Visibility.Hidden;
            tutar_kontrol.Visibility = Visibility.Hidden;
            tutar_wrong.Visibility = Visibility.Hidden;

            mcctablo.Items.Add("Bakkal");
            mcctablo.Items.Add("Kırtasiye");
            mcctablo.Items.Add("Giyim");
            mcctablo.Items.Add("Otomobil");
            mcctablo.Items.Add("Züccaciye");

            showChart();

        }
        private void musteri_ekle()
        {
            int dummy2;
            string dummy, dummy1;
            SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            connn.Open();
            SqlCommand bul = new SqlCommand("Select ID,AD,SOYAD from Musteri_Tab", connn);
            SqlDataReader Read = bul.ExecuteReader();
            while (Read.Read())
            {
                dummy2 = Read.GetInt32(0);
                dummy = Read.GetString(1);
                dummy = Regex.Replace(dummy, @"\s+", "");
                dummy1 = Read.GetString(2);
                dummy1 = Regex.Replace(dummy1, @"\s+", "");
                kisiler_combobox.Items.Add("" + dummy2 + " " + dummy + " " + dummy1);
            }
            connn.Close();
        }
        public void showChart()
        { 
            List<KeyValuePair<string, int>> MyValue = new List<KeyValuePair<string, int>>();
            MyValue.Add(new KeyValuePair<string, int>("Sen kimsin ya", 200));
            MyValue.Add(new KeyValuePair<string, int>("Management", 36));
            MyValue.Add(new KeyValuePair<string, int>("Development", 89));
            MyValue.Add(new KeyValuePair<string, int>("Support", 500));
            

            if (chart_kontrol ==0)
            {
                SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                connnn.Open();
                SqlCommand cmd = new SqlCommand("Select COUNT(m.ID),(Select COUNT(ID) from Islem_tablo),(select COUNT(ID) from Kampanyalar) from Musteri_Tab m", connnn);
                SqlDataReader Read = cmd.ExecuteReader();
                List<KeyValuePair<string, int>> IlkAcilis = new List<KeyValuePair<string, int>>();
                while(Read.Read())
                {
                    IlkAcilis.Add(new KeyValuePair<string, int>("Müsteri Sayisi", Read.GetInt32(0)));
                    IlkAcilis.Add(new KeyValuePair<string, int>("İşlem Sayisi", Read.GetInt32(1)) );
                    IlkAcilis.Add(new KeyValuePair<string, int>("Kampanya Sayisi", Read.GetInt32(2)));
                    break;
                }
                
                
                ColumnChart1.DataContext = IlkAcilis;
            }
            else
            {
                ColumnChart1.DataContext = MyValue;
                
            }
            chart_kontrol++;

        }
        private void Goster_Click(object sender,EventArgs e)
        {
            if (olcut_combobox.SelectedItem.Equals("Genel"))
            {
                if(tutar ==false && tarih == false)
                {
                    MessageBox.Show("Eksik veya yanlış bilgi girdiniz !");
                    return;
                }else
                {
                    MessageBox.Show("Genel Basladi");
                }
            }
            else
            {

                string tBitis = "";
                string tBaslangic = "";
                double max = 0;
                double min = 0;
                if (Tarih_Aralik.IsChecked.Value)
                {
                    if (tarih == false)
                    {
                        MessageBox.Show("Yanlış bilgi girdiniz !");
                        return;
                    }
                    else
                    {
                        tBaslangic = TarihBaslangic.SelectedDate.Value.ToString("yyyy-MM-dd");
                        tBitis = TarihBitis.SelectedDate.Value.ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    tBaslangic = "1000-01-01";
                    tBitis = "3000-01-01";
                }
                if (Tutar_Aralik.IsChecked.Value)
                {
                    if (tutar == false)
                    {
                        MessageBox.Show("Yanlış bilgi girdiniz !");
                        return;
                    }
                    else
                    {
                        min = double.Parse(TutarMin.Text);
                        max = double.Parse(TutarMax.Text);
                    }
                }
                else
                {
                    min = 0;
                    max = 111111111;
                }
                int i;
                if (kontrol[0] == true && kontrol[2] == true)
                {
                    mcctablo.Visibility = Visibility.Hidden;
                    odenenpara.Visibility = Visibility.Hidden;
                    for (i = 0; i < 20; i++)
                    {
                        kontrol[i] = false;
                    }
                    mcc_istatislik(min, max, tBaslangic, tBitis);
                    kisi_kontrol.Visibility = Visibility.Hidden;
                    mcc_kontrol.Visibility = Visibility.Hidden;
                    Olcut_kontrol.Visibility = Visibility.Hidden;
                    MessageBox.Show("" + mcctablo.SelectedItem + " Istatistikleri getirildi !");
                    TarihBaslangic.SelectedDate = null;
                    TarihBitis.SelectedDate = null;
                    TarihBaslangic.Visibility = Visibility.Hidden;
                    TarihBitis.Visibility = Visibility.Hidden;
                    TutarMax.Text = "";
                    TutarMin.Text = "";
                    TutarMax.Visibility = Visibility.Hidden;
                    TutarMin.Visibility = Visibility.Hidden;
                    tutar_kontrol.Visibility = Visibility.Hidden;
                    tutar_wrong.Visibility = Visibility.Hidden;
                    tarih_wrong.Visibility = Visibility.Hidden;
                    Tarih_kontrol.Visibility = Visibility.Hidden;
                    tutar = false;
                    tarih = false;
                    Tarih_Aralik.Visibility = Visibility.Hidden;
                    Tutar_Aralik.Visibility = Visibility.Hidden;
                    Tutar_Aralik.IsChecked = false;
                    Tarih_Aralik.IsChecked = false;


                    olcut_combobox.SelectedItem = "";
                   
                    mcctablo.Visibility = Visibility.Hidden;
                    
                    kisiler_combobox.Visibility = Visibility.Hidden;

                }
                else if (kontrol[1] == true && kontrol[3])
                {
                    
                    for (i = 0; i < 20; i++)
                    {
                        kontrol[i] = false;
                    }
                    MessageBox.Show("" +kisiler_combobox.SelectedItem +" Kişisinin istatisliği seçildi");
                    kisiler_istatislik(min, max, tBaslangic, tBitis);
                    kisiler_combobox.Visibility = Visibility.Hidden;
                    
                    TarihBaslangic.SelectedDate = null;
                    TarihBitis.SelectedDate = null;
                    TarihBaslangic.Visibility = Visibility.Hidden;
                    TarihBitis.Visibility = Visibility.Hidden;
                    TutarMax.Text = "";
                    TutarMin.Text = "";
                    TutarMax.Visibility = Visibility.Hidden;
                    TutarMin.Visibility = Visibility.Hidden;
                    tutar_kontrol.Visibility = Visibility.Hidden;
                    tutar_wrong.Visibility = Visibility.Hidden;
                    tarih_wrong.Visibility = Visibility.Hidden;
                    Tarih_kontrol.Visibility = Visibility.Hidden;
                    tutar = false;
                    tarih = false;
                    Tarih_Aralik.Visibility = Visibility.Hidden;
                    Tutar_Aralik.Visibility = Visibility.Hidden;
                    Tutar_Aralik.IsChecked = false;
                    Tarih_Aralik.IsChecked = false;

                    
                    kisiler_combobox.Visibility = Visibility.Hidden;
                    
                    mcctablo.Visibility = Visibility.Hidden;
                    kisi_kontrol.Visibility = Visibility.Hidden;
                    mcc_kontrol.Visibility = Visibility.Hidden;
                    Olcut_kontrol.Visibility = Visibility.Hidden;

                }
                else
                {
                    MessageBox.Show("Lutfen Kriter Secin");
                }

            }
        }
        private void kampanya_istatistik()
        {
            string secilenMCC = "" + mcctablo.SelectedItem;
            int i = 0;
            string[]  dizi= new string[10];
            SqlConnection conn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select KampanyaAdı,COUNT(*) from Cevaplar inner join Kampanyalar on Kampanyalar.ID = Cevaplar.KampanyaID where MCC = '" + secilenMCC + "' group by KampanyaAdı",conn);
            SqlDataReader Read = cmd.ExecuteReader();
            while(Read.Read())
            {

            }
        }
        private void mcc_istatislik(double min,double max,string tbaslangic,string tbitis)
        {
            toplam_kar = 0;
            List<KeyValuePair<string, double>> MyValue = new List<KeyValuePair<string, double>>();
            //kampanya_istatistik();
            string secilenMCC = "" + mcctablo.SelectedItem;
            SqlConnection conn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            conn.Open();
            int toplamMCC_islem = 0;
            double toplamTutar = 0;
            int taksitli_Islem = 0;
            int Taksitsiz_Islem = 0;
            int i = 0;
            SqlCommand cmd = new SqlCommand("Select Islem_tablo.Tutar,KGirdi From Islem_tablo  INNER JOIN IslemEk  on IslemEk.IslemID = Islem_tablo.ID  where MCC='"+secilenMCC+"' and Tutar Between " + min + "and " + max +"and Tarih Between '"+tbaslangic+"' and '"+tbitis+"'", conn);
            SqlDataReader read = cmd.ExecuteReader();
            while(read.Read())
            {
                toplamTutar = toplamTutar + read.GetDouble(0);
                i = read.GetInt32(1);
                if(i == 3)
                {
                    taksitli_Islem++;
                }

                toplamMCC_islem++;
            }
            read.Close();
            conn.Close();
            Taksitsiz_Islem = toplamMCC_islem - taksitli_Islem;
            odenenpara.Text = "Odenen Toplam Para: " + toplamTutar + " ₺";
            Bilgi.Text = mcctablo.SelectedItem + " Istatistikleri";
            Bilgi.Visibility = Visibility.Visible;
            odenenpara.Visibility = Visibility.Visible;
            MyValue.Add(new KeyValuePair<string, double>("Toplam ISayisi", toplamMCC_islem));
            MyValue.Add(new KeyValuePair<string, double>("KGirmiş I.Sayisi", taksitli_Islem));
            MyValue.Add(new KeyValuePair<string, double>("KGirmemiş I.Sayisi", Taksitsiz_Islem));

            SqlConnection data = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            data.Open();

            SqlCommand yeni = new SqlCommand("Select Kampanyalar.ID,KampanyaAdı,COUNT(*) from Cevaplar inner join Kampanyalar on Kampanyalar.ID = Cevaplar.KampanyaID inner join Islem_tablo on Cevaplar.IslemID = Islem_tablo.ID where Kampanyalar.MCC = '"+secilenMCC+"' and Islem_tablo.Tutar Between "+min+"and "+max+" and Islem_tablo.Tarih between '"+tbaslangic+"' and '"+tbitis+"'  group by KampanyaAdı,Kampanyalar.ID", data);
            SqlDataReader Read = yeni.ExecuteReader();
            string kad = "";
            int maxi = 0;
            int dummyint = 0;
            double dumy = 0;
            while (Read.Read())
            {
                dummyint = Read.GetInt32(0);
                kad = ""+dummyint + " " + Read.GetString(1);
                maxi = Read.GetInt32(2);
                dumy = Convert.ToDouble(maxi);
                MyValue.Add(new KeyValuePair<string, double>(kad,dumy));
            }

            Read.Close();
            
            data.Close();

            SqlConnection get = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            get.Open();
            SqlCommand komut = new SqlCommand("Select Musteri_Tab.ID,Musteri_Tab.Ad,Musteri_Tab.SOYAD,COUNT(*) From Islem_tablo inner join Musteri_Tab on Musteri_Tab.ID = Islem_tablo.MusteriID where MCC='"+secilenMCC+"' and Tutar Between "+min+" and "+max+" and Tarih between '"+tbaslangic+"' and '"+tbitis+"' group by Musteri_Tab.ID,Musteri_Tab.AD,Musteri_Tab.SOYAD", get);
            SqlDataReader oku = komut.ExecuteReader();
            while(oku.Read())
            {
                dummyint = oku.GetInt32(0);
                
                kad = "" + dummyint + " " + Regex.Replace(oku.GetString(1), @"\s+", "") + " " + Regex.Replace(oku.GetString(2), @"\s+", "");
                maxi = oku.GetInt32(3);
                dumy = Convert.ToDouble(maxi);
                MyValue.Add(new KeyValuePair<string, double>(kad, dumy));
            }

            
            get.Close();
            oku.Close();
            SqlConnection database = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            database.Open();
            SqlCommand cmdkomut = new SqlCommand("Select COUNT(*) from Cevaplar inner join Islem_tablo on Cevaplar.IslemID = Islem_tablo.ID where MCC='"+secilenMCC+"' and Tutar Between "+min+" and "+max+" and Tarih between '"+tbaslangic+"' and '"+tbitis+"' and Response = 1", database);
            SqlDataReader okuma = cmdkomut.ExecuteReader();
            while(okuma.Read())
            {
                maxi = okuma.GetInt32(0);
                dumy = Convert.ToDouble(maxi);
                MyValue.Add(new KeyValuePair<string, double>("Taksitlenmiş I.Sayisi", dumy));
            }
            okuma.Close();
            database.Close();
            

            SqlConnection sample = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            sample.Open();
            SqlCommand samplecmd = new SqlCommand("Select IslemID from Cevaplar inner join Islem_tablo on Islem_tablo.ID= Cevaplar.IslemID where MCC='" + secilenMCC + "' and Tutar between " + min + " and " + max + " and Tarih between '" + tbaslangic + "' and '" + tbitis + "' and Response=1", sample);
            SqlDataReader sampleRead = samplecmd.ExecuteReader();

            while(sampleRead.Read())
            {
                maxi = sampleRead.GetInt32(0);
                kar_hesapla(maxi);
            }
            Kar.Text = "Elde Edilen Toplam Kar:" + toplam_kar + " ₺";
            Kar.Visibility = Visibility.Visible;
            MyValue.Add(new KeyValuePair<string, double>("Taksitlenmemiş I.Sayisi", taksitli_Islem - dumy));
            ColumnChart1.DataContext = MyValue;
        }
        private void kisiler_istatislik(double min,double max,string tbaslangic,string tbitis)
        {
            toplam_kar = 0;
            List<KeyValuePair<string, double>> MyValue = new List<KeyValuePair<string, double>>();
            //kampanya_istatistik();
            string mundi = kisiler_combobox.SelectedItem.ToString();
            mundi = Regex.Match(mundi, @"\d+").Value;
            int id = Convert.ToInt32(mundi);
            SqlConnection conn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            conn.Open();
            int toplamMCC_islem = 0;
            double toplamTutar = 0;
            int taksitli_Islem = 0;
            int Taksitsiz_Islem = 0;
            int i = 0;
            SqlCommand cmd = new SqlCommand("Select Islem_tablo.Tutar, KGirdi From Islem_tablo  INNER JOIN IslemEk  on IslemEk.IslemID = Islem_tablo.ID  where MusteriID = "+id+" and Tutar Between "+min+" and "+max+" and Tarih Between '"+tbaslangic+"' and '"+tbitis+"'",conn);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                toplamTutar = toplamTutar + read.GetDouble(0);
                i = read.GetInt32(1);
                if (i == 3)
                {
                    taksitli_Islem++;
                }

                toplamMCC_islem++;
            }
            read.Close();
            conn.Close();
            Taksitsiz_Islem = toplamMCC_islem - taksitli_Islem;
            odenenpara.Text = "Odenen Toplam Para: " + toplamTutar + " ₺";
            Bilgi.Text = kisiler_combobox.SelectedItem + " Istatistikleri";
            Bilgi.Visibility = Visibility.Visible;
            odenenpara.Visibility = Visibility.Visible;
            MyValue.Add(new KeyValuePair<string, double>("Toplam ISayisi", toplamMCC_islem));
            MyValue.Add(new KeyValuePair<string, double>("KGirmiş I.Sayisi", taksitli_Islem));
            MyValue.Add(new KeyValuePair<string, double>("KGirmemiş I.Sayisi", Taksitsiz_Islem));
            SqlConnection data = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            data.Open();
            SqlCommand yeni = new SqlCommand("Select Kampanyalar.ID,KampanyaAdı,COUNT(*) from Cevaplar inner join Kampanyalar on Kampanyalar.ID = Cevaplar.KampanyaID inner join Islem_tablo on Cevaplar.IslemID = Islem_tablo.ID where Cevaplar.MusteriID = '" + id + "' and Islem_tablo.Tutar Between " + min + "and " + max + " and Islem_tablo.Tarih between '" + tbaslangic + "' and '" + tbitis + "'  group by KampanyaAdı,Kampanyalar.ID", data);
            SqlDataReader Read = yeni.ExecuteReader();
            string kad = "";
            int maxi = 0;
            int dummyint = 0;
            double dumy = 0;
            while (Read.Read())
            {
                dummyint = Read.GetInt32(0);
                kad = "" + dummyint + " " + Read.GetString(1);
                maxi = Read.GetInt32(2);
                dumy = Convert.ToDouble(maxi);
                MyValue.Add(new KeyValuePair<string, double>(kad, dumy));
            }

            Read.Close();

            data.Close();
            SqlConnection get = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            get.Open();
            SqlCommand komut = new SqlCommand("Select MCC,COUNT(*) from Islem_tablo where MusteriID="+id+" and Tutar between "+min+" and "+max+" and Tarih between '"+tbaslangic+"' and '"+tbitis+"' group by MCC;", get);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                kad = oku.GetString(0);
                dummyint = oku.GetInt32(1);
                dumy = Convert.ToDouble(dummyint);
                MyValue.Add(new KeyValuePair<string, double>(kad, dumy));
            }


            get.Close();
            oku.Close();

            SqlConnection database = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            database.Open();
            SqlCommand cmdkomut = new SqlCommand("Select COUNT(*) from Cevaplar inner join Islem_tablo on Cevaplar.IslemID = Islem_tablo.ID where Cevaplar.MusteriID='" + id + "' and Tutar Between " + min + " and " + max + " and Tarih between '" + tbaslangic + "' and '" + tbitis + "' and Response = 1", database);
            SqlDataReader okuma = cmdkomut.ExecuteReader();
            while (okuma.Read())
            {
                maxi = okuma.GetInt32(0);
                dumy = Convert.ToDouble(maxi);
                MyValue.Add(new KeyValuePair<string, double>("Taksitlenmiş I.Sayisi", dumy));
            }
            okuma.Close();
            database.Close();
            MyValue.Add(new KeyValuePair<string, double>("Taksitlenmemiş I.Sayisi", taksitli_Islem-dumy));

            SqlConnection sample = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            sample.Open();
            SqlCommand samplecmd = new SqlCommand("Select IslemID from Cevaplar inner join Islem_tablo on Islem_tablo.ID= Cevaplar.IslemID where Cevaplar.MusteriID='"+id+"' and Tutar between " + min + " and " + max + " and Tarih between '" + tbaslangic + "' and '" + tbitis + "' and Response=1", sample);
            SqlDataReader sampleRead = samplecmd.ExecuteReader();

            while (sampleRead.Read())
            {
                maxi = sampleRead.GetInt32(0);
                kar_hesapla(maxi);
            }
            Kar.Text = "Elde Edilen Toplam Kar:" + toplam_kar + " ₺";
            Kar.Visibility = Visibility.Visible;
            ColumnChart1.DataContext = MyValue;
        }
        private void kar_hesapla(int gelenID)
        {
            SqlConnection database = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            database.Open();
            SqlCommand yeni = new SqlCommand("Select SUM(TaksitTutari) - AVG(IslemTutari) as \"dummy\"  from taksit where IslemID="+gelenID, database);
            SqlDataReader read = yeni.ExecuteReader();
            
            while(read.Read())
            {
                var dummy =read.GetSqlMoney(0);
                toplam_kar = toplam_kar + Convert.ToDouble(dummy.Value);
                break;
            }
            
            database.Close();
            read.Close();
        }
        private void olcut_degisti(object sender,SelectionChangedEventArgs e)
        {
            
            if(olcut_combobox.SelectedItem.Equals("MCC"))
            {
                kontrol[0] = true;
                kontrol[1] = false;
                kontrol[3] = false;
                mcctablo.Visibility = Visibility.Visible;
                kisiler_combobox.Visibility = Visibility.Hidden;
                kisi_kontrol.Visibility = Visibility.Hidden;
                Olcut_kontrol.Visibility = Visibility.Visible;
                Tutar_Aralik.Visibility = Visibility.Hidden;
                TutarMin.Visibility = Visibility.Hidden;
                TutarMax.Visibility = Visibility.Hidden;
                TutarMin.Text = "";
                TutarMax.Text = "";
                TarihBaslangic.SelectedDate = null;
                TarihBitis.SelectedDate = null;

                Tarih_Aralik.Visibility = Visibility.Hidden;
                TarihBaslangic.Visibility = Visibility.Hidden;
                TarihBitis.Visibility = Visibility.Hidden;

            }
            if (olcut_combobox.SelectedItem.Equals("Kisiler"))
            {
                kontrol[1] = true;
                kontrol[0] = false;
                kontrol[2] = false;
                mcctablo.Visibility = Visibility.Hidden;
                kisiler_combobox.Visibility = Visibility.Visible;
                kisi_kontrol.Visibility = Visibility.Hidden;
                mcc_kontrol.Visibility = Visibility.Hidden;
                Olcut_kontrol.Visibility = Visibility.Visible;
                Tutar_Aralik.Visibility = Visibility.Hidden;
                TutarMin.Visibility = Visibility.Hidden;
                TutarMax.Visibility = Visibility.Hidden;
                TutarMin.Text = "";
                TutarMax.Text = "";
                TarihBaslangic.SelectedDate = null;
                TarihBitis.SelectedDate = null;

                Tarih_Aralik.Visibility = Visibility.Hidden;
                TarihBaslangic.Visibility = Visibility.Hidden;
                TarihBitis.Visibility = Visibility.Hidden;

            }
            if(olcut_combobox.SelectedItem.Equals("Genel"))
            {
                kontrol[4] = true;
                kontrol[0] = false;
                kontrol[3] = false;
                kontrol[2] = false;
                kontrol[1] = false;
                mcctablo.Visibility = Visibility.Hidden;
                kisiler_combobox.Visibility = Visibility.Hidden;
                kisi_kontrol.Visibility = Visibility.Hidden;
                Olcut_kontrol.Visibility = Visibility.Visible;
                Tutar_Aralik.Visibility = Visibility.Hidden;
                TutarMin.Visibility = Visibility.Hidden;
                TutarMax.Visibility = Visibility.Hidden;
                TutarMin.Text = "";
                TutarMax.Text = "";
                TarihBaslangic.SelectedDate = null;
                TarihBitis.SelectedDate = null;

                Tarih_Aralik.Visibility = Visibility.Hidden;
                TarihBaslangic.Visibility = Visibility.Hidden;
                TarihBitis.Visibility = Visibility.Hidden;

                Tutar_Aralik.Visibility = Visibility.Visible;


                Tarih_Aralik.Visibility = Visibility.Visible;

            }

        }
        private void mcctablo_Degisti(object sender,SelectionChangedEventArgs e)
        {
            kontrol[2] = true;
            mcc_kontrol.Visibility = Visibility.Visible;
            Tutar_Aralik.Visibility = Visibility.Visible;
            

            Tarih_Aralik.Visibility = Visibility.Visible;
            

        }
        private void kisilerCombobox_Degisti(object sender, SelectionChangedEventArgs e)
        {
            kontrol[3] = true;
            kisi_kontrol.Visibility = Visibility.Visible;
            Tutar_Aralik.Visibility = Visibility.Visible;


            Tarih_Aralik.Visibility = Visibility.Visible;
        }
        public void Kampanya_click(object sender, EventArgs e)
        {
            Window1 yeni = new Window1();
            yeni.Show();

        }
        public void Islem_click(object sender, EventArgs e)
        {
            Window2 yeni = new Window2();
            yeni.Show();
        }
        public void sms_click(object sender, EventArgs e)
        {
            MainWindow yeni = new MainWindow();
            yeni.Show();
        }
        public void Musteri_click(object sender, EventArgs e)
        {
            Window3 yeni = new Window3();
            yeni.Show();
        }
        private void tutar_aralik_kontrol()
        {
            double n;
            double m;
            bool isNumeric = double.TryParse(TutarMin.Text, out n);
            bool sayi = double.TryParse(TutarMax.Text, out m);
            if (!isNumeric || TutarMin.Text.Equals("") || n < 0)
            {
                tutar_wrong.Visibility = Visibility.Visible;
                tutar_kontrol.Visibility = Visibility.Hidden;
                tutar = false;

            }
            else if (!sayi || TutarMax.Text.Equals("") || m < 0)
            {
                tutar_wrong.Visibility = Visibility.Visible;
                tutar_kontrol.Visibility = Visibility.Hidden;
                tutar = false;
            }
            else
            {
                m = double.Parse(TutarMin.Text);
                n = double.Parse(TutarMax.Text);
                if (n <= m)
                {
                    tutar_wrong.Visibility = Visibility.Visible;
                    tutar_kontrol.Visibility = Visibility.Hidden;
                    tutar = false;

                }
            }

        }
        private void TutarMax_Birakti(object sender, RoutedEventArgs e)
        {
            double n;
            bool isNumeric = double.TryParse(TutarMax.Text, out n);


            if (!isNumeric || TutarMax.Text.Equals("") || n < 0)
            {
                tutar_wrong.Visibility = Visibility.Visible;
                tutar_kontrol.Visibility = Visibility.Hidden;
                tutar = false;
            }
            else
            {

                tutarlar++;
                if (tutarlar >= 2)
                {
                    tutar_wrong.Visibility = Visibility.Hidden;
                    tutar_kontrol.Visibility = Visibility.Visible;
                    tutar = true;
                    tutarlar++;
                    tutar_aralik_kontrol();
                }

            }
        }
        private void TutarMin_Birakti(object sender, RoutedEventArgs e)
        {
            double n;
            bool isNumeric = double.TryParse(TutarMin.Text, out n);


            if (!isNumeric || TutarMin.Text.Equals("") || n < 0)
            {
                tutar_wrong.Visibility = Visibility.Visible;
                tutar_kontrol.Visibility = Visibility.Hidden;
                tutar= false;
            }
            else
            {

                tutarlar++;
                if (tutarlar >= 2)
                {
                    tutar_wrong.Visibility = Visibility.Hidden;
                    tutar_kontrol.Visibility = Visibility.Visible;
                    tutar = true;
                    tutarlar++;
                    tutar_aralik_kontrol();
                }

            }
        }

        private void Tutar_acik(object sender,RoutedEventArgs e)
        {
            TutarMin.Visibility = Visibility.Visible;
            TutarMax.Visibility = Visibility.Visible;
        }

        private void Tutar_kapali(object sender, RoutedEventArgs e)
        {
            TutarMin.Visibility = Visibility.Hidden;
            TutarMax.Visibility = Visibility.Hidden;
            tutar_wrong.Visibility = Visibility.Hidden;
            tutar_kontrol.Visibility = Visibility.Hidden;
            tutar = false;
        }
        private void Tarih_Acik(object sender, RoutedEventArgs e)
        {
            TarihBaslangic.Visibility = Visibility.Visible;
            TarihBitis.Visibility = Visibility.Visible;
            tarih = false;
        }

        private void Tarih_kapali(object sender, RoutedEventArgs e)
        {
            TarihBaslangic.Visibility = Visibility.Hidden;
            TarihBitis.Visibility = Visibility.Hidden;
            tarih_wrong.Visibility = Visibility.Hidden;
            Tarih_kontrol.Visibility = Visibility.Hidden;
        }
        private void TarihBaslangic_Degisti(object sender, SelectionChangedEventArgs e)
        {
            tarihler++;
            if (tarihler >= 2)
            {
                tarih_wrong.Visibility = Visibility.Hidden;
                Tarih_kontrol.Visibility = Visibility.Visible;
                tarih = true;
                tarihler++;
                tarih_control();
            }
        }
        private void TarihBitis_Degisti(object sender, SelectionChangedEventArgs e)
        {
            tarihler++;
            if (tarihler >= 2)
            {
                tarih_wrong.Visibility = Visibility.Hidden;
                Tarih_kontrol.Visibility = Visibility.Visible;
                tarih = true;
                tarihler++;
                tarih_control();
            }
        }
        private void tarih_control()
        {
            if (TarihBaslangic.SelectedDate >= TarihBitis.SelectedDate || TarihBaslangic.SelectedDate == null || TarihBitis.SelectedDate == null)
            {
                tarih_wrong.Visibility = Visibility.Visible;
                Tarih_kontrol.Visibility = Visibility.Hidden;
                tarih= false;
                tarihler++;
            }
        }
    }
}

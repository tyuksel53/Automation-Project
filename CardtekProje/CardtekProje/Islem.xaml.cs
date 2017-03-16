using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CardtekProje
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public bool[] kontrol = new bool[4];
        public bool goster_tablo = true;
        public double id = 0;
        
        public Window2()
        {
            

            InitializeComponent();
            Mcc_comboBox.Items.Add("Bakkal");
            Mcc_comboBox.Items.Add("Kırtasiye");
            Mcc_comboBox.Items.Add("Giyim");
            Mcc_comboBox.Items.Add("Otomobil");
            Mcc_comboBox.Items.Add("Züccaciye");
            kapat();
            musteri_ekle();
            Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void musteri_ekle()
        {
            int dummy2;
            string dummy,dummy1;
            SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            connn.Open();
            SqlCommand bul = new SqlCommand("Select ID,AD,SOYAD from Musteri_Tab",connn);
            SqlDataReader Read = bul.ExecuteReader();
            while (Read.Read())
            {
                dummy2 = Read.GetInt32(0);
                dummy = Read.GetString(1);
                dummy = Regex.Replace(dummy, @"\s+", "");
                dummy1 = Read.GetString(2);
                dummy1 = Regex.Replace(dummy1, @"\s+", "");
                MusteriID_ComboBox.Items.Add("" + dummy2 + " " + dummy+ " " + dummy1);
            }
            connn.Close();
        }
        private void kapat()
        {
            Mcc_control.Visibility = Visibility.Hidden;
            Tutar_wrong.Visibility = Visibility.Hidden;
            Tutar_control.Visibility = Visibility.Hidden;
            Tarih_control.Visibility = Visibility.Hidden;
            Tarih_Wrong.Visibility = Visibility.Hidden;
            MusteriID_control.Visibility = Visibility.Hidden;
            int i = 0;
            for (i=0; i<4;i++)
            {
                kontrol[i] = false;
            }
        }
        private void Guncelle_click(object sender, RoutedEventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Tabladon Bir satir Secin");
            }
            else
            {
                int i = 0;
                int z = 0;
                for (i = 0; i < 4; i++)
                {
                    if (kontrol[i])
                    {
                        z++;
                    }
                }

                if (z <= 3)
                {
                    MessageBox.Show("Eksik Veya Yanlış Doldurdugunuz Bilgiler Var(Hepsinin Yanında Tik işareti olduğundan Emin olun) !");
                }

                else
                {
                    string CmdString = string.Empty;
                    string musteri = MusteriID_ComboBox.SelectedItem.ToString();
                    musteri = Regex.Match(musteri, @"\d+").Value;
                    int mus = int.Parse(musteri);
                    SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                    connnn.Open();
                    CmdString = "Update Islem_tablo Set Tutar=@Tutar,Tarih=@Tarih,MCC=@MCC,MusteriID=@MusteriID where ID=" + id;
                    SqlCommand cmd = new SqlCommand(CmdString, connnn);

                    cmd.Parameters.AddWithValue("Tarih", Tarih.SelectedDate);
                    cmd.Parameters.AddWithValue("Tutar", Tutar_textbox.Text);
                    cmd.Parameters.AddWithValue("MCC", Mcc_comboBox.SelectedItem);
                    cmd.Parameters.AddWithValue("MusteriID", mus);

                    cmd.ExecuteNonQuery();
                    int max = Convert.ToInt32(id);
                    ek_guncelle(max);
                    connnn.Close();
                    MessageBox.Show("Kayit Guncellendi !");
                    Temizle.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    goster_tablo = true;
                    Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    id = 0;
                }
            }
        }

        private void Ekle_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int z = 0;
            for (i = 0; i < 4; i++)
            {
                if (kontrol[i])
                {
                    z++;
                }
            }

            if (z <= 3)
            {
                MessageBox.Show("Eksik Veya Yanlış Doldurdugunuz Bilgiler Var(Hepsinin Yanında Tik işareti olduğundan Emin olun) !");
            } else
                {
                    string CmdString = string.Empty;
                    SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                    connnn.Open();
                    string musteri = MusteriID_ComboBox.SelectedItem.ToString();
                    musteri = Regex.Match(musteri, @"\d+").Value;
                    int mus = int.Parse(musteri);
                    CmdString = "Insert Into Islem_tablo(Tutar,Tarih,MCC,MusteriID) Values(@Tutar,@Tarih,@MCC,@MusteriID)";
                    SqlCommand cmd = new SqlCommand(CmdString, connnn);        
                    cmd.Parameters.Add(new SqlParameter("Tarih", Tarih.SelectedDate));
                    cmd.Parameters.Add(new SqlParameter("Tutar", Tutar_textbox.Text));
                    cmd.Parameters.Add(new SqlParameter("MCC", Mcc_comboBox.SelectedItem));
                    cmd.Parameters.Add(new SqlParameter("MusteriID", mus));
                    
                    cmd.ExecuteNonQuery();
                    ID_Bul();
                    connnn.Close();
                    MessageBox.Show("Kayit Eklendi !");
                    Temizle.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    goster_tablo = true;
                    Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    id = 0;
                }


            }
        private void ID_Bul()
        {
            SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            connnn.Open();
            SqlCommand yeni = new SqlCommand("Select MAX(ID) from Islem_tablo", connnn);
            SqlDataReader Read = yeni.ExecuteReader();
            int max = 0;
            while(Read.Read())
            {
                max = Read.GetInt32(0);
            }
            ek(max);

        }
        private void ek(int gelenID)
        {
            SqlConnection data = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            data.Open();
            SqlCommand cmd = new SqlCommand("Insert Into IslemEk (ITarih,KGirdi,IslemID) Values(@ITarih,@KGirdi,@IslemID)", data);

            cmd.Parameters.Add(new SqlParameter("@ITarih", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@KGirdi", 1));
            cmd.Parameters.Add(new SqlParameter("@IslemID", gelenID));
            cmd.ExecuteNonQuery();
            data.Close();
        }
        private void ek_guncelle(int gelenID)
        {
            SqlConnection dummy = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            dummy.Open();
            SqlCommand cmd = new SqlCommand("Update IslemEk set ITarih=@ITarih,KGirdi=@KGirdi where IslemID="+gelenID, dummy);
            cmd.Parameters.Add(new SqlParameter("@ITarih", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@KGirdi", 1));
            cmd.ExecuteNonQuery();
            dummy.Close();
        }
        private void ek_sil(int gelenID)
        {
            SqlConnection dummy = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            dummy.Open();
            SqlCommand cmd = new SqlCommand("Delete from IslemEk where IslemID=" + gelenID, dummy);
            cmd.ExecuteNonQuery();
            dummy.Close();
        }
        private void Temizle_Click(object sender, RoutedEventArgs e)
        {
            Mcc_comboBox.SelectedItem = null;
            MusteriID_ComboBox.SelectedItem = null;
            Tutar_textbox.Text = "";
            Tarih.SelectedDate = null;
            kapat();
            id = 0;
            MessageBox.Show("Bilgiler Temizlendi !");
        }

        private void Sil_Click(object sender, RoutedEventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Tabloda bir satır seçin !");
            }
            else
            {
                SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                connn.Open();
                SqlCommand cmd = new SqlCommand("Delete from Islem_tablo where ID=" + id, connn);
                cmd.ExecuteNonQuery();
                connn.Close();
                int max = Convert.ToInt32(id);
                ek_sil(max);
                MessageBox.Show("Kayit Silindi !");
                Temizle.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                goster_tablo = true;
                Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                id = 0;
            }
        }

        private void Goster_Click(object sender, RoutedEventArgs e)
        {
            if (goster_tablo)
            {
                string CmdString = string.Empty;
                SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                connn.Open();
                CmdString = "SELECT ID,Tutar,Tarih,MCC,MusteriID FROM Islem_tablo";
                SqlCommand cmd = new SqlCommand(CmdString, connn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Islem_tablo");
                sda.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                connn.Close();
                goster_tablo = false;
            }
            else
            {
                dataGrid.ItemsSource = null;
                goster_tablo = true;
            }
        }
        private void Mcc_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Mcc_control.Visibility = Visibility.Visible;
            kontrol[1] = true;
        }
        private void Musteri_combobox(object sender, SelectionChangedEventArgs e)
        {
            MusteriID_control.Visibility = Visibility.Visible;
            kontrol[2] = true;
        }
        private void Tutar_Birakti(object sender,RoutedEventArgs e)
        {
            double n;
            bool isNumeric = double.TryParse(Tutar_textbox.Text, out n);


            if (!isNumeric || Tutar_textbox.Text.Equals("") || n <= 0)
            {
                Tutar_wrong.Visibility = Visibility.Visible;
                Tutar_control.Visibility = Visibility.Hidden;
                kontrol[0] = false;
            }
            else
            {
                Tutar_wrong.Visibility = Visibility.Hidden;
                Tutar_control.Visibility = Visibility.Visible;
                kontrol[0] = true;
            }
        }
        private void Tarih_birakti(object sender,SelectionChangedEventArgs e)
        {
            if(Tarih.SelectedDate == null)
            {
                Tarih_Wrong.Visibility = Visibility.Visible;
                Tarih_control.Visibility = Visibility.Hidden;
                kontrol[3] = false;
            }else
            {
                Tarih_Wrong.Visibility = Visibility.Hidden;
                Tarih_control.Visibility = Visibility.Visible;
                kontrol[3] = true;
            }
        }
        private void dataGrid_click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid.SelectedItem;
                id = double.Parse((drv["ID"]).ToString());
                SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                connn.Open();
                SqlCommand bul = new SqlCommand("Select Tarih,Tutar,MCC,MusteriID from Islem_tablo where ID=" + id, connn);
                SqlDataReader Read = bul.ExecuteReader();
                int musterid;
                while (Read.Read())
                {
                    Tarih.SelectedDate = Read.GetDateTime(0);
                    Tutar_textbox.Text = "" + Read.GetDouble(1).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                    Mcc_comboBox.SelectedItem = "" + Read.GetString(2);
                    musterid = Read.GetInt32(3);
                    musteri_olustur(musterid);
                }

                kapat();
                MessageBox.Show("Bilgiler Getirildi !");
                MusteriID_control.Visibility = Visibility.Visible;
                Mcc_control.Visibility = Visibility.Visible;
                kontrol[1] = true;
                kontrol[2] = true;

                connn.Close();

                //getir_control = true;

            }
            catch
            {

            }
        }
        private void musteri_olustur(int gelenid)
        {
            int dummy2;
            string dummy = "";
            string dummy1 = "";

            SqlConnection mmm = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            mmm.Open();
            SqlCommand cmd = new SqlCommand("Select ID,AD,SOYAD from Musteri_Tab", mmm);
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                dummy2 = oku.GetInt32(0);
                if (dummy2 == gelenid)
                {
                    dummy = oku.GetString(1);
                    dummy = Regex.Replace(dummy, @"\s+", "");
                    dummy1 = oku.GetString(2);
                    dummy1 = Regex.Replace(dummy1, @"\s+", "");
                    MusteriID_ComboBox.SelectedItem = "" + dummy2 + " " + dummy + " " + dummy1;
                    break;
                }

            }
            mmm.Close();
        }
        public void Kampanya_click(object sender, EventArgs e)
        {
            Window1 yeni = new Window1();
            yeni.Show();

        }
        public void Islem_click(object sender, EventArgs e)
        {
            MainWindow yeni = new MainWindow();
            yeni.Show();
        }
        public void sms_click(object sender, EventArgs e)
        {
            Window3 yeni = new Window3();
            yeni.Show();
        }
        public void istatislik_click(object sender, EventArgs e)
        {
            Window5 yeni = new Window5();
            yeni.Show();
        }
        
    }
}

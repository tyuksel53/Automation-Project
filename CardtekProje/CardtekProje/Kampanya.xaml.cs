using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

namespace CardtekProje
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public int kontrol = 0;
        public int clickCount = 0;
        public bool[] tb_check = new bool[7];
        public double id = 0;
        public double tik = 0;
        public int control_goster = 1;
        public bool goster_control = true;
        public int tutarlar = 0;
        public int tarihler = 0;
        public bool getir_control=true;
        public Window1()
        {
            InitializeComponent();
            Mcc_comboBox.Items.Add("Bakkal");
            Mcc_comboBox.Items.Add("Kırtasiye");
            Mcc_comboBox.Items.Add("Giyim");
            Mcc_comboBox.Items.Add("Otomobil");
            Mcc_comboBox.Items.Add("Züccaciye");
            Mcc_comboBox.Background = Brushes.LightSteelBlue;
            
            Taksit_Sayisi_Textbox.Background = Brushes.LightSteelBlue;
            Tarih_Baslangic.Background = Brushes.LightSteelBlue;
            Faiz_TextBox.Background = Brushes.LightSteelBlue;
            Ucret_TextBox.Background = Brushes.LightSteelBlue;
            KampanyaAdi_textbox.Background = Brushes.LightSteelBlue;
            TutarAraligi_textbox2.Background = Brushes.LightSteelBlue;
            TutarAraligi_textbox1.Background = Brushes.LightSteelBlue;
            kapat();
            Kampanya_Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void tutar_aralik_kontrol()
        {
            double n;
            double m;
            bool isNumeric = double.TryParse(TutarAraligi_textbox1.Text, out n);
            bool sayi = double.TryParse(TutarAraligi_textbox1.Text, out m);
            if (!isNumeric || TutarAraligi_textbox1.Text.Equals("") || n <= 0)
            {
                TutarAraligi_Wrong.Visibility = Visibility.Visible;
                TutarAraligi_control.Visibility = Visibility.Hidden;
                tb_check[0] = false;
                
            }else if(!sayi || TutarAraligi_textbox2.Text.Equals("") || m <= 0)
            {
                    TutarAraligi_Wrong.Visibility = Visibility.Visible;
                    TutarAraligi_control.Visibility = Visibility.Hidden;
                    tb_check[0] = false;  
            }else
            {
                m = double.Parse(TutarAraligi_textbox1.Text);
                n = double.Parse(TutarAraligi_textbox2.Text);
                if(n<=m)
                {
                    TutarAraligi_Wrong.Visibility = Visibility.Visible;
                    TutarAraligi_control.Visibility = Visibility.Hidden;
                    tb_check[0] = false;
                
                }
            }
            
        }
        public void basildi(object sender, RoutedEventArgs e)
        {
            

        }
        private void tabloyu_goster()
        {
            string CmdString = string.Empty;
            SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            connn.Open();
            CmdString = "SELECT ID,KampanyaAdı,TutarMin,TutarMax,TarihBaslangic,TarihBitis,TaksitSayisi,MCC,Ucret,Faiz FROM Kampanyalar";
            SqlCommand cmd = new SqlCommand(CmdString, connn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Kampanyalar");
            sda.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            connn.Close();
        }
        private void kapat()
        {
            
                      
            TaksitSayisi_control.Visibility = Visibility.Hidden;            
            TarihAraligi_control.Visibility = Visibility.Hidden;           
            KampanyaAdi_control.Visibility = Visibility.Hidden;           
            Faiz_kontrol.Visibility = Visibility.Hidden;           
            Ucret_control.Visibility = Visibility.Hidden;            
            MCC_control.Visibility = Visibility.Hidden;  
            
            TarihAraligi_Wrong.Visibility = Visibility.Hidden;
            TaksitSayisi_Wrong.Visibility = Visibility.Hidden;
            MCC_Wrong.Visibility = Visibility.Hidden;
            Faiz_Wrong.Visibility = Visibility.Hidden;
            Ucret_Wrong.Visibility = Visibility.Hidden;
            KampanyaAdı_Wrong.Visibility = Visibility.Hidden;
            TutarAraligi_Wrong.Visibility = Visibility.Hidden;   
            TutarAraligi_control.Visibility = Visibility.Hidden;
            int i = 0;
            for (i = 0; i < 7; i++)
            {
                tb_check[i] = false;
            }
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
                SqlCommand cmd = new SqlCommand("Delete from Kampanyalar where ID=" + id, connn);
                cmd.ExecuteNonQuery();
                connn.Close();
                int max = Convert.ToInt32(id);
                ek_sil(max);
                MessageBox.Show("Kayit Silindi !");
                string CmdString = string.Empty;
                goster_control = true;
                Kampanya_Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Temizle.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                

                id = 0;
            }
        }
        private void Goster_Click(object sender, RoutedEventArgs e)
        {
            if(goster_control)
            {
                string CmdString = string.Empty;
                SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                connn.Open();
                CmdString = "SELECT ID,KampanyaAdı,TutarMin,TutarMax,TarihBaslangic,TarihBitis,TaksitSayisi,MCC,Ucret,Faiz FROM Kampanyalar";
                SqlCommand cmd = new SqlCommand(CmdString, connn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Kampanyalar");
                sda.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                connn.Close();
                goster_control = false;
            }
            else
            {
                dataGrid.ItemsSource = null;
                goster_control = true;
            }
        }
        private void Ekle_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int z = 0;
            for (i = 0; i < 7; i++)
            {
                if (tb_check[i])
                {
                    z++;
                }
            }

            if (z <= 6)
            {
                MessageBox.Show("Eksik Veya Yanlış Doldurdugunuz Bilgiler Var(Hepsinin Yanında Tik işareti olduğundan Emin olun) !");
            }
            
                
                else
                {
                    string CmdString = string.Empty;
                    SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                    connnn.Open();
                    CmdString = "Insert Into Kampanyalar(KampanyaAdı,TutarMin,TutarMax,TarihBaslangic,TarihBitis,TaksitSayisi,MCC,Ucret,Faiz) Values(@KampanyaAdı,@TutarMin,@TutarMax,@TarihBaslangic,@TarihBitis,@TaksitSayisi,@MCC,@Ucret,@Faiz)";
                    SqlCommand cmd = new SqlCommand(CmdString, connnn);
                    cmd.Parameters.Add(new SqlParameter("KampanyaAdı", KampanyaAdi_textbox.Text));
                    cmd.Parameters.Add(new SqlParameter("TutarMin", TutarAraligi_textbox1.Text));
                    cmd.Parameters.Add(new SqlParameter("TutarMax", TutarAraligi_textbox2.Text));
                    cmd.Parameters.Add(new SqlParameter("TarihBaslangic", Tarih_Baslangic.SelectedDate));
                    cmd.Parameters.Add(new SqlParameter("TarihBitis", Tarih_Bitis.SelectedDate));
                    cmd.Parameters.Add(new SqlParameter("TaksitSayisi", Taksit_Sayisi_Textbox.Text));
                    cmd.Parameters.Add(new SqlParameter("MCC", Mcc_comboBox.SelectedItem.ToString()));
                    cmd.Parameters.Add(new SqlParameter("Ucret", Ucret_TextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("Faiz", Faiz_TextBox.Text));
                    cmd.ExecuteNonQuery();
                    bugun_tarih();

                    connnn.Close();
                    MessageBox.Show("Kayit Eklendi !");
                    clickCount = 0;
                    tabloyu_goster();
                    Temizle.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    id = 0;             
                }

            
        }
        private void bugun_tarih()
        {
            SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            connnn.Open();
            SqlCommand cmd = new SqlCommand("Select MAX(ID) From Kampanyalar", connnn);
            SqlDataReader read = cmd.ExecuteReader();
            int max = 0;
            while(read.Read())
            {
                max = read.GetInt32(0);
            }
            ek(max);
            connnn.Close();

        }
        private void ek(int gelenID)
        {
            SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            connnn.Open();
            SqlCommand cmd = new SqlCommand("Insert Into KampanyaEk(KTarih,KampanyaID) Values(@KTarih,@KampanyaID)", connnn);
            cmd.Parameters.Add(new SqlParameter("KTarih", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("KampanyaID", gelenID));
            cmd.ExecuteNonQuery();
            connnn.Close();
        }
        private void ek_sil(int gelenID)
        {
            SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            connnn.Open();
            SqlCommand cmd = new SqlCommand("Delete from KampanyaEk where KampanyaID=" + gelenID, connnn);
            cmd.ExecuteNonQuery();
            connnn.Close();

        }
        private void ek_guncelle(int gelenID)
        {
            SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            connnn.Open();
            SqlCommand yeni = new SqlCommand("Update KampanyaEk Set KTarih=@KTarih where KampanyaID=" + gelenID,connnn);
            yeni.Parameters.AddWithValue("Ktarih",DateTime.Now);
            yeni.ExecuteNonQuery();
            connnn.Close();
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
                for (i = 0; i < 7; i++)
                {
                    if (tb_check[i])
                    {
                        z++;
                    }
                }

                if (z <= 6)
                {
                    MessageBox.Show("Eksik Veya Yanlış Doldurdugunuz Bilgiler Var(Hepsinin Yanında Tik işareti olduğundan Emin olun) !");
                }
                
                    else
                    {
                        string CmdString = string.Empty;
                        SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                        connnn.Open();
                        CmdString = "Update Kampanyalar Set KampanyaAdı=@KampanyaAdı,TutarMin=@TutarMin,TutarMax=@TutarMax,TarihBaslangic=@TarihBaslangic,TarihBitis=@TarihBitis,TaksitSayisi=@TaksitSayisi,MCC=@MCC,Ucret=@Ucret,Faiz=@Faiz where ID=" + id;
                        SqlCommand cmd = new SqlCommand(CmdString, connnn);

                        
                        cmd.Parameters.AddWithValue("KampanyaAdı", KampanyaAdi_textbox.Text);
                        cmd.Parameters.AddWithValue("TutarMin", TutarAraligi_textbox1.Text);
                        cmd.Parameters.AddWithValue("TutarMax", TutarAraligi_textbox2.Text);
                        cmd.Parameters.AddWithValue("TarihBaslangic", Tarih_Baslangic.SelectedDate);
                        cmd.Parameters.AddWithValue("TarihBitis", Tarih_Bitis.SelectedDate);
                        cmd.Parameters.AddWithValue("TaksitSayisi", Taksit_Sayisi_Textbox.Text);
                        cmd.Parameters.AddWithValue("MCC", Mcc_comboBox.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("Ucret", Ucret_TextBox.Text);
                        cmd.Parameters.AddWithValue("Faiz", Faiz_TextBox.Text);

                        cmd.ExecuteNonQuery();
                        int max = Convert.ToInt32(id);
                        ek_guncelle(max);
                        connnn.Close();
                        MessageBox.Show("Kayit Guncellendi !");
                        Temizle.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        goster_control = true;
                        Kampanya_Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        id = 0;
                    }

                
            }
        }
        private void Tutar1_birakti(object sender, RoutedEventArgs e) 
        {
            double n;
            bool isNumeric = double.TryParse(TutarAraligi_textbox1.Text, out n);


            if (!isNumeric || TutarAraligi_textbox1.Text.Equals("") || n <= 0)
            {
                TutarAraligi_Wrong.Visibility = Visibility.Visible;
                TutarAraligi_control.Visibility = Visibility.Hidden;
                tb_check[0] = false;
            }
            else
            {
                
                tutarlar++;
                if(tutarlar>=2)
                {
                    TutarAraligi_Wrong.Visibility = Visibility.Hidden;
                    TutarAraligi_control.Visibility = Visibility.Visible;
                    tb_check[0] = true;
                    tutarlar++;
                    tutar_aralik_kontrol();
                }
                
            }
        }
        private void Tutar2_birakti(object sender, RoutedEventArgs e)
        {
            double n;
            bool isNumeric = double.TryParse(TutarAraligi_textbox2.Text, out n);


            if (!isNumeric || TutarAraligi_textbox2.Text.Equals("") || n <= 0)
            {
                TutarAraligi_Wrong.Visibility = Visibility.Visible;
                TutarAraligi_control.Visibility = Visibility.Hidden;
                tb_check[0] = false;
            }
            else
            {
                
                tutarlar++;
                if(tutarlar>=2)
                {
                    TutarAraligi_Wrong.Visibility = Visibility.Hidden;
                    TutarAraligi_control.Visibility = Visibility.Visible;
                    tb_check[0] = true;
                    tutarlar++;
                    tutar_aralik_kontrol();
                }
                
            }
        }
        
        private void KampanyaAdi_birakti(object sender, RoutedEventArgs e)
        {
            bool containsInt = KampanyaAdi_textbox.Text.Any(char.IsDigit);
            char[] SpecialChars = "!@#$%^&*><[]()/{}-_;.Ã‡Ã§ÄŸÅŸÅÃ–Ã¶ÃœÃ¼Ä°Ä±â€â€™~ ¨\\".ToCharArray();
            int indexOf = KampanyaAdi_textbox.Text.IndexOfAny(SpecialChars);
            if (containsInt || KampanyaAdi_textbox.Text.Equals("") || indexOf != -1)
            {
                KampanyaAdı_Wrong.Visibility = Visibility.Visible;
                KampanyaAdi_control.Visibility = Visibility.Hidden;
                tb_check[1] = false;
            }
            else
            {
                KampanyaAdı_Wrong.Visibility = Visibility.Hidden;
                KampanyaAdi_control.Visibility = Visibility.Visible;
                tb_check[1] = true;

            }
        }
        private void TaksitSayisi_Birakti(object sender, RoutedEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(Taksit_Sayisi_Textbox.Text, out n);


            if (!isNumeric || Taksit_Sayisi_Textbox.Text.Equals("") || n <= 1)
            {
                TaksitSayisi_Wrong.Visibility = Visibility.Visible;
                TaksitSayisi_control.Visibility = Visibility.Hidden;
                tb_check[2] = false;
            }
            else
            {
                TaksitSayisi_Wrong.Visibility = Visibility.Hidden;
                TaksitSayisi_control.Visibility = Visibility.Visible;
                tb_check[2] = true;
            }
        }
        private void Ucret_birakti(object sender, RoutedEventArgs e)
        {
            double n;
            bool isNumeric = double.TryParse(Ucret_TextBox.Text, out n);


            if (!isNumeric || Ucret_TextBox.Text.Equals("") || n <= 0)
            {
                Ucret_Wrong.Visibility = Visibility.Visible;
                Ucret_control.Visibility = Visibility.Hidden;
                tb_check[3] = false;
            }
            else
            {
                Ucret_Wrong.Visibility = Visibility.Hidden;
                Ucret_control.Visibility = Visibility.Visible;
                tb_check[3] = true;
            }
        }
        private void Faiz_birakti(object sender, RoutedEventArgs e)
        {
            double n;
            bool isNumeric = double.TryParse(Faiz_TextBox.Text, out n);


            if (!isNumeric || Faiz_TextBox.Text.Equals("") || n <= 0)
            {
                Faiz_Wrong.Visibility = Visibility.Visible;
                Faiz_kontrol.Visibility = Visibility.Hidden;
                tb_check[4] = false;
            }
            else
            {
                Faiz_Wrong.Visibility = Visibility.Hidden;
                Faiz_kontrol.Visibility = Visibility.Visible;
                tb_check[4] = true;
            }
        }
        private void Temizle_Click(object sender, RoutedEventArgs e)
        {
            
            KampanyaAdi_textbox.Text = "";
            TutarAraligi_textbox1.Text = "";
            TutarAraligi_textbox2.Text = "";
            Taksit_Sayisi_Textbox.Text = "";
            Tarih_Baslangic.SelectedDate = null;
            Tarih_Bitis.SelectedDate = null;
            Mcc_comboBox.SelectedItem = null;
            Faiz_TextBox.Text = "";
            Ucret_TextBox.Text = "";
            kapat();
            id = 0;
            MessageBox.Show("Bilgiler Temizlendi !");
        }
        private void Mcc_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MCC_Wrong.Visibility = Visibility.Hidden;
            MCC_control.Visibility = Visibility.Visible;
            tb_check[5] = true;
        }
        private void degisti(object sender, SelectionChangedEventArgs e)
        {
            tarihler++;
            if(tarihler>=2)
            {
                TarihAraligi_Wrong.Visibility = Visibility.Hidden;
                TarihAraligi_control.Visibility = Visibility.Visible;
                tb_check[6] = true;
                tarihler++;
                tarih_control();
            }
        }
        private void degisti2(object sender, SelectionChangedEventArgs e)
        {
            tarihler++;
            if (tarihler >= 2)
            {
                TarihAraligi_Wrong.Visibility = Visibility.Hidden;
                TarihAraligi_control.Visibility = Visibility.Visible;
                tb_check[6] = true;
                tarihler++;
                tarih_control();
            }
        }
        private void tarih_control()
        {

            
           if(Tarih_Baslangic.SelectedDate >= Tarih_Bitis.SelectedDate || Tarih_Baslangic.SelectedDate == null || Tarih_Bitis.SelectedDate ==null)
            {
                TarihAraligi_Wrong.Visibility = Visibility.Visible;
                TarihAraligi_control.Visibility = Visibility.Hidden;
                tb_check[6] = false;
                tarihler++;
            }
        }
        private void Kisi_Selected(object sender,MouseButtonEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid.SelectedItem;
                id = double.Parse((drv["ID"]).ToString());
                SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                connn.Open();
                SqlCommand bul = new SqlCommand("Select KampanyaAdı,TutarMin,TutarMax,TarihBaslangic,TarihBitis,TaksitSayisi,MCC,Ucret,Faiz from Kampanyalar where ID=" + id, connn);
                SqlDataReader Read = bul.ExecuteReader();
                while (Read.Read())
                {
                    
                    KampanyaAdi_textbox.Text = "" + Read.GetString(0);

                    KampanyaAdi_textbox.Text = Regex.Replace(KampanyaAdi_textbox.Text, @"\s+", "");
                    TutarAraligi_textbox1.Text = "" + Read.GetDouble(1).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

                    TutarAraligi_textbox2.Text = "" + Read.GetDouble(2).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                    Tarih_Baslangic.SelectedDate = Read.GetDateTime(3);
                    Tarih_Bitis.SelectedDate = Read.GetDateTime(4);

                    Taksit_Sayisi_Textbox.Text = "" + Read.GetInt32(5);
                    Mcc_comboBox.SelectedItem = Read.GetString(6);
                    Ucret_TextBox.Text =    "" + Read.GetDouble(7).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                    Faiz_TextBox.Text  =     "" + Read.GetDouble(8).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
  
                }
                kapat();
                MessageBox.Show("Bilgiler Getirildi !");
                MCC_control.Visibility = Visibility.Visible;
                tb_check[5] = true;
                connn.Close();
                
                getir_control = true;
                
            }
            catch
            {

            }
        }
        public void Kampanya_click(object sender, EventArgs e)
        {
            MainWindow yeni = new MainWindow();
            yeni.Show();

        }
        public void Islem_click(object sender, EventArgs e)
        {
            Window2 yeni = new Window2();
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

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
using System.ServiceProcess;

namespace CardtekProje
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int kontrol = 0;
        public int clickCount = 0;
        public bool[] tb_check = new bool[6];
        public double id = 0;
        public int control_goster = 1;
        public bool goster_control;
        public bool show =true;
        
        public MainWindow()
        {
            InitializeComponent();
            hepsini_kapat();
            Ad_TextBox.Background = Brushes.LightSteelBlue;          
            Soyad_textbox.Background = Brushes.LightSteelBlue;
            Telefon_Texbox.Background = Brushes.LightSteelBlue;
            KartNo_TextBox.Background = Brushes.LightSteelBlue;
            Email_TextBox.Background = Brushes.LightSteelBlue;
            Adres_TextBox.Background = Brushes.LightSteelBlue;
            Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        }
        public void hepsini_kapat()
        {
            Ad_Control.Visibility = Visibility.Hidden;
            Soyad_Control.Visibility = Visibility.Hidden;           
            Telefon_Control.Visibility = Visibility.Hidden;           
            KartNo_Control.Visibility = Visibility.Hidden;           
            Email_Control.Visibility = Visibility.Hidden;           
            Adres_Kontrol.Visibility = Visibility.Hidden;           
            SoyadImage_Check.Visibility = Visibility.Hidden;
            AdImage_Check.Visibility = Visibility.Hidden;
            KartNoImage_Check.Visibility = Visibility.Hidden;
            TelefonImage_Check.Visibility = Visibility.Hidden;
            EmailImage_Check.Visibility = Visibility.Hidden;
            AdresImage_Check.Visibility = Visibility.Hidden;
            int i = 0;
            for (i = 0; i < 6; i++)
            {
                tb_check[i] = false;
            }

        }

        public void goster()
        {
                string CmdString = string.Empty;
                SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                 connn.Open();
                CmdString = "SELECT ID, AD, SOYAD, TELEFON,KARTNO,EMAIL,ADRES FROM Musteri_Tab";
                SqlCommand cmd = new SqlCommand(CmdString, connn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Musteri_Tab");
                sda.Fill(dt);            
                dataGrid.ItemsSource = dt.DefaultView;
                connn.Close();  
        }
        private void Goster_Click(object sender, RoutedEventArgs e)
        {
            if(show)
            {
                string CmdString = string.Empty;
                SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                connn.Open();
                CmdString = "SELECT ID, AD, SOYAD, TELEFON,KARTNO,EMAIL,ADRES FROM Musteri_Tab";
                SqlCommand cmd = new SqlCommand(CmdString, connn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Musteri_Tab");
                sda.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                connn.Close();
                show = false;
            }else
            {
                dataGrid.ItemsSource = null;
                show = true;
            }
            

        }
        public void Sil_Click(object sender,EventArgs e)
        {
            if(id == 0)
            {
                MessageBox.Show("Tabloda bir satır seçin !");
            }else
            {
                SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                connn.Open();
                SqlCommand cmd = new SqlCommand("Delete from Musteri_Tab where ID=" + id, connn);
                cmd.ExecuteNonQuery();
                connn.Close();
                MessageBox.Show("Kayit Silindi !");
                Temizle.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                show = true;
                Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                id = 0;
            }
        }
        public void Ekle_Click(object sender,EventArgs e)
        {
            int i = 0;
            int z = 0;
            for(i=0; i< 6; i++)
            {
                if( tb_check[i] )
                {
                    z++;
                }
            }

            if( z <= 5)
            {
                MessageBox.Show("Eksik Veya Yanlış Doldurdugunuz Bilgiler Var(Hepsinin Yanında Tik işareti olduğundan Emin olun) !");
            }      
               else
                {
                    string CmdString = string.Empty;
                    SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                    connnn.Open();
                    CmdString = "Insert Into Musteri_Tab(AD,SOYAD,TELEFON,KARTNO,EMAIL,ADRES) Values(@AD,@SOYAD,@TELEFON,@KARTNO,@EMAIL,@ADRES)";
                    SqlCommand cmd = new SqlCommand(CmdString, connnn);
                    
                    cmd.Parameters.Add(new SqlParameter("AD", Ad_TextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("SOYAD", Soyad_textbox.Text));
                    cmd.Parameters.Add(new SqlParameter("TELEFON", Telefon_Texbox.Text));
                    cmd.Parameters.Add(new SqlParameter("KARTNO", KartNo_TextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("EMAIL", Email_TextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("ADRES", Adres_TextBox.Text));
                    cmd.ExecuteNonQuery();
                    connnn.Close();
                    MessageBox.Show("Kayit Eklendi !");
                    Temizle.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    show = true;
                    Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    id = 0;
                }
             
        }
        private void Temizle_Click(object sender,RoutedEventArgs e)
        {
            
            Ad_TextBox.Text = "";
            Soyad_textbox.Text = "";
            Telefon_Texbox.Text = "";
            Email_TextBox.Text = "";
            Adres_TextBox.Text = "";
            KartNo_TextBox.Text = "";
            hepsini_kapat();
            id = 0;
            MessageBox.Show("Bilgiler Temizlendi !");
        }
        public void Guncelle_click(object sender,EventArgs e)
        {
            if(id==0)
            {
                MessageBox.Show("Tabladon Bir satir Secin");
            } else
            {
                int i = 0;
                int z = 0;
                for (i = 0; i < 6; i++)
                {
                    if (tb_check[i])
                    {
                        z++;
                    }
                }

                if (z <= 5)
                {
                    MessageBox.Show("Eksik Veya Yanlış Doldurdugunuz Bilgiler Var(Hepsinin Yanında Tik işareti olduğundan Emin olun) !");
                }
                
                    
                    else
                    {
                        string CmdString = string.Empty;
                        SqlConnection connnn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                        connnn.Open();
                        CmdString = "Update Musteri_Tab Set AD=@AD,SOYAD=@SOYAD,TELEFON=@TELEFON,KARTNO=@KARTNO,EMAIL=@EMAIL,ADRES=@ADRES where ID="+id;
                        SqlCommand cmd = new SqlCommand(CmdString, connnn);
                        cmd.Parameters.AddWithValue("AD", Ad_TextBox.Text);
                        cmd.Parameters.AddWithValue("SOYAD", Soyad_textbox.Text);
                        cmd.Parameters.AddWithValue("TELEFON", Telefon_Texbox.Text);
                        cmd.Parameters.AddWithValue("KARTNO", KartNo_TextBox.Text);
                        cmd.Parameters.AddWithValue("EMAIL", Email_TextBox.Text);
                        cmd.Parameters.AddWithValue("ADRES", Adres_TextBox.Text);
                        cmd.ExecuteNonQuery();
                        connnn.Close();
                        MessageBox.Show("Kayit Guncellendi !");
                        Temizle.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        show = true;
                        Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        id = 0;
                    }
            }
        }
        public void Dokundu(object sender,RoutedEventArgs e)
        {            
        }
        public void Soyad_Birakti(object sender,RoutedEventArgs e)
        {
            
            
            bool containsInt = Soyad_textbox.Text.Any(char.IsDigit);
            char[] SpecialChars = "!@#$%^&*><[]()/{}-_;.Ã‡Ã§ÄŸÅŸÅÃ–Ã¶ÃœÃ¼Ä°Ä±â€â€™~ ¨\\".ToCharArray();
            int indexOf = Soyad_textbox.Text.IndexOfAny(SpecialChars);
            if (containsInt || Soyad_textbox.Text.Equals("") || indexOf !=-1)
            {
                Soyad_Control.Visibility = Visibility.Visible;
                SoyadImage_Check.Visibility = Visibility.Hidden;
                tb_check[0] = false;
            }
            else
            {
                Soyad_Control.Visibility = Visibility.Hidden;
                SoyadImage_Check.Visibility = Visibility.Visible;
                tb_check[0] = true;

            }

        }
        public void Ad_Birakti(object sender, RoutedEventArgs e)
        {
            

            bool containsInt = Ad_TextBox.Text.Any(char.IsDigit);
            char[] SpecialChars = "!@#$%^&*><[]()/{}-_;.Ã‡Ã§ÄŸÅŸÅÃ–Ã¶ÃœÃ¼Ä°Ä±â€â€™~ ¨\\".ToCharArray();
            int indexOf = Ad_TextBox.Text.IndexOfAny(SpecialChars);

            if (containsInt || Ad_TextBox.Text.Equals("") || indexOf != -1)
            {
                Ad_Control.Visibility = Visibility.Visible;
                AdImage_Check.Visibility = Visibility.Hidden;
                tb_check[1] = false;
            }
            else
            {
                Ad_Control.Visibility = Visibility.Hidden;
                AdImage_Check.Visibility = Visibility.Visible;
                tb_check[1] = true;
            }
        }
        public void Telefon_Birakti(object sender, RoutedEventArgs e)
        {
            double n;
            bool isNumeric = double.TryParse(Telefon_Texbox.Text, out n);
            int uzunluk = Telefon_Texbox.Text.Length;

            if (!isNumeric || Telefon_Texbox.Text.Equals("") || uzunluk != 10)
            {
                Telefon_Control.Visibility = Visibility.Visible;
                TelefonImage_Check.Visibility = Visibility.Hidden;
                tb_check[2] = false;
            }
            else
            {
                Telefon_Control.Visibility = Visibility.Hidden;
                TelefonImage_Check.Visibility = Visibility.Visible;
                tb_check[2] = true;
            }
        }
        public void KartNo_Birakti(object sender, RoutedEventArgs e)
        {
            double n;
            bool isNumeric = double.TryParse(KartNo_TextBox.Text, out n);
            int uzunluk = KartNo_TextBox.Text.Length;
            if (!isNumeric || KartNo_TextBox.Text.Equals("") || uzunluk!=16 )
            {
                KartNo_Control.Visibility = Visibility.Visible;
                KartNoImage_Check.Visibility = Visibility.Hidden;
                tb_check[3] = false;
            }
            else
            {
                KartNo_Control.Visibility = Visibility.Hidden;
                KartNoImage_Check.Visibility = Visibility.Visible;
                tb_check[3] = true;
            }

        }
        public void Email_Birakti(object sender, RoutedEventArgs e)
        {

            char[] SpecialChars = "!#$%^&*><[]()/{}-;Ã‡Ã§ÄŸÅŸÅÃ–Ã¶ÃœÃ¼Ä°Ä±â€â€™~ ¨\\".ToCharArray();
            int indexOf = Email_TextBox.Text. IndexOfAny(SpecialChars);
            int etIndex = Email_TextBox.Text.IndexOf('@');
            string dummy = Email_TextBox.Text;
            dummy = dummy.ToLower();
            int com = dummy.IndexOf(".com");

            if (Ad_TextBox.Text.Equals("") || indexOf != -1 || etIndex == -1 || com == -1)
            {
                Email_Control.Visibility = Visibility.Visible;
                EmailImage_Check.Visibility = Visibility.Hidden;
                tb_check[4] = false;
            }
            else
            {
                Email_Control.Visibility = Visibility.Hidden;
                EmailImage_Check.Visibility = Visibility.Visible;
                tb_check[4] = true;
            }

        }
        public void Adres_Birakti(object sender, RoutedEventArgs e)
        {  
            char[] SpecialChars = "!@#$%^&*><[](){}_;Ã‡Ã§ÄŸÅŸÅÃ–Ã¶ÃœÃ¼Ä°Ä±â€â€™~¨\\".ToCharArray();
            int indexOf = Adres_TextBox.Text.IndexOfAny(SpecialChars);

            if (Adres_TextBox.Text.Equals("") || indexOf != -1 ||String.IsNullOrWhiteSpace(Adres_TextBox.Text))
            {
                Adres_Kontrol.Visibility = Visibility.Visible;
                AdresImage_Check.Visibility = Visibility.Hidden;
                tb_check[5] = false;
            }
            else
            {
                Adres_Kontrol.Visibility = Visibility.Hidden;
                AdresImage_Check.Visibility = Visibility.Visible;
                tb_check[5] = true;
            }


        }
        private void dg_click(object sender, SelectionChangedEventArgs e)
        {
           
            
        }
        private void Tiklandi(object sender,MouseButtonEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid.SelectedItem;
                id = double.Parse((drv["ID"]).ToString());
                goster_control = true;
                SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                connn.Open();
                SqlCommand bul = new SqlCommand("Select AD,SOYAD,TELEFON,KARTNO,EMAIL,ADRES from Musteri_Tab where ID=" + id, connn);
                SqlDataReader Read = bul.ExecuteReader();
                while (Read.Read())
                {  
                    Ad_TextBox.Text = "" + Read.GetString(0);

                    Ad_TextBox.Text = Regex.Replace(Ad_TextBox.Text, @"\s+", "");
                    Soyad_textbox.Text = "" + Read.GetString(1);

                    Soyad_textbox.Text = Regex.Replace(Soyad_textbox.Text, @"\s+", "");

                    Telefon_Texbox.Text = "" + Read.GetDouble(2);
                    KartNo_TextBox.Text = "" + Read.GetInt64(3);
                    Email_TextBox.Text = "" + Read.GetString(4);

                    Email_TextBox.Text = Regex.Replace(Email_TextBox.Text, @"\s+", "");
                    Adres_TextBox.Text = "" + Read.GetString(5);
                    break;
                }
                MessageBox.Show("Bilgiler Getirildi !");
                connn.Close();
            }
            catch
            {

            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //e.Cancel = false;
            /*if (MessageBox.Show("Cıkmak istediğinden emin misin?", "Hey", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {

                Application.Current.Shutdown();
            }*/
        }
        public void Kampanya_click(object sender ,EventArgs e )
        {
            Window1 yeni = new Window1();
            yeni.Show();

        }
        public void Islem_click(object sender,EventArgs e)
        {
            Window2 yeni = new Window2();
            yeni.Show();
        }
        public void sms_click(object sender,EventArgs e)
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
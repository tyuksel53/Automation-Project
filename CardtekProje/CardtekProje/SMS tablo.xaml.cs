using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace CardtekProje
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public bool show = true;
        public double[] id = new double[50];
        public int sayi = 0;
        public Window3()
        {
            InitializeComponent();
            
            SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            connn.Open();
            
            int i = 0;
            for(i=0; i<50; i++)
            {
                id[i] = 0;
            }
            Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        }
        private void Goster_Click(object sender, RoutedEventArgs e)
        {
            if (show)
            {
                string CmdString = string.Empty;
                SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                connn.Open();
                CmdString = "SELECT id,MusteriID,IslemID,KampanyaID,TelefonFlag,EmailFlag,Response FROM Cevaplar where Response=0";
                SqlCommand cmd = new SqlCommand(CmdString, connn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Cevaplar");
                sda.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                connn.Close();
                show = false;
            }
            else
            {
                dataGrid.ItemsSource = null;
                show = true;
            }
        }
        private void Taksitle_click(object sender,RoutedEventArgs e)
        {
            if(id[0] == 0)
            {
                MessageBox.Show("Satir secin !");
            }else
            {
                int i = 0;
                for(i=0; id[i]!=0;i++)
                {

                    Taksitle_basla(id[i].ToString());
                }

                for (i=0; i<50; i++)
                {
                    id[i] = 0;
                }
                sayi = 0;
                MessageBox.Show("Seçimler sifirlandi");
                show = true;
                Goster.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                textBlock.Text = "Secilen IDler: ";
            }
            
            

        }
        public void Taksitle_basla(string cevaplarID)
        {
            SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            connn.Open();
            int IslemID = 0;
            int KampanyaID = 0;
            SqlCommand yeni = new SqlCommand("Select IslemID,KampanyaID from Cevaplar where id="+cevaplarID,connn);
            SqlDataReader Read = yeni.ExecuteReader();
            while(Read.Read())
            {
                IslemID = Read.GetInt32(0);
                KampanyaID = Read.GetInt32(1);
                break;
            }
            connn.Close();
            Taksitle_bitir(IslemID, KampanyaID,cevaplarID);
            

        }
        public void Taksitle_bitir(int islemID,int kampanyaId,string cevaplarID)
        {
            SqlConnection data = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            data.Open();
            double tutar = 0;
            int taksit = -1;
            int i_id = 0;
            double faiz = 0;
            double ucret = 0;
            string tarih = "";
            SqlCommand yeni = new SqlCommand("Select i.Tutar,k.TaksitSayisi,k.Faiz,k.Ucret,i.Tarih,i.ID  from Islem_tablo i,Kampanyalar k where K.ID="+kampanyaId + "and i.ID=" +islemID,data);
            SqlDataReader Read = yeni.ExecuteReader();
            DateTime myDateTime = DateTime.Now;
            while (Read.Read())
            {
                tutar = Read.GetDouble(0);
                taksit = Read.GetInt32(1);
                faiz = Read.GetDouble(2);
                ucret = Read.GetDouble(3);
                myDateTime =Read.GetDateTime(4);
                i_id = Read.GetInt32(5);
                break;
            }
            tarih = myDateTime.ToString("yyyy-MM-dd");         
            faiz = faiz / 100;
            data.Close();
            string komut = "AddTo_Taksit '" + tutar.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "','" + taksit + "','" + faiz.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "','" + ucret.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "','" + tarih + "','"+i_id+"'";
            taksitle_artık(komut,cevaplarID);
            
        }
        public void taksitle_artık(string cmd,string cevaplarID)
        {
            SqlConnection data = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            data.Open();
            SqlCommand yeni = new SqlCommand(cmd, data);
            yeni.ExecuteNonQuery();
            tabloyu_guncelle(cevaplarID);
            data.Close();
        }
        public void tabloyu_guncelle(string cevaplarID)
        {
            SqlConnection data = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            data.Open();
            SqlCommand yeni = new SqlCommand("Update Cevaplar SET Response=@Response where id=" + cevaplarID, data);
            yeni.Parameters.AddWithValue("Response", 1);
            yeni.ExecuteNonQuery();
            data.Close();


        }
        private void Temizle_Click(object sender,RoutedEventArgs e)
        {
            int i;
            for (i = 0; i < 50; i++)
            {
                id[i] = 0;
            }
            sayi = 0;
            MessageBox.Show("Seçimler sifirlandi");
            textBlock.Text = "Secilen IDler: ";
        }
        private void GeriAl_click(object sender, RoutedEventArgs e)
        {
            if(id[0] !=0)
            {
                int i;
                for (i = 0; id[i]!=0; i++)
                {
                
                }
                string dummy = id[i - 1].ToString();
                id[i - 1] = 0;
                textBlock.Text = textBlock.Text.Replace(dummy + ",", "");
                sayi--;
            }
            

        }
        private void degisti(object sender,SelectionChangedEventArgs e)
        {
            double zundi = 0;
            int Control = 0;
            try
            {
                DataRowView drv = (DataRowView)dataGrid.SelectedItem;
                zundi = double.Parse((drv["id"]).ToString());
                for(int i=0; i<50;i++)
                {
                    if(zundi == id[i])
                    {
                        Control = 1;
                        break;
                    }
                }
                if(Control == 0)
                {
                    textBlock.Text = textBlock.Text + zundi + ", ";
                    id[sayi] = zundi;
                    sayi++;
                }
                
            }
            catch
            {

            }
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
        public void istatislik_click(object sender, EventArgs e)
        {
            Window5 yeni = new Window5();
            yeni.Show();
        }
        
    }
}

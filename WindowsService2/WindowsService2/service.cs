using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using xmlpost;

namespace WindowsService2
{
    public partial class service : ServiceBase

    {
        SqlConnection connn = new SqlConnection("Data Source=pcname;Initial Catalog=SampleDatabase;Integrated Security=True");
        Timer trd = new Timer();
        
        public service()
        {
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
                trd.Elapsed += new ElapsedEventHandler(trd_elapsed);
                trd.Interval = 1000 * 10;
                trd.Start();
                
        }
        protected static void yaz(string gelen)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Onstart.txt", true))
            {
                sw.WriteLine(System.DateTime.Now.ToString() + " " +gelen);
            }
        }
        protected void trd_elapsed(object sender,ElapsedEventArgs e)
        {
            yaz("Trd_elapsed foksiyonu calisti");
            int musteriID,IslemID,KampanyaID;
            connn.Open();
            SqlCommand bul = new SqlCommand("select m.ID,k.ID,i.ID from Kampanyalar k ,Islem_tablo i,Musteri_Tab m where i.MCC = K.MCC and i.Tutar between k.TutarMin and k.TutarMax and i.Tarih between k.TarihBaslangic and k.TarihBitis and i.MusteriID = m.ID; ", connn);
            SqlDataReader Read = bul.ExecuteReader();

            while(Read.Read())
            {
                musteriID = Read.GetInt32(0);
                KampanyaID = Read.GetInt32(1);
                IslemID = Read.GetInt32(2);
                yaz(("" + musteriID + " " + KampanyaID + " " + IslemID).ToString());
                ekleCevaplar(musteriID, IslemID, KampanyaID);        
            }
            yaz("Yazma Bitti");
            connn.Close(); 
        }
        protected  void ekleCevaplar(int musteriID,int islemID,int kampanyaID)
        {
            yaz("EkleCevaplar Foksiyonu Calisti");
            SqlConnection baglan = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            baglan.Open();
            int cevap = 0;
            SqlCommand yeni = new SqlCommand("Select count(*) from Cevaplar where MusteriID=" + musteriID + " and IslemID=" + islemID + " and KampanyaID=" + kampanyaID, baglan);
            SqlDataReader Read = yeni.ExecuteReader();
            while (Read.Read())
            {
                cevap = Read.GetInt32(0);
                yaz("Sonuc bulundu 1");
                break;
            }
            baglan.Close();
            if(cevap == 0)
            {
                yaz("Yazma Basliyor...");
                SqlConnection yok_et = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                yok_et.Open();
                SqlCommand cmd = new SqlCommand("Insert Into Cevaplar(MusteriID,IslemID,KampanyaID,TelefonFlag,EmailFlag,Response) Values(@MusteriID,@IslemID,@KampanyaID,@TelefonFlag,@Emailflag,@Response)", yok_et);  
                cmd.Parameters.AddWithValue("MusteriID", musteriID);
                cmd.Parameters.AddWithValue("IslemID", islemID);
                cmd.Parameters.AddWithValue("KampanyaID", kampanyaID);
                cmd.Parameters.AddWithValue("TelefonFlag", 1); 
                cmd.Parameters.AddWithValue("EmailFlag", 1); 
                cmd.Parameters.AddWithValue("Response", 0); 
                cmd.ExecuteNonQuery(); 
                yaz("Islem Eklendi -?");
                yok_et.Close();
                email_gonder(musteriID,kampanyaID,islemID);
                
            }
            
        }
        protected void email_gonder(int musteriID,int kampanyaID,int islemID)
        {
            yaz("email_gonder foksiyonu calisti !");
            SqlConnection email_baglan = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            email_baglan.Open();
            string isim_soyisim="",dummy="";
            string mail= "";
            string kampanya_bilgi="";
            string islem_bilgi="";
            string telefonNO = "";
            string tarih = "";
            SqlCommand gonder = new SqlCommand("Select m.AD,m.SOYAD,m.EMAIL,i.Tarih,i.Tutar,k.Ucret,k.Faiz,k.TaksitSayisi,m.TELEFON from Kampanyalar k,Islem_tablo i,Musteri_Tab m where m.ID =" + musteriID + " and k.ID ="+ kampanyaID +" and i.ID="+islemID,email_baglan);
            SqlDataReader Read = gonder.ExecuteReader();
            while(Read.Read())
            {
                dummy = Regex.Replace(Read.GetString(0), @"\s+", "");
                isim_soyisim = "Sayin " + dummy;
                dummy = Regex.Replace(Read.GetString(1), @"\s+", "");
                isim_soyisim = isim_soyisim + " " + dummy;
                mail = Read.GetString(2);
                tarih = Read.GetDateTime(3).ToString();
                tarih = tarih.Replace("00:00:00", " ");
                islem_bilgi = " " + tarih + " Tarihindeki " + Read.GetDouble(4) + " Tutarinda yapmış olduğunuz işeminizi ";
                kampanya_bilgi = Read.GetDouble(5) + " Ucret, %" + Read.GetDouble(6) + " Faiz orani ile " + Read.GetInt32(7) + " taksit yapmamızı ister misiniz ?";
                telefonNO = Read.GetDouble(8).ToString();
                break;
            }
            yaz("email_gonder foksiyonunda bilgiler alindi");
            
            email_baglan.Close();
            yaz("Mail gönderme islemi basliyor");

            MailMessage email = new MailMessage();
            email.From = new MailAddress("example@hotmail.com");
            email.To.Add(mail);
            email.Subject = "Taksitlendirme Hakkında";
            email.Body = isim_soyisim + islem_bilgi + kampanya_bilgi;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("example@hotmail.com", "pass");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = "smtp.live.com";
            smtp.Send(email);

            yaz("email gönderildi");
            string mesaj = isim_soyisim + islem_bilgi + kampanya_bilgi;
            yaz("sms gönderiliyor");
            Executer yeni = new Executer();
            yeni.Main(mesaj,telefonNO);
            yaz("sms gönderildi");

        }

        protected override void OnStop()
        {
        }
    }
}

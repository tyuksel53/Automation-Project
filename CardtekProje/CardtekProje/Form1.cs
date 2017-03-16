using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardtekProje
{
    public partial class Form1 : Form
    {
        public bool control = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
            connn.Open();
            SqlCommand cmd = new SqlCommand("Select EMAIL,UserName,Password from Users", connn);
            SqlDataReader Read = cmd.ExecuteReader();
            string mail = "",username="",password="";
            while(Read.Read())
            {
                mail = Read.GetString(0);
                username = Read.GetString(1);
                password = Read.GetString(2);
                check(mail, username, password);
            }

            MessageBox.Show("Mail Gönderildi !");
            Application.Exit();

        }
        private void check(string gelenMail,string gelenUsername,string gelenPassWord)
        {
            if(gelenMail.Equals(textBox1.Text))
            {
                MailMessage email = new MailMessage();
                email.From = new MailAddress("example@hotmail.com");
                email.To.Add(textBox1.Text);
                email.Subject = "Kullanici Bilgileriniz hakkinda";
                email.Body = "Kullanici bilgileriniz:\n\nUsername:" + gelenUsername + "\n\nPassword:" + gelenPassWord;
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential("example@hotmail.com", "pass");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.live.com";
                smtp.Send(email);
                control = true;
            }else
            {
                control = false;
            }
        }
    }
}

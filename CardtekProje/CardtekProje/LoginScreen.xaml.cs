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
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace CardtekProje
{
    /// <summary>
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public bool[] login_control = new bool[2];
        public bool kontrol = false;
        public Window4()
        {
            InitializeComponent();
            kapat();
        }
        private void kapat()
        {
            Username_control.Visibility = Visibility.Hidden;
            username_wrong.Visibility = Visibility.Hidden;
            password_control.Visibility = Visibility.Hidden;
            password_wrong.Visibility = Visibility.Hidden;

        }
        private void basti(object sender,RoutedEventArgs e)
        {
        }
        private void username_birakti(object sender,RoutedEventArgs e)
        {
            
            char[] SpecialChars = "!@#$%^&*><[]()/?{}-_;.Ã‡Ã§ÄŸÅŸÅÃ–Ã¶ÃœÃ¼Ä°Ä±â€â€™~ ¨\\".ToCharArray();
            int indexOf = Username_textbox.Text.IndexOfAny(SpecialChars);
            if (Username_textbox.Text.Equals("") || indexOf != -1)
            {
                username_wrong.Visibility = Visibility.Visible;
                Username_control.Visibility = Visibility.Hidden;
                login_control[0] = false;
            }
            else
            {
                username_wrong.Visibility = Visibility.Hidden;
                Username_control.Visibility = Visibility.Visible;
                login_control[0] = true;

            }
        }
        private void Password_Birakti(object sender,RoutedEventArgs e)
        {
            char[] SpecialChars = "!@#$%^&*><?[]()/{}-_;.Ã‡Ã§ÄŸÅŸÅÃ–Ã¶ÃœÃ¼Ä°Ä±â€â€™~ ¨\\".ToCharArray();
            int indexOf = Username_textbox.Text.IndexOfAny(SpecialChars);
            if (passwordBox.Password.Equals("") || indexOf != -1)
            {
                password_wrong.Visibility = Visibility.Visible;
                password_control.Visibility = Visibility.Hidden;
                login_control[1] = false;
            }
            else
            {
                password_wrong.Visibility = Visibility.Hidden;
                password_control.Visibility = Visibility.Visible;
                login_control[1] = true;

            }
        }
        private void Forgot_basti(object sender,MouseButtonEventArgs e)
        {
            Form1 yeni = new Form1();
            yeni.Show();  
            

        }
        private void Kapaniyor(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Login_button_Click(object sender, RoutedEventArgs e)
        {
            if(login_control[0] && login_control[1])
            {
                SqlConnection connn = new SqlConnection("Data Source=pc-name;Initial Catalog=SampleDatabase;Integrated Security=True");
                connn.Open();
                SqlCommand cmd = new SqlCommand("Select UserName,Password from Users", connn);
                SqlDataReader Read = cmd.ExecuteReader();
                string username = "", password = "";
                
                        
                while(Read.Read())
                {
                    username = Read.GetString(0);
                    password = Read.GetString(1);
                    check(username, password);
                    if(kontrol == true)
                    {
                        MessageBox.Show("Giris Basarili !");
                        MainWindow yeni = new MainWindow();
                        yeni.Show();
                        this.Close();

                        break;


                    }
                }
                if(kontrol == false)
                {
                    MessageBox.Show("Girdiginiz bilgiler eslesmiyor !");
                    kapat();
                    login_control[0] = false;
                    login_control[1] = false;
                    Username_textbox.Text = "";
                    passwordBox.Password = "";
                }

            }else
            {
                MessageBox.Show("Yanlış veya Eksik Girmiş olduğunuz bilgiler mevcut !");
            }
            
        }
        private void check(string username,string password)
        {
            if(Username_textbox.Text.Equals(username)&& passwordBox.Password.Equals(password))
            {
                kontrol = true;
               
            }else
            {
                kontrol = false;
            }
        }
        private void Tus_basti(object sender,KeyEventArgs e)
        {
            if(e.Key.Equals(Key.Return) )
            {
                e.Handled = true;
                Login_button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
        private void password_basti(object sender,RoutedEventArgs e)
        {
            login_control[1] = true;
        }
    }
}

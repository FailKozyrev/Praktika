using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow aut = new MainWindow();
            aut.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginErrorBox.Text = string.Empty;
            EmailErrorBox.Text = string.Empty;
            PasswordErrorBox.Text = string.Empty;
            lg.BorderBrush = new SolidColorBrush(Colors.Black);
            em.BorderBrush = new SolidColorBrush(Colors.Black);
            ps1.BorderBrush = new SolidColorBrush(Colors.Black);
            ps2.BorderBrush = new SolidColorBrush(Colors.Black);
            var login = lg.Text;
            var pass = ps1.Text;
            var email = em.Text;
            var pass2 = ps2.Text;
            var Context = new AppDbContext();

            var user_exists = Context.Users.FirstOrDefault(x => x.Login == login);
            if (user_exists is not null)
            {
                MessageBox.Show("Такой пользователь уже купил компьютер");
                return;
            }
            else if (LoginCheck() == false)
            {
                lg.BorderBrush = new SolidColorBrush(Colors.Red);
                LoginErrorBox.Text = "Введите логин";
            }
            else if (EmailCheck() == false)
            {
                em.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            else if (PasswordCheck() == false)
            {
                ps1.BorderBrush = new SolidColorBrush(Colors.Red);
                ps2.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            else
            {
                var User = new User { Login = login, Password = pass, Email = email };
                Context.Users.Add(User);
                Context.SaveChanges();
                MessageBox.Show("Добро пожаловать в клуб обладателей компьютеров!");
                MainWindow mainWindow = new MainWindow();
                this.Hide();
                mainWindow.Show();
            }

        }
        private bool LoginCheck()
        {
            if (lg.Text.Length == 0)
            {
                return false;
            }
            return true;
        }
        private bool EmailCheck()
        {
            if (em.Text.Length == 0)
            {
                EmailErrorBox.Text = "Введите электронную почту";
                return false;
            }

            if (em.Text.IndexOf("@") > -1)
            {
                if (em.Text.IndexOf(".", em.Text.IndexOf("@")) > em.Text.IndexOf("@"))
                {
                    return true;
                }
            }
            EmailErrorBox.Text = "Формат введенной почты некорректен";
            return false;
        }
        private bool PasswordCheck()
        {
            bool hasUpperCase = false;
            bool hasDigit = false;
            bool hasSpecialCharacter = false;
            if (ps1.Text.Length < 8)
            {
                PasswordErrorBox.Text = "Пароль менее 8 символов";
                return false;
            }
            foreach (char c in ps1.Text)
            {
                if (char.IsUpper(c))
                {
                    hasUpperCase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else if (!char.IsLetterOrDigit(c))
                {
                    hasSpecialCharacter = true;
                }
            }
            if (hasUpperCase == false)
            {
                PasswordErrorBox.Text = "Пароль не содержит букв верхнего регистра";
                return false;
            }
            if (hasDigit == false)
            {
                PasswordErrorBox.Text = "Пароль не содержит цифр";
                return false;
            }
            if (hasSpecialCharacter == false)
            {
                PasswordErrorBox.Text = "Пароль не содержит спецсимволов";
                return false;
            }
            if (ps1.Text != ps2.Text)
            {
                PasswordErrorBox.Text = "Введенные пароли не совпадают";
                return false;
            }
            return true;
        }

        private void lg_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void ps1_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        
    }
}

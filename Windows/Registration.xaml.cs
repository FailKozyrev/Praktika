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
            var login = lg.Text;
            var pass = ps1.Text;
            var email = em.Text;
            if (lg.Text.Length > 0) // проверяем логин
{
                if (em.Text.Length > 0 && EmailCheck() == true)// почта
                {
                    if (ps1.Text.Length > 0) // проверяем пароль
                    {
                        if (ps2.Text.Length > 0 && ps2Check() == true) // проверяем второй пароль
                        {
                            var context = new AppDbContext();
                            var user_exists = context.Users.FirstOrDefault(x => x.Login == login);
                            if (user_exists != null)
                            {
                                MessageBox.Show("Такой пользователь уже купил компьютер!");
                                return;
                            }
                            var user = new User { Login = login, Password = pass, Email = email };
                            context.Users.Add(user);
                            context.SaveChanges();
                            MessageBox.Show("Добро пожаловать в клуб обладателей компуктеров!");
                        }
                        else MessageBox.Show("Повторите пароль");
                    }
                    else MessageBox.Show("Укажите пароль");
                }
                else MessageBox.Show("Укажите потчу");
            }
            else MessageBox.Show("Укажите логин");
            
        }

        private void lg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private bool ps2Check()
        {
            ps1.Text = ps2.Text;
            return true;
        }

        private bool EmailCheck()
        {
            if (em.Text.Length == 0)
            {
                return false;
            }

            if (em.Text.IndexOf("@") > -1)
            {
                if (em.Text.IndexOf(".", em.Text.IndexOf("@")) > em.Text.IndexOf("@"))
                {
                    return true;
                }
            }
            MessageBox.Show("Почта введена неверно!");

            return false;
        }
    }
}

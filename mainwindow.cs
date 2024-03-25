using Microsoft.EntityFrameworkCore;
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
using WpfApp1.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var login=LoginBox.Text;
            var password = Pass.Text;

            var context = new AppDbContext();

            var user = context.Users.SingleOrDefault(x => x.Login == login && x.Password == password);
            if (user is null) 
            {
                ErrorBox.Text="Неправильный логин или пароль";
                return;
            }
            else MessageBox.Show("Вы успешно вошли в аккаунт!");
            Hi ju = new Hi();
            ju.Show();
            this.Hide();
            ju.bro.Text = LoginBox.Text;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           Registration reg = new Registration();
            reg.Show();
            this.Hide();
        }
        
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PasswordBox.Visibility = Visibility.Collapsed;
            Pass.Text = PasswordBox.Password;
            Pass.Visibility = Visibility.Visible;
            PasswordHiddenBtn.Visibility = Visibility.Collapsed;
            PasswordOpenBtn.Visibility = Visibility.Visible;
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PasswordBox.Visibility = Visibility.Visible;
            PasswordBox.Password = Pass.Text;
            Pass.Visibility = Visibility.Collapsed;
            PasswordHiddenBtn.Visibility = Visibility.Visible;
            PasswordOpenBtn.Visibility = Visibility.Collapsed;
        }

    }
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }


}

using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        static int _enterAttempts = 0;
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            Models.Worker user = App.db.Workers
                .Where(x => x.Login == tb_login.Text)
                .Include(x => x.Surname)
                .Include(x => x.Firstname)
                .FirstOrDefault();
            if (user == null)
            {
                OnBadAttempt();
                return;
            }
            if (user.Password != pb_password.Password)
            {
                App.db.EnterHistories.Add(new Models.EnterHistory
                {
                    IsSuccessful = false,
                    WorkerId = user.WorkerId,
                    Datetime = DateTime.Now
                });
                App.db.SaveChangesAsync();
                OnBadAttempt();
                return;
            }
            _enterAttempts = 0;
            using (var db = new Models.UP_02_01_DBEntities())
            {
                db.EnterHistories.Add(new Models.EnterHistory()
                {
                    IsSuccessful = true,
                    WorkerId = user.WorkerId,
                    Datetime = DateTime.Now
                });
                db.SaveChanges();
            }

            App.User = user;

            var role = user.Role;
            switch (role.Name)
            {   
                case "Администратор":
                    new Admin(this).Show();
                    Hide(); 
                    break;
                case "Начальник отдела":
                    new HeadOfDepartment(this).Show();
                    Hide();
                    break;
                case "Работник склада":
                    new StorageWorker(this).Show();
                    Hide();
                    break;
                case "Продавец-кассир": 
                    new Cassier(this).Show();
                    Hide();
                    break;
            }
        }
        private void OnBadAttempt()
        {
            var msg = "Логин или пароль неверен.";
            switch (++_enterAttempts)
            {
                default:
                case 5:
                    btn_login.IsEnabled = false;
                    MessageBox.Show(msg+" Доступ заблокирован. Перезапустите приложение.");
                    break;
                case 4:
                    btn_login.IsEnabled = false;
                    System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    dispatcherTimer.Tick += (s,a) => btn_login.IsEnabled = true;
                    dispatcherTimer.Interval = TimeSpan.FromMinutes(3);
                    dispatcherTimer.Start();
                    msg += " Доступ заблокирован на 3 минуты.";
                    goto case 2;
                case 3:
                case 2:
                    new Captcha(this).Show();
                    Hide();
                    goto case 1;
                case 1:
                    MessageBox.Show(msg); 
                    break;
            }
                
        }
        private void btn_show_password_Click(object sender, RoutedEventArgs e)
        {
            if (tb_password.Visibility == Visibility.Collapsed)
            {
                tb_password.Visibility = Visibility.Visible;
                tb_password.Text = pb_password.Password;
                return;
            }
            tb_password.Visibility = Visibility.Collapsed;
            pb_password.Password= tb_password.Text;
        }
    }
}

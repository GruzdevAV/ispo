using System.Net;
using System.Timers;
using System.Windows;
using DrivingSchoolAPIModels;
namespace DrivingSchoolGUIApp
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        Timer _timer = new Timer(5000);
        public LoginWindow()
        {
            InitializeComponent();
            _timer.Elapsed += _timer_Elapsed;
            _timer.AutoReset = true;
        }

        private async void _timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            await txtLoginMessage.Dispatcher.BeginInvoke( async () =>
            {
                txtLoginMessage.Visibility = !await APIClass.Ping() ? Visibility.Visible : Visibility.Hidden;
            });
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            var status = await APIClass.Login(
                new LoginModel
                {
                    Email = txtLoginEmail.Text,
                    Password = txtLoginPassword.Password
                }
                );
            if (status.Status != $"{HttpStatusCode.OK}")
            {
                MessageBox.Show($"Не удалось подключиться:\n{status.Message}");
                return;
            }
            new MainWindow().Show();
            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _timer_Elapsed(null, null);
            _timer.Start();
        }
    }
}

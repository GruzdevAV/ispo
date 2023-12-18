using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для HeadOfDepartment.xaml
    /// </summary>
    public partial class HeadOfDepartment : Window
    {
        private Window _prevWindow;
        public HeadOfDepartment(Window prevWindow)
        {
            InitializeComponent();
            _prevWindow = prevWindow;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            if (App.User.ImgId != null)
                img_person.Source = new BitmapImage(new Uri(App.User.Image?.PathToImage, UriKind.Relative));
            tb_name.Text = $"Здравствуйте, {App.User.Firstname.Firstname1} {App.User.Surname.Surname1}";
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            _prevWindow.Show();
            Close();
        }

        private void btn_crud_departments_Click(object sender, RoutedEventArgs e)
        {
            new DepartmentsList(this).Show();
            Hide();
        }

        private void btn_show_papers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Отчёт: эта кнопка работает прекрасно.");
        }

        private void btn_add_worker_Click(object sender, RoutedEventArgs e)
        {
            new AddWorker(this).Show();
            Hide();
        }

        private void btn_show_workers_list_Click(object sender, RoutedEventArgs e)
        {
            var worker = new WorkersList(this);
            worker.Head = true;
            worker.Show();
            Hide();
        }
    }
}

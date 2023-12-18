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

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private Window _prevWindow;

        public Admin(Window prevWindow)
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

        private void btn_show_enter_history_Click(object sender, RoutedEventArgs e)
        {
            new EnterHistory(this).Show();
            Hide();
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

        private void btn_crud_products_Click(object sender, RoutedEventArgs e)
        {
            new ProductsList(this).Show();
            Hide();
        }

        private void btn_show_papers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Отчёт: эта кнопка работает прекрасно.");
        }

        private void btn_add_worker_Click(object sender, RoutedEventArgs e)
        {
            new AddWorker(this).Show();
            Hide() ;
        }

        private void btn_show_workers_list_Click(object sender, RoutedEventArgs e)
        {
            new WorkersList(this).Show();
            Hide();
        }
    }
}

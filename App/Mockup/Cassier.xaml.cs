using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для Cassier.xaml
    /// </summary>
    public partial class Cassier : Window
    {
        private Window _prevWindow;
        public Cassier(Window prevWindow)
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

        private void btn_show_my_sales_Click(object sender, RoutedEventArgs e)
        {
            new SalesList(this).Show();
            Hide();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            _prevWindow.Show();
            Close();
        }

        private void btn_make_new_sale_Click(object sender, RoutedEventArgs e)
        {
            new CommiteSell(this).Show();
            Hide();
        }

        private void btn_show_storage_product_list_Click(object sender, RoutedEventArgs e)
        {
            new ProductsOfDepartmentList(this).Show();
            Hide();
        }

        private void btn_show_all_product_list_Click(object sender, RoutedEventArgs e)
        {
            new ProductsList(this).Show();
            Hide();
        }
    }
}

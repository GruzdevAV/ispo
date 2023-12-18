using System;
using System.Linq;
using System.Windows;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для CrudProduct.xaml
    /// </summary>
    public partial class CrudProduct : Window
    {
        private Window _prevWindow;
        Models.MeasurementUnit Unit {  get=> (Models.MeasurementUnit)cb_unit.SelectedItem; }
        public CrudProduct(Window prevWindow)
        {
            InitializeComponent();
            _prevWindow = prevWindow;
            cb_unit.ItemsSource = App.db.MeasurementUnits.ToList();
        }

        private void btn_go_back_Click(object sender, RoutedEventArgs e)
        {
            _prevWindow.Show();
            Close();
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.db.Products.Add(new Models.Product
                {
                    ProductArticleNumber = tb_article.Text,
                    MeasurementUnitId = Unit.Id,
                    ProductName = tb_name.Text,
                    Price = decimal.Parse(tb_price.Text)
                });
                App.db.SaveChanges();
                btn_go_back_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для ShipIn.xaml
    /// </summary>
    public partial class ShipIn : Window
    {
        private Window _prevWindow;
        Models.Product Product { get => ((Models.Product)cb_article.SelectedItem); }
        public ShipIn(Window prevWindow)
        {
            InitializeComponent();
            _prevWindow = prevWindow;
            cb_article.ItemsSource = App.db.Products.ToList();
        }

        private void btn_go_back_Click(object sender, RoutedEventArgs e)
        {
            _prevWindow.Show();
            Close();
        }

        private void btn_shipin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.db.ShipmentsToAndWriteOffsFromWarehouses.Add(new Models.ShipmentsToAndWriteOffsFromWarehous
                {
                    Datetime = DateTime.Now,
                    DepartmentCode = (int)App.User.DepartmentCode,
                    Quantity = decimal.Parse(tb_value.Text),
                    ProductId = Product.ProductArticleNumber,
                    StorageWorkerId = App.User.WorkerId,
                    TypeId = App.db.TypesOfStorageMovements
                        .Where(x => x.Name == "Погрузка")
                        .First()
                        .TypeId
                });
                App.db.SaveChanges();
                btn_go_back_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cb_article_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb_name.Text = Product.ProductName;
            tb_price.Text = Product.Price.ToString();
        }
    }
}

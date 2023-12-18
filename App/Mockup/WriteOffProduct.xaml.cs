using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для WriteOffProduct.xaml
    /// </summary>
    public partial class WriteOffProduct : Window
    {
        private Window _prevWindow;
        Models.ListProductsOfDepartment Product { get => ((Models.ListProductsOfDepartment)cb_article.SelectedItem); }
        public WriteOffProduct(Window prevWindow)
        {
            InitializeComponent();
            _prevWindow = prevWindow;
            cb_article.ItemsSource = App.db.ListProductsOfDepartments
                .Where(x=>x.DepartmentCode==App.User.DepartmentCode)
                .ToList();
        }

        private void btn_writeoff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.db.ShipmentsToAndWriteOffsFromWarehouses.Add(new Models.ShipmentsToAndWriteOffsFromWarehous
                {
                    Datetime = DateTime.Now,
                    DepartmentCode = (int)App.User.DepartmentCode,
                    Quantity = decimal.Parse(tb_value.Text),
                    ProductId = Product.ProductId,
                    StorageWorkerId = App.User.WorkerId,
                    TypeId = App.db.TypesOfStorageMovements
                        .Where(x => x.Name == "Списание")
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

        private void btn_go_back_Click(object sender, RoutedEventArgs e)
        {
            _prevWindow.Show();
            Close();
        }

        private void cb_article_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb_name.Text = Product.ProductName;
            tb_price.Text = Product.Price.ToString();
        }
    }
}

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для CommiteSell.xaml
    /// </summary>
    public partial class CommiteSell : Window
    {
        private Window _prevWindow;
        Models.ListProductsOfDepartment Product { get => ((Models.ListProductsOfDepartment)cb_choose_product.SelectedItem); }
        public CommiteSell(Window prevWindow)
        {
            _prevWindow = prevWindow;
            InitializeComponent();
            cb_choose_product.ItemsSource = App.db.ListProductsOfDepartments
                .Where(x=>x.DepartmentCode==App.User.DepartmentCode)
                .ToList();
        }

        private void btn_cancel_sell_Click(object sender, RoutedEventArgs e)
        {
            _prevWindow.Show();
            Close();
        }

        private void btn_make_sell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.db.Sales.Add(new Models.Sale()
                {
                    DepartmentCode = App.User.DepartmentCode.Value,
                    WorkerId = App.User.WorkerId,
                    ProductArticle = Product.ProductId,
                    SoldQuantity = decimal.Parse(tb_product_quantity.Text),
                    Datetime = DateTime.Now
                });

                App.db.SaveChanges();
                btn_cancel_sell_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ex.InnerException.Message);
            }
        }

        private void cb_choose_product_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Product==null) return;
            tb_product_name.Text = Product.ProductName;
            tb_product_price.Text = Product.Price.ToString();
            tb_product_total_price.Text = (Product.Price * decimal.Parse(tb_product_quantity.Text+"0")).ToString();
        }

        private void tb_product_quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Product != null)
                tb_product_total_price.Text = (Product.Price * decimal.Parse(tb_product_quantity.Text+"0")).ToString();
        }
    }
}

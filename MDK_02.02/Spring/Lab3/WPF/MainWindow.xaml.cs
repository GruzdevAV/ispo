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
using WPF.Properties;
using Settings = WPF.Properties.Settings;

namespace WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Good> goods;
        public MainWindow()
        {
            InitializeComponent();
            l_orderNumber.Content = $"Заказ №{Settings.Default.DocCount+1}";
            l_date.Content = DateTime.Now.Date.ToString("d");

            goods = new List<Good>();
            goods.Add(
                new Good()
                {
                    Name = "Арбуз",
                    Price = 16,
                    Quantity = 3
                });
            goods.Add(
                new Good()
                {
                    Name = "Тыква",
                    Price = 40,
                    Quantity = 10
                });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lv_table.ItemsSource = goods;
            
        }

        private void btn_makeDoc_Click(object sender, RoutedEventArgs e)
        {
            DocWorkings.Lab3TaskWord(tbox_supplier.Text, tbox_buyer.Text, goods);
        }

        private void lv_table_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            decimal sum = ((List<Good>)lv_table.ItemsSource).Sum(x => x.Sum);
            tblock_total.Text = $"{sum} руб.";
            lv_table.ItemsSource = lv_table.ItemsSource;
        }
        
        

        private void Btn_makeExcel_Click(object sender, RoutedEventArgs e)
        {
            //DocWorkings.Test();
            DocWorkings.Lab3TaskExcel(tbox_supplier.Text, tbox_buyer.Text, goods);
        }
    }

    public class Good
    {
        public Good()
        {
            Number = ++Count;
        }
        public static int Count { get; private set; } = 0;
        public int Number { get; private set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Sum { get => Quantity * Price; }
    }
}

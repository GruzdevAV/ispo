using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для SalesList.xaml
    /// </summary>
    public partial class SalesList : Window
    {
        private Window _prevWindow;
        int _pagesCount = 0;
        int _currPage = 0;
        public SalesList(Window prevWindow)
        {
            InitializeComponent();
            _prevWindow = prevWindow;
        }

        private void btn_go_back_Click(object sender, RoutedEventArgs e)
        {
            _prevWindow.Show();
            Close();
        }
        private List<Models.ListSale> GetListSales(int page)
        {
            var filter = tb_filter.Text.ToUpper();
            System.Linq.Expressions.Expression<Func<Models.ListSale, bool>> predicate = 
                x => x.Datetime.ToString().ToUpper().Contains(tb_filter.Text) ||
                    x.Worker.ToUpper().Contains(tb_filter.Text) ||
                    x.ProductName.ToUpper().Contains(tb_filter.Text) ||
                    x.Sold.ToUpper().Contains(tb_filter.Text) ||
                    x.SalePrice.ToString().ToUpper().Contains(tb_filter.Text) ||
                    x.DepartmentName.ToUpper().Contains(tb_filter.Text);
            var items = App.db.ListSales
                .Where(x => x.WorkerId == App.User.WorkerId)
                .Where(predicate)
                .Count();
            _pagesCount = (int)Math.Ceiling(items / 10.0);
            tb_showed_elements_number.Text = $"Отображаются: {items}/{App.db.ListSales.Where(x => x.WorkerId == App.User.WorkerId).Count()}";
            return App.db.ListSales
                .Where(x => x.WorkerId == App.User.WorkerId)
                .Where(predicate)
                .OrderBy(x => x.Datetime)
                .Skip(page * 10)
                .Take(10)
                .ToList();
        }
        private void btn_first_page_Click(object sender, RoutedEventArgs e)
        {
            lv_sales.ItemsSource = GetListSales(0);
            _currPage = 0;
            CheckPage();
        }

        private void btn_previous_page_Click(object sender, RoutedEventArgs e)
        {
            lv_sales.ItemsSource = GetListSales(--_currPage);
            CheckPage();
        }

        private void btn_next_page_Click(object sender, RoutedEventArgs e)
        {
            lv_sales.ItemsSource = GetListSales(++_currPage);
            CheckPage();
        }

        private void btn_last_page_Click(object sender, RoutedEventArgs e)
        {
            _currPage = _pagesCount - 1;
            lv_sales.ItemsSource = GetListSales(_currPage);
            CheckPage();
        }

        private void btn_make_new_sale_Click(object sender, RoutedEventArgs e)
        {
            new CommiteSell(this).Show();
            Hide();
        }

        private void CheckPage()
        {
            if (_currPage == 0)
            {
                btn_first_page.IsEnabled = btn_previous_page.IsEnabled = false;
                btn_next_page.IsEnabled = btn_last_page.IsEnabled = _pagesCount > 1;
            }
            else if (_currPage == _pagesCount - 1)
            {
                btn_first_page.IsEnabled = btn_previous_page.IsEnabled = true;
                btn_next_page.IsEnabled = btn_last_page.IsEnabled = false;
            }
            else
            {
                btn_first_page.IsEnabled = btn_previous_page.IsEnabled =
                btn_next_page.IsEnabled = btn_last_page.IsEnabled = true;
            }
        }
        private void btn_drop_filter_Click(object sender, RoutedEventArgs e)
        {
            tb_filter.Text = string.Empty;
        }
        private void btn_make_return_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)sender;
                var context = (Models.ListSale)btn.DataContext;
                App.db.ReturnedSales.Add(new Models.ReturnedSale
                {
                    SaleId = context.SaleId,
                    Reason = "No reason",
                    Datetime = DateTime.Now
                });
                App.db.SaveChanges();
                lv_sales.ItemsSource = GetListSales(_currPage);
            }
            catch
            (Exception ex)
            { MessageBox.Show(ex.Message); }
            MessageBox.Show(App.db.ReturnedSales.Count().ToString());
        }

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        void GridViewColumnHeaderClickedHandler(object sender,
                                                RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }
        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(lv_sales.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void tb_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            lv_sales.ItemsSource = GetListSales(0);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            lv_sales.ItemsSource = GetListSales(0);
            CheckPage();
        }
    }
}

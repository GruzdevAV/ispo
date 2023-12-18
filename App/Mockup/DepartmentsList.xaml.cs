using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для DepartmentsList.xaml
    /// </summary>
    public partial class DepartmentsList : Window
    {
        private Window _prevWindow;
        public DepartmentsList(Window prevWindow)
        {
            InitializeComponent();
            _prevWindow = prevWindow;
            listview.ItemsSource = App.db.Departments.ToList();
        }
        private void btn_go_back_Click(object sender, RoutedEventArgs e)
        {
            _prevWindow.Show();
            Close();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var window = new CrudDepartment(this) { Department = (Models.Department)btn.DataContext };
            window.Show();
            Hide();
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
              CollectionViewSource.GetDefaultView(listview.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void btn_drop_filter_Click(object sender, RoutedEventArgs e)
        {
            tb_filter.Text = string.Empty;
        }

        private void tb_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_filter.Text.Length > 0)
                listview.ItemsSource = App.db.Departments
                    .Where(x => x.DepartmentName.ToUpper().Contains(tb_filter.Text.ToUpper())||
                                x.PhoneNumber.ToUpper().Contains(tb_filter.Text.ToUpper()) ||
                                x.PlannedSoldQuantityPerDay.ToString().ToUpper().Contains(tb_filter.Text.ToUpper()))
                    .ToList();
            else
                listview.ItemsSource = App.db.Departments
                    .ToList();

        }

    }
}

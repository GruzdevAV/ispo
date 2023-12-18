using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для WorkersList.xaml
    /// </summary>
    public partial class WorkersList : Window
    {
        private Window _prevWindow;
        private bool _head = false;
        public bool Head { 
            get => _head; 
            set
            {
                _head = value;
                if (value)
                { tb_filter_TextChanged(null, null); }
                else btn_add_worker.Visibility = Visibility.Collapsed;
            }
        }
        public WorkersList(Window prevWindow)
        {
            InitializeComponent();
            _prevWindow = prevWindow;
            listview.ItemsSource = App.db.ListWorkers.ToList();
        }
        private void btn_add_worker_Click(object sender, RoutedEventArgs e)
        {
            new AddWorker(this).Show();
            Hide();
        }
        
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        private void btn_go_back_Click(object sender, RoutedEventArgs e)
        {
            _prevWindow.Show();
            Close();
        }
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
            if (tb_filter.Text.Length> 0) 
                listview.ItemsSource = App.db.ListWorkers
                    .Where(x => (x.Surname.ToUpper().Contains(tb_filter.Text.ToUpper()) ||
                                x.Firstname.ToUpper().Contains(tb_filter.Text.ToUpper()) ||
                                x.DepartmentName.ToUpper().Contains(tb_filter.Text.ToUpper()) ||
                                x.Name.ToUpper().Contains(tb_filter.Text.ToUpper()) ||
                                x.Login.ToUpper().Contains(tb_filter.Text.ToUpper()) ||
                                x.Password.ToUpper().Contains(tb_filter.Text.ToUpper()) ||
                                x.PathToImage.ToUpper().Contains(tb_filter.Text.ToUpper())) &&
                                (!Head || x.DepartmentName == App.db.Departments.Where(y => y.DepartmentCode == App.User.DepartmentCode).FirstOrDefault().DepartmentName))
                    .ToList();
            else
                listview.ItemsSource = App.db.ListWorkers
                    .Where(x=>
                                (!Head || x.DepartmentName == App.db.Departments.Where(y => y.DepartmentCode == App.User.DepartmentCode).FirstOrDefault().DepartmentName))
                    .ToList();

        }

    }
}

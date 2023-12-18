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
    /// Логика взаимодействия для AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {
        private Window _prevWindow;
        Models.Surname Surname { get => (Models.Surname)cb_surname.SelectedItem; }
        Models.Firstname Firstname { get => (Models.Firstname)cb_firstname.SelectedItem; }
        Models.Department Department { get=> (Models.Department)cb_department.SelectedItem; }
        Models.Role Role { get=> (Models.Role)cb_role.SelectedItem; }
        Models.Image Image { get=> (Models.Image)cb_image.SelectedItem; }
        public AddWorker(Window prevWindow)
        {
            InitializeComponent();
            _prevWindow = prevWindow;
            tb_hide.Visibility = Visibility.Collapsed;
            cb_surname.ItemsSource = App.db.Surnames.ToList();
            cb_firstname.ItemsSource = App.db.Firstnames.ToList();
            cb_department.ItemsSource = App.db.Departments.ToList();
            cb_role.ItemsSource = App.db.Roles
                .Where(x=>x.Name!= "Администратор")
                .ToList();
            cb_image.ItemsSource = App.db.Images.ToList();
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
                App.db.Workers.Add(new Models.Worker
                {
                    SurnameId = Surname.SurnameId,
                    FirstnameId = Firstname.FirstnameId,
                    DepartmentCode = Department.DepartmentCode,
                    RoleId = Role.RoleId,
                    ImgId = Image.ImgId,
                    Login = tb_login.Text,
                    Password = tb_password.Text
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

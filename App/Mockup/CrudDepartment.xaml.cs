using System;
using System.Data.Entity.Migrations;
using System.Windows;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для CrudDepartment.xaml
    /// </summary>
    public partial class CrudDepartment : Window
    {
        private Window _prevWindow;
        public Models.Department Department {get; set;} = null;
        public CrudDepartment(Window prevWindow)
        {
            InitializeComponent();
            _prevWindow = prevWindow;
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
                if (Department == null)
                {
                    App.db.Departments.Add(new Models.Department
                    {
                        DepartmentName = tb_name.Text,
                        PhoneNumber = tb_phone.Text,
                        PlannedSoldQuantityPerDay = decimal.Parse("0" + tb_plan.Text)
                    });
                }
                else
                {
                    Department.DepartmentName = tb_name.Text;
                    Department.PhoneNumber = tb_phone.Text;
                    Department.PlannedSoldQuantityPerDay = decimal.Parse("0" + tb_plan.Text);
                    App.db.Departments.AddOrUpdate(Department);
                }
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

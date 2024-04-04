using DrivingSchoolAPIModels;
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

namespace DrivingSchoolGUIApp
{
    /// <summary>
    /// Логика взаимодействия для AddOuterLayout.xaml
    /// </summary>
    public partial class AddOuterSchedule : Window
    {
        public AddOuterScheduleModel Model =>
            new()
            {
                GoogleSheetId = tbox_google_sheet_id.Text,
                GoogleSheetPageName = tbox_google_sheet_page_name.Text,
                TimesOfClassesRange = tbox_times_range.Text,
                DatesOfClassesRange = tbox_dates_range.Text,
                YearRange = tbox_year_range.Text,
                FreeClassExampleRange = tbox_free_class_range.Text,
                NotFreeClassExampleRange = tbox_not_free_class_range.Text,
                ClassesRange = tbox_classes_range.Text
            };
        public AddOuterSchedule()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void tbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn_ok.IsEnabled =
                tbox_classes_range.Text.Length > 0
                && tbox_dates_range.Text.Length > 0
                && tbox_free_class_range.Text.Length > 0
                && tbox_google_sheet_id.Text.Length > 0
                && tbox_google_sheet_page_name.Text.Length > 0
                && tbox_times_range.Text.Length > 0
                && tbox_year_range.Text.Length > 0;
        }
    }
}

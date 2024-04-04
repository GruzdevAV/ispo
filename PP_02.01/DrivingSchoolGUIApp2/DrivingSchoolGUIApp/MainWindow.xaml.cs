using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DrivingSchoolAPIModels;
using DrivingSchoolAPIModels.ApiModels;
using MessageBox = System.Windows.MessageBox;

namespace DrivingSchoolGUIApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EditMeModel _editMe;
        object _me;
        List<Instructor> _instructors;
        List<Student> _students;
        List<Class> _classes;
        List<Class> _myClasses;
        List<InnerScheduleOfInstructor> _schedules;
        List<InnerScheduleOfInstructor> _mySchedules;
        List<GradeByInstructorToStudent> _gradesByInstructorsToStudents;
        List<GradeByStudentToInstructor> _gradesByStudentsToInstructors;
        List<StudentRating> _studentRatings;
        List<InstructorRating> _instructorRatings;
        List<Student> _myStudents;
        List<StudentRating> _myStudentRatings;
        Instructor _myInstructor;
        InstructorRating _myInstructorRating;

        List<Class> Classes => cbShowMyClasses.IsChecked == true ? _myClasses : _classes;
        List<InnerScheduleOfInstructor> Schedules => cbShowMySchedules.IsChecked == true ? _mySchedules : _schedules;
        List<Student> Students => cbShowMyStudents.IsChecked == true ? _myStudents : _students;
        List<StudentRating> StudentRatings => cbShowMyStudents.IsChecked == true ? _myStudentRatings : _studentRatings;
        List<InstructorRating> InstructorRatings => cbShowMyInstructor.IsChecked == true ? new List<InstructorRating>(new[] { _myInstructorRating }) : _instructorRatings;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnMeChange_Click(object sender, RoutedEventArgs e)
        {
            if (btnMeSaveChanges.IsEnabled)
            {
                btnMeChange.Content = "Именить";

                txtMeFirstName.IsEnabled =
                txtMeLastName.IsEnabled =
                txtMePatronym.IsEnabled =
                txtMePhone.IsEnabled =
                btnMeSaveChanges.IsEnabled = false;

                btnMeSaveChanges.Visibility = Visibility.Collapsed;
                txtMeFirstName.Text = _editMe.FirstName;
                txtMeLastName.Text = _editMe.LastName;
                txtMePatronym.Text = _editMe.Patronym;
                txtMePhone.Text = _editMe.PhoneNumber;
                switch (APIClass.LoginResponse.Role)
                {
                    case UserRoles.Student:
                        ReloadStudents();
                        break;
                    case UserRoles.Instructor:
                        ReloadInstructors();
                        break;
                }
                return;
            }
            btnMeChange.Content = "Отменить";
            txtMeFirstName.IsEnabled =
            txtMeLastName.IsEnabled =
            txtMePatronym.IsEnabled = APIClass.LoginResponse.Role != UserRoles.Admin;
            txtMePhone.IsEnabled =
            btnMeSaveChanges.IsEnabled = true;

            btnMeSaveChanges.Visibility = Visibility.Visible;
            _editMe = new EditMeModel
            {
                FirstName = txtMeFirstName.Text,
                LastName = txtMeLastName.Text,
                Patronym = txtMePatronym.Text,
                PhoneNumber = txtMePhone.Text
            };

        }

        private async void MePage_Loaded(object sender, RoutedEventArgs e)
        {
            lblMeRole.Content = APIClass.LoginResponse.Role;
            switch (APIClass.LoginResponse.Role)
            {
                case UserRoles.Admin:
                    var admin = (ApplicationUser)await APIClass.GetMe();
                    lblMeEmail.Content = admin.Email;
                    txtMeFirstName.Text = "-";
                    txtMeLastName.Text = "-";
                    txtMePatronym.Text = "-";
                    txtMePhone.Text = admin.PhoneNumber;
                    break;
                case UserRoles.Student:
                    var student = (Student)await APIClass.GetMe();
                    lblMeEmail.Content = student.User.Email;
                    txtMeFirstName.Text = student.FirstName;
                    txtMeLastName.Text = student.LastName;
                    txtMePatronym.Text = student.Patronym;
                    txtMePhone.Text = student.User.PhoneNumber;
                    break;
                case UserRoles.Instructor:
                    var instructor = (Instructor)await APIClass.GetMe();
                    lblMeEmail.Content = instructor.User.Email;
                    txtMeFirstName.Text = instructor.FirstName;
                    txtMeLastName.Text = instructor.LastName;
                    txtMePatronym.Text = instructor.Patronym;
                    txtMePhone.Text = instructor.User.PhoneNumber;
                    break;
            }
        }

        private async void btnMeSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var me = new EditMeModel
                {
                    FirstName = txtMeFirstName.Text,
                    LastName = txtMeLastName.Text,
                    Patronym = txtMePatronym.Text,
                    PhoneNumber = txtMePhone.Text,
                    Id = APIClass.LoginResponse.Id

                };

                var resp = await APIClass.EditMe(me);
                resp.EnsureSuccessStatusCode();
                _editMe = me;
                btnMeChange_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnRegisterRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var regModel = new RegisterModel
                {
                    Password = txtRegisterPassword.Password,
                    FirstName = txtRegisterFirstName.Text,
                    LastName = txtRegisterLastName.Text,
                    Patronym = string.IsNullOrEmpty(txtRegisterPatronym.Text) ? null : txtRegisterPatronym.Text,
                    PhoneNumber = string.IsNullOrEmpty(txtRegisterPhone.Text) ? null : txtRegisterPhone.Text,
                    Email = txtRegisterEmail.Text
                };

                Func<RegisterModel, Task<HttpResponseMessage>> regFunc;
                Action updateFunc;
                if (rbInstructor.IsChecked == true)
                {
                    regFunc = APIClass.RegisterInstructor;
                    updateFunc = ReloadInstructors;
                }
                else if (rbStudent.IsChecked == true)
                {
                    regFunc = APIClass.RegisterStudent;
                    updateFunc = ReloadStudents;
                }
                else throw new Exception("Как-то ни ученик, ни инструктор не выбраны.");
                var res = await regFunc(regModel);
                if (!res.IsSuccessStatusCode)
                    throw new Exception(await APIClass.GetErrorsFromContent(res.Content));
                updateFunc();
                txtRegisterPassword.Password =
                txtRegisterFirstName.Text =
                txtRegisterLastName.Text =
                txtRegisterPatronym.Text =
                txtRegisterPhone.Text =
                txtRegisterEmail.Text = "";
                rbInstructor.IsChecked = rbStudent.IsChecked = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbRegister_Checked(object sender, RoutedEventArgs e)
        {
            btnRegisterRegister.IsEnabled = RegisterFormFilled();
        }

        private bool RegisterFormFilled()
        {
            return (rbStudent.IsChecked == true
                || rbInstructor.IsChecked == true)
                && txtRegisterPassword.Password.Length > 4
                && txtRegisterFirstName.Text.Length > 0
                && txtRegisterLastName.Text.Length > 0
                && System.Net.Mail.MailAddress.TryCreate(txtRegisterEmail.Text, out var _);
        }

        private void txtRegister_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnRegisterRegister.IsEnabled = RegisterFormFilled();
        }

        private void txtRegister_TextChanged(object sender, RoutedEventArgs e)
        {
            btnRegisterRegister.IsEnabled = RegisterFormFilled();

        }

        private async void StudentsPage_GotFocus(object sender, RoutedEventArgs e)
        {
            lvStudents.ItemsSource = StudentRatings;
            cbStudentsInstructor.ItemsSource = _instructors;
        }

        private async void btnAssignInstructorToStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var student = (Student)lvStudents.SelectedItem;
                var instructor = (Instructor)cbStudentsInstructor.SelectedItem;

                var res = await APIClass.SetInstructorToStudent(new InstructorStudentPairModel
                {
                    InstructorId = instructor.InstructorId,
                    StudentId = student.StudentId
                });
                if (res.IsSuccessStatusCode == false)
                    throw new Exception(await APIClass.GetErrorsFromContent(res.Content));
                ReloadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _classes = await APIClass.GetClasses();
            _schedules = await APIClass.GetInnerSchedules();
            ReloadStudents();
            ReloadInstructors();
            _me = await APIClass.GetMe();

            switch (APIClass.LoginResponse.Role)
            {
                case UserRoles.Admin:
                    PageRegister.Visibility = Visibility.Visible;
                    PageGrades.Visibility = Visibility.Collapsed;
                    tbStudentsForAdmin.Visibility = Visibility.Visible;
                    tbSchedulesForAdmin1.Visibility = Visibility.Visible;
                    tbSchedulesForAdmin2.Visibility = Visibility.Visible;
                    tbSchedulesForInstructorAndStudent.Visibility = Visibility.Collapsed;
                    tbSchedulesForInstructor.Visibility = Visibility.Collapsed;
                    tbtClasses.Visibility = Visibility.Collapsed;
                    tbtInstructors.Visibility = Visibility.Collapsed;
                    tbtStudents.Visibility = Visibility.Visible;
                    tbStudentsForInstructor.Visibility = Visibility.Collapsed;
                    break;
                case UserRoles.Student:
                    PageRegister.Visibility = Visibility.Collapsed;
                    PageGrades.Visibility = Visibility.Visible;
                    tbStudentsForAdmin.Visibility = Visibility.Collapsed;
                    tbSchedulesForAdmin1.Visibility = Visibility.Collapsed;
                    tbSchedulesForAdmin2.Visibility = Visibility.Collapsed;
                    tbSchedulesForInstructorAndStudent.Visibility = Visibility.Visible;
                    tbSchedulesForInstructor.Visibility = Visibility.Collapsed;
                    tbtClasses.Visibility = Visibility.Visible;
                    tbtInstructors.Visibility = Visibility.Visible;
                    tbtStudents.Visibility = Visibility.Collapsed;
                    btnAddClass.Content = "Добавить занятие себе";
                    cbClassesStudent.Visibility = Visibility.Collapsed;
                    cbGrade.ItemsSource = Enum
                    .GetValues(typeof(GradesByStudentsToInstructors))
                    .Cast<GradesByStudentsToInstructors>();
                    //cbClassesSchedule.ItemsSource = _mySchedules;
                    tbStudentsForInstructor.Visibility = Visibility.Collapsed;
                    cbShowMyClasses.IsChecked = true;


                    _mySchedules = await APIClass.GetInnerSchedulesOfMyInstructor();
                    _myClasses = await APIClass.GetClassesOfStudent();
                    _gradesByStudentsToInstructors = await APIClass.GetGradesByStudent();
                    _gradesByInstructorsToStudents = await APIClass.GetGradesToStudent();

                    break;
                case UserRoles.Instructor:
                    _mySchedules = await APIClass.GetMyInnerSchedules();
                    _myClasses = await APIClass.GetClassesOfInstructor();
                    PageRegister.Visibility = Visibility.Collapsed;
                    PageGrades.Visibility = Visibility.Visible;
                    tbStudentsForAdmin.Visibility = Visibility.Collapsed;
                    tbSchedulesForAdmin1.Visibility = Visibility.Collapsed;
                    tbSchedulesForAdmin2.Visibility = Visibility.Collapsed;
                    tbSchedulesForInstructorAndStudent.Visibility = Visibility.Visible;
                    tbSchedulesForInstructor.Visibility = Visibility.Visible;
                    tbtClasses.Visibility = Visibility.Visible;
                    tbtInstructors.Visibility = Visibility.Collapsed;
                    tbtStudents.Visibility = Visibility.Visible;
                    cbClassesStudent.Visibility = Visibility.Visible;
                    cbGrade.ItemsSource = Enum
                    .GetValues(typeof(GradesByInstructorsToStudents))
                    .Cast<GradesByInstructorsToStudents>();
                    //cbClassesSchedule.ItemsSource = _mySchedules;
                    _gradesByStudentsToInstructors = await APIClass.GetGradesToInstructor();
                    _gradesByInstructorsToStudents = await APIClass.GetGradesByInstructor();
                    tbStudentsForInstructor.Visibility = Visibility.Visible;
                    cbShowMyClasses.IsChecked = true;
                    break;
            }
        }
        // TODO: Redo
        private async void btnAddSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (dtpSchedulesDateStart.Value == null || dtpSchedulesDateEnd.Value == null
                //    || cbSchedulesInstructor.SelectedItem.GetType() != typeof(Instructor))
                //    throw new Exception("Сначала выберите даты и инструктора");
                var classes = new List<ClassModel>
                {
                    new()
                    {
                        StartTime = new TimeSpan(9,0,0),
                        Duration = new TimeSpan(1,30,0)
                    },
                    new()
                    {
                        StartTime = new TimeSpan(10,30,0),
                        Duration = new TimeSpan(1,30,0)
                    },
                    new()
                    {
                        StartTime = new TimeSpan(12,0,0),
                        Duration = new TimeSpan(1,30,0)
                    },
                    new()
                    {
                        StartTime = new TimeSpan(13,30, 0),
                        Duration = new TimeSpan(1,30,0)
                    },
                    new()
                    {
                        StartTime = new TimeSpan(15,30, 0),
                        Duration = new TimeSpan(1,30,0)
                    },
                    new()
                    {
                        StartTime = new TimeSpan(17,0, 0),
                        Duration = new TimeSpan(1,30,0)
                    },
                    new()
                    {
                        StartTime = new TimeSpan(18,30, 0),
                        Duration = new TimeSpan(1,30,0)
                    }
                }.GetRange(0, Convert.ToInt32(sliderClasses.Value));
                var date = DateOnly.FromDateTime((DateTime)dpSchedulesDate.SelectedDate);
                var res = await APIClass.SetInnerSchedule(new InstructorScheduleModel
                {
                    InstructorId = ((Instructor)cbSchedulesInstructor.SelectedItem).InstructorId,
                    DayOfWork = date,
                    Classes = classes.ToArray(),
                });
                if (!res.IsSuccessStatusCode)
                    throw new Exception(await APIClass.GetErrorsFromContent(res.Content));
                lvSchedules.ItemsSource = _schedules = await APIClass.GetInnerSchedules();
                lvClasses.ItemsSource = _classes = await APIClass.GetClasses();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PageSchedules_GotFocus(object sender, RoutedEventArgs e)
        {
            lvSchedules.ItemsSource = Schedules;
            cbSchedulesInstructor.ItemsSource = _instructors;
        }

        private void PageClasses_GotFocus(object sender, RoutedEventArgs e)
        {
            //cbClassesSchedule.ItemsSource = _mySchedules;
            cbClassesStudent.ItemsSource = _myStudents;
            lvClasses.ItemsSource = Classes;
        }

        private void PageInstructors_GotFocus(object sender, RoutedEventArgs e)
        {
            lvInstructors.ItemsSource = InstructorRatings;
        }
        // TODO: REDO
        private async void btnAddClass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var schedule = (InnerScheduleOfInstructor)cbClassesSchedule.SelectedItem;
                var @class = lvClasses.SelectedItem as Class;
                var student = (Student)(APIClass.LoginResponse.Role == UserRoles.Student ?
                    _me :
                     cbClassesStudent.SelectedItem);
                if (/*timeStart == null || timeEnd == null
                    || */student == null || @class == null)
                    throw new Exception("Сначала выберите ученика и расписание");
                var res = await APIClass.SetClass(new ClassStudentPairModel
                {
                    ClassId = @class.ClassId,
                    StudentId = student.StudentId,
                });
                if (!res.IsSuccessStatusCode)
                    throw new Exception(await APIClass.GetErrorsFromContent(res.Content));
                await ReloadClasses();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task ReloadClasses()
        {
            _classes = await APIClass.GetClasses();
            _myClasses = APIClass.LoginResponse.Role == UserRoles.Student ?
                await APIClass.GetClassesOfStudent() :
                await APIClass.GetClassesOfInstructor();
        }

        private async void btnCancelClass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var @class = (Class)lvClasses.SelectedItem;
                var res = await APIClass.CancelClass(@class.ClassId);
                if (res.IsSuccessStatusCode == false)
                    throw new Exception(await APIClass.GetErrorsFromContent(res.Content));
                await ReloadClasses();
                lvClasses.UnselectAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btmMeExit_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }

        private async void btnSetGradeForClass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var @class = (Class)lvClasses.SelectedItem;
                var grade = (byte)cbGrade.SelectedValue;
                switch (APIClass.LoginResponse.Role)
                {
                    case UserRoles.Student:
                        var gradeByStuden = new GradeByStudentToInstructorModel
                        {
                            ClassId = @class.ClassId,
                            Grade = (GradesByStudentsToInstructors)grade,
                            Comment = string.IsNullOrWhiteSpace(txtCommentForGrade.Text) ? null : txtCommentForGrade.Text
                        };

                        var res1 = await APIClass.PostGradeToInstructorForClass(gradeByStuden);
                        if (!res1.IsSuccessStatusCode)
                            throw new Exception(await APIClass.GetErrorsFromContent(res1.Content));
                        _gradesByStudentsToInstructors = await APIClass.GetGradesByStudent();
                        ReloadInstructors();
                        break;
                    case UserRoles.Instructor:
                        var gradeByInstructor = new GradeByInstructorToStudentModel
                        {
                            ClassId = @class.ClassId,
                            Grade = (GradesByInstructorsToStudents)grade,
                            Comment = string.IsNullOrWhiteSpace(txtCommentForGrade.Text) ? null : txtCommentForGrade.Text
                        };

                        var res2 = await APIClass.PostGradeToStudentForClass(gradeByInstructor);
                        if (!res2.IsSuccessStatusCode)
                            throw new Exception(await APIClass.GetErrorsFromContent(res2.Content));
                        _gradesByInstructorsToStudents = await APIClass.GetGradesByInstructor();
                        ReloadStudents();
                        break;
                    default:
                        return;
                }
                lvClasses.UnselectAll();
                cbGrade.Text = string.Empty; cbGrade.SelectedIndex = -1;
                await ReloadClasses();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ReloadStudents()
        {
            _students = await APIClass.GetStudents();
            _studentRatings = await APIClass.GetStudentRatings();
            if (APIClass.LoginResponse.Role == UserRoles.Instructor)
            {
                _myStudents = await APIClass.GetMyStudents();
                _myStudentRatings = await APIClass.GetMyStudentsRatings();
            }
        }

        private async void ReloadInstructors()
        {
            _instructors = await APIClass.GetInstructors();
            _instructorRatings = await APIClass.GetInstructorRatings();
            if (APIClass.LoginResponse.Role == UserRoles.Student)
            {
                _myInstructor = await APIClass.GetMyInstructor();
                _myInstructorRating = _instructorRatings
                    .FirstOrDefault(x => x.InstructorId == ((Student)_me).InstructorId);
            }

        }

        private async void cbClassesStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var student = (Student)(APIClass.LoginResponse.Role == UserRoles.Student ?
                _me :
                 cbClassesStudent.SelectedItem);
            //if (student == null)
            //{
            //    cbClassesSchedule.ItemsSource = _schedules;
            //    return;
            //}
        }

        private void cbShowMyInstructor_Changed(object sender, RoutedEventArgs e)
        {
            lvInstructors.ItemsSource = InstructorRatings;
        }

        private void cbShowMyStudents_Changed(object sender, RoutedEventArgs e)
        {
            lvStudents.ItemsSource = StudentRatings;
        }

        private void cbShowMySchedules_Changed(object sender, RoutedEventArgs e)
        {
            lvSchedules.ItemsSource = Schedules;
        }

        private void cbShowMyClasses_Changed(object sender, RoutedEventArgs e)
        {
            lvClasses.ItemsSource = Classes;
            tbClassesForInstructorAndStudent1.Visibility =
            //tbClassesForInstructorAndStudent2.Visibility =
            tbClassesForInstructorAndStudent3.Visibility =
            tbClassesForInstructorAndStudent4.Visibility =
            (cbShowMyClasses.IsChecked == false ? Visibility.Collapsed : Visibility.Visible);
        }
        private void PageGrades_GotFocus(object sender, RoutedEventArgs e)
        {
            switch (APIClass.LoginResponse.Role)
            {
                case UserRoles.Student:
                    lvGradesByMe.ItemsSource = _gradesByStudentsToInstructors;
                    lvGradesToMe.ItemsSource = _gradesByInstructorsToStudents;
                    break;
                case UserRoles.Instructor:
                    lvGradesByMe.ItemsSource = _gradesByInstructorsToStudents;
                    lvGradesToMe.ItemsSource = _gradesByStudentsToInstructors;
                    break;
            }
        }

        private void sliderClasses_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sliderClasses.Value = Convert.ToInt32(e.NewValue);
        }

        private async void btnAddOuterSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new AddOuterSchedule();
                if (dialog.ShowDialog() == true)
                {
                    AddOuterScheduleModel model = dialog.Model;
                    model.UserId = APIClass.LoginResponse.Id;
                    var res = await APIClass.AddOuterScheduleToMe(model);
                    if (res.IsSuccessStatusCode == false)
                        throw new Exception(await APIClass.GetErrorsFromContent(res.Content));

                    _schedules = await APIClass.GetInnerSchedules();
                    _classes = await APIClass.GetClasses();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

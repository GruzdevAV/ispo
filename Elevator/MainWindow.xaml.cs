using System;
using System.Timers;
using System.Windows;
using ElevatorClass;

namespace Elevator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ElevatorClass.Elevator elevator;
        Timer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(TimerEvent);
            timer.Enabled = true;
            Dispatcher.BeginInvoke((Action)(() => ElevatorMessage.Content = elevator.GetCondition()));
        }

        private void SliderFloors_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MakeNewElevator(Convert.ToInt32(SliderFloors.Value));
        }

        private void MakeNewElevator(int floors)
        {
            elevator = new ElevatorClass.Elevator(floors);
            FloorsLabel.Content = $"Выберите число этажей: {floors}";
            //Dispatcher.BeginInvoke((Action)(() => ComboBoxFloors.Items.Clear()));
            ComboBoxFloors.ItemsSource = elevator.Floors;
        }

        private async void TimerEvent(object o, ElapsedEventArgs e)
        {
            elevator.Move();
            // Костыль!
            await Dispatcher.BeginInvoke((Action)(() =>
                {
                    ElevatorMessage.Content = elevator.GetCondition();
                    ComboBoxFloors.Items.Refresh();
                    var temp = ComboBoxFloors.SelectedIndex;
                    ComboBoxFloors.SelectedIndex = -1;
                    ComboBoxFloors.SelectedIndex = temp;
                }
            ));

        }

        private void CallButton_Click(object sender, RoutedEventArgs e)
        {
            elevator.CallElevator(((FloorItem)ComboBoxFloors.SelectedItem).Number);
            ComboBoxFloors.Items.Refresh();
            var temp = ComboBoxFloors.SelectedIndex;
            ComboBoxFloors.SelectedIndex = -1;
            ComboBoxFloors.SelectedIndex = temp;
            //ComboBoxFloors.ItemsSource = elevator.GetFloors();
        }
    }
}

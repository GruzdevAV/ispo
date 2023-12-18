using System.Windows;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Models.UP_02_01_DBEntities db { get; set; } = new Models.UP_02_01_DBEntities();
        public static Models.Worker User { get; set; }
    }
}

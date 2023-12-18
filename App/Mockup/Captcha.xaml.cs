using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;
using Point = System.Drawing.Point;

namespace Mockup
{
    /// <summary>
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        private string text = String.Empty;
        private Window _prevWindow;
        public Captcha(Window prevWindow)
        {
            InitializeComponent();
            _prevWindow = prevWindow;
        }

        private BitmapSource CreateImage(int Width, int Height)
        {
            Random rnd = new Random();
            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);

            //Вычислим позицию текста
            int Xpos = rnd.Next(0, Width - 50);
            int Ypos = rnd.Next(15, Height - 15);

            //Добавим различные цвета
            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((Image)result);

            //Пусть фон картинки будет серым
            g.Clear(Color.Gray);

            //Сгенерируем текст
            text = string.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            //Нарисуем сгенирируемый текст
            g.DrawString(text,
                         new Font("Ink Free", 15),
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));

            //Добавим немного помех
            /////Линии из углов
            g.DrawLine(Pens.Black,
                       new Point(0, 0),
                       new Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
                       new Point(0, Height - 1),
                       new Point(Width - 1, 0));
            ////Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            return Imaging.CreateBitmapSourceFromHBitmap(
                result.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        private void btn_regenerate_captcha_Click(object sender, RoutedEventArgs e)
        {
            img_captcha.Source= CreateImage((int)img_captcha.Width, (int)img_captcha.Height);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            img_captcha.Source = CreateImage((int)img_captcha.Width, (int)img_captcha.Height);
        }

        private void btn_enter_capthca_Click(object sender, RoutedEventArgs e)
        {
            if (tb_captcha_entry_field.Text != this.text)
            {
                MessageBox.Show("Ошибка!");
                return;
            }
            _prevWindow.Show();
            Close();
        }
    }
}

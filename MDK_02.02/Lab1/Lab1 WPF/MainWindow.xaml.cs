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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Beverage beverage;
        double money;
        public MainWindow()
        {
            InitializeComponent();
            money = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            money += double.Parse('0' + InputMoney.Text);
            InputMoney.Text = "";
            InputedMoney.Content = $"Внесённая сумма: {money}";
            UpdatePrice();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            RBAmericano.IsChecked = RBCappuccino.IsChecked = RBEspresso.IsChecked = RBCocoa.IsChecked = OptionMilk.IsChecked = OptionSugar.IsChecked = false;
            money -= beverage.Cost();
            InputedMoney.Content = $"Внесённая сумма: {money}";
            BeveragePrice.Content = $"Цена напитка:";
            ChangeMoney.Content = $"Сдача:";
            BtnOK.IsEnabled = false;
            beverage = null;
            UpdatePrice();
        }

        private void RB_Checked(object sender, RoutedEventArgs e)
        {
            BtnOK.IsEnabled = true;
            UpdatePrice();
        }
        private void UpdatePrice()
        {
            if ((bool)RBAmericano.IsChecked)
            {
                beverage = new Americano();
                ImgBeverage.Source = new BitmapImage(new Uri(@"/americano.png", UriKind.Relative));
            }
            else if ((bool)RBCappuccino.IsChecked)
            {
                beverage = new Cappuccino();
                ImgBeverage.Source = new BitmapImage(new Uri(@"/cappuccino.png", UriKind.Relative));
            }
            else if ((bool)RBEspresso.IsChecked)
            {
                beverage = new Espresso();
                ImgBeverage.Source = new BitmapImage(new Uri(@"/espresso.png", UriKind.Relative));
            }
            else if ((bool)RBCocoa.IsChecked)
            {
                beverage = new Cocoa();
                ImgBeverage.Source = new BitmapImage(new Uri(@"/cocoa.png", UriKind.Relative));
            }
            else ImgBeverage.Source=null;
            if ((bool)OptionMilk.IsChecked)
            {
                beverage = new Milk(beverage);
                ImgMilk.Visibility = Visibility.Visible;
            } else ImgMilk.Visibility = Visibility.Hidden;
            if ((bool)OptionSugar.IsChecked)
            {
                beverage = new Sugar(beverage);
                ImgSugar.Visibility = Visibility.Visible;
            }
            else ImgSugar.Visibility = Visibility.Hidden;
            var cost = beverage?.Cost();
            BeveragePrice.Content = $"Цена напитка: {cost}";
            if (money >= cost)
                ChangeMoney.Content = $"Сдача: {money - cost}";

        }
        private void Options_Click(object sender, RoutedEventArgs e)
        {
            UpdatePrice();
        }

        private void InputMoney_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!Char.IsNumber(c)) { e.Handled = true; break; }
            }
        }
    }
    public abstract class Beverage
    {
        protected string description = "Unknown Beverage";

        public virtual string GetDescription()
        {
            return description;
        }

        public abstract double Cost();
    }
    public class HouseBlend : Beverage
    {
        public HouseBlend()
        {
            description = "House Blend Coffee";
        }
        public override double Cost()
        {
            return 0.89;
        }
    }
    public class Americano : Beverage
    {
        public Americano()
        {
            description = "Americano Coffee";
        }
        public override double Cost()
        {
            return 0.89;
        }
    }
    public class Cocoa : Beverage
    {
        public Cocoa()
        {
            description = "Cocoa Coffee";
        }
        public override double Cost()
        {
            return 0.89;
        }
    }
    public class Cappuccino : Beverage
    {
        public Cappuccino()
        {
            description = "Cappuccino Coffee";
        }
        public override double Cost()
        {
            return 0.89;
        }
    }

    public class DarkRoast : Beverage
    {
        public DarkRoast()
        {
            description = "Dark Roast Coffee";
        }
        public override double Cost()
        {
            return 1.29;
        }
    }
    public class Espresso : Beverage
    {
        public Espresso()
        {
            description = "Espresso Coffee";
        }

        public override double Cost()
        {
            return 1.99;
        }
    }
    public class Decaf : Beverage
    {
        public Decaf()
        {
            description = "Decaf Coffee";
        }

        public override double Cost()
        {
            return 0.79;
        }
    }
    public abstract class CondimentDecorator : Beverage
    {
        protected Beverage beverage;
        public override abstract string GetDescription();
    }
    public class Milk : CondimentDecorator
    {
        public Milk(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override double Cost()
        {
            return beverage.Cost() + .3;
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Milk";
        }
    }
    public class Mocha : CondimentDecorator
    {
        public Mocha(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override double Cost()
        {
            return beverage.Cost() + .2;
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Mocha";
        }
    }
    public class Soy : CondimentDecorator
    {
        public Soy(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override double Cost()
        {
            return beverage.Cost() + .1;
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Soy";
        }
    }
    public class Whip : CondimentDecorator
    {
        public Whip(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override double Cost()
        {
            return beverage.Cost() + .25;
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Whip";
        }
    }
    public class Sugar : CondimentDecorator
    {
        public Sugar(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override double Cost()
        {
            return beverage.Cost() + .25;
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Sugar";
        }
    }

}

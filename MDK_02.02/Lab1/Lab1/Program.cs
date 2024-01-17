using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
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
    class Program
    {
        static void Main(string[] args)
        {
            Beverage beverage = new Espresso();
            beverage = new Soy(beverage);
            Console.WriteLine(beverage.GetDescription() + " $"
            + beverage.Cost());

            Beverage beverage3 = new HouseBlend();

            beverage3 = new Soy(beverage3);

            beverage3 = new Mocha(beverage3);

            beverage3 = new Mocha(beverage3);

            beverage3 = new Whip(beverage3);

            Console.WriteLine(beverage3.GetDescription() + " $"
            + beverage3.Cost());
            Beverage beverage2 = new Whip(new Mocha(new Mocha(new Soy(new HouseBlend()))));
            Console.WriteLine(beverage2.GetDescription() + " $" + beverage2.Cost());
            Console.ReadLine();
        }
    }
}

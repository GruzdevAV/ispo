using Lab3.Models;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Lab3.Infrastructure
{
    public class NinjectRegistrations :NinjectModule
    {
        public override void Load()
        {
            Bind<IValueCalculator>().To<LinqValueCalculator>().InRequestScope();
            //Bind<IDiscountHelper>().To<DefaultDiscountHelper>().
            //    WithPropertyValue("DiscountSize",50m);
            Bind<IDiscountHelper>().To<DefaultDiscountHelper>().
                WithConstructorArgument("discountParam",10m);
            Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().
                WhenInjectedInto<LinqValueCalculator>();

        }
    }
}
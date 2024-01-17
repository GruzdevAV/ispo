using Lab3.Infrastructure;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lab3
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}

using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Restaurant.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.MapPageRoute(
        //        "About",
        //        "About",
        //        "~/Paginas/About.aspx"
        //    );
        //    routes.MapPageRoute(
        //        "contact",
        //        "contact",
        //        "~/Paginas/contact.aspx"
        //    );
        //    routes.MapPageRoute(
        //        "default",
        //        "default",
        //        "~/Paginas/default.aspx"
        //    );
        //}
    }
}
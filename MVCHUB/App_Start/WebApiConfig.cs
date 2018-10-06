using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace MVCHUB.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            IHttpRoute defaultRoute = config.Routes.CreateRoute("api/{controller}/{id}",
                                             new { controller = "HomeController", id = RouteParameter.Optional }, null);

            // Add route
            config.Routes.Add("DefaultApi", defaultRoute);



        }
    }
}
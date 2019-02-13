using Autofac;
using Autofac.Integration.WebApi;
using ServiceAPI.App_Start;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
namespace ServiceAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired().InstancePerRequest();
            //builder.RegisterType<AccountServices>().As<IAccountService>();
            //builder.RegisterType<DBContext>().As<IDBContext>();

            var container = builder.Build();
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                new MediaTypeHeaderValue("text/html"));

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
                new Newtonsoft.Json.Converters.StringEnumConverter());
        }
    }
}

using System.Web.Http;
using adventure_forks_database;
using Autofac;
using Autofac.Integration.WebApi;

namespace adventure_forks_api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<DatabaseService>().As<IDatabaseService>();
            var container = containerBuilder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

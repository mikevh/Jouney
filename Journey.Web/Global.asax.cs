using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Journey.Web.App_Start;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Journey.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start() {
            var settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;

            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            AutoMapperConfig.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}

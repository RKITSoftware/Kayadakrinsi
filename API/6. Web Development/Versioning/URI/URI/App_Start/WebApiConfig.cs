using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace URI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                            name: "CompanyV1",
                            routeTemplate: "api/v1/company/{id}",
                            defaults: new { controller = "CLCompanyV1", id = RouteParameter.Optional }
                        );

            config.Routes.MapHttpRoute(
                name: "CompanyV2",
                routeTemplate: "api/v2/company/{id}",
                defaults: new { controller = "CLCompanyV2", id = RouteParameter.Optional }
            );

        }
    }
}
 
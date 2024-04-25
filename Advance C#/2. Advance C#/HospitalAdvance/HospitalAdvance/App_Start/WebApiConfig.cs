using System.Web.Http;
using System.Web.Http.Cors;
using HospitalAdvance.Auth;
using HospitalAdvance.Filter;

namespace HospitalAdvance
{
	public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Web API configuration and services
			var cors = new EnableCorsAttribute("https://www.google.com", "*", "*");

			config.EnableCors(cors);

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Registers and applys custom filters globally sequence matters
            config.Filters.Add(new CustomExceptionFilterAttribute());
            config.Filters.Add(new BasicAuthentication());
            config.Filters.Add(new BearerAuthentication());
        }
    }
}

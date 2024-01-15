using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace QueryString.Controllers
{
    public class CustomSelectorController : DefaultHttpControllerSelector
    {
        HttpConfiguration _config;
        public CustomSelectorController(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //returns all possible API Controllers
            var controllers = GetControllerMapping();

            //return the information about the route
            var routeData = request.GetRouteData();

            //get the controller name passed
            var controllerName = routeData.Values["controller"].ToString();
            string apiVersion = "1";

            //get querystring from the URI
            var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);
            if (versionQueryString["version"] != null)
            {
                apiVersion = Convert.ToString(versionQueryString["version"]);
            }
            if (apiVersion == "1")
            {
                controllerName = controllerName + "V1";
            }
            else
            {
                controllerName = controllerName + "V2";
            }

            //Custom Header Name to be check
            //string customHeaderForVersion = "X-Company-Version";
            //if (request.Headers.Contains(customHeaderForVersion))
            //{
            //    apiVersion = request.Headers.GetValues(customHeaderForVersion).FirstOrDefault();
            //}

            //if (apiVersion == "1")
            //{
            //    controllerName = controllerName + "V1";
            //}
            //else
            //{
            //    controllerName = controllerName + "V2";
            //}

            HttpControllerDescriptor controllerDescriptor;
           
            //check the value in controllers dictionary. TryGetValue is an efficient way to check the value existence
            if (controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            return null;
        }
    }
}

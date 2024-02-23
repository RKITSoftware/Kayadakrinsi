using System.Configuration;
using System.Web.Http;
using ServiceStack.OrmLite;

namespace HospitalAdvance
{
	public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
			var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
			Application["dbFactory"] = dbFactory;
		}
    }
}

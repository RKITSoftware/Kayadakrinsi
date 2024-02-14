using System;
using System.IO;
using System.Web;
using System.Web.Http.Filters;

namespace HospitalAPI.Filters
{
    /// <summary>
    /// Filter for handling not implemented methods
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public string path = HttpContext.Current.Server.MapPath("~/Log") + "\\" + DateTime.Now.ToString("dd-mm-yyyy") + ".txt";
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is Exception)
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine(context.Exception.StackTrace);
                    sw.WriteLine('\n');
                }
            }
        }
    }
}
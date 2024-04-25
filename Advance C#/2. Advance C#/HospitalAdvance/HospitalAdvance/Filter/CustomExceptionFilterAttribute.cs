using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Filters;

namespace HospitalAdvance.Filter
{
    /// <summary>
    /// Filter for Exception Logging
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Path to exception log file
        /// </summary>
        public string path = HttpContext.Current.Server.MapPath("~/Log") + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

        /// <summary>
        /// Logs exception details into file on encountring exception
        /// </summary>
        /// <param name="context">Current executed context</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is Exception)
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(@"Time : {0}", DateTime.Now);
                    sw.WriteLine(@"Type : {0}", context.Exception.GetType());

                    var s = new StackTrace(context.Exception);
                    var thisAssembly = Assembly.GetExecutingAssembly();
                    var methodname = s.GetFrames().Select(f => f.GetMethod()).First(m => m.Module.Assembly == thisAssembly).Name;

                    sw.WriteLine(@"Method : {0}", methodname);
                    sw.WriteLine(@"Details : {0}", context.Exception.StackTrace);

                    sw.WriteLine('\n');
                }
            }
        }

    }
}
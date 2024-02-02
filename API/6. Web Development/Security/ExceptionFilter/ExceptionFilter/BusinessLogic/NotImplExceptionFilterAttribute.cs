﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace ExceptionFilter.BusinessLogic
{
    /// <summary>
    /// Filter for handling not implemented methods
    /// </summary>
    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public string path = HttpContext.Current.Server.MapPath("~/Log") + "\\" + DateTime.Now.ToString("dd-mm-yyyy") + ".txt";
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is Exception)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent("Not Implemented Method")
                    //Content = new StringContent(context.Exception.Message)
                };
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(DateTime.Now);
                    sw.Write(context.Exception.StackTrace);
                    sw.WriteLine('\n');
                }
            }
        }
    }

}
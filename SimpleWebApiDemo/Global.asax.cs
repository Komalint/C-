using System;
using System.Web;
using System.Web.Http;

namespace SimpleWebApiDemo
{
    // =====================================================================================
    // Global.asax.cs - The Starting Point (Bootstrapper) of your .NET Web Application
    // =====================================================================================
    // When your application starts on the web server (IIS Express / IIS), ASP.NET looks for
    // this class and calls the Application_Start() method ONCE.
    //
    // For a Web API, all we need to do is register our WebApiConfig!
    // =====================================================================================
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // This single line tells ASP.NET:
            // "Configure and enable Web API routing and formatting using WebApiConfig.cs"
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
